using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GameGenLib.GameEntities;
using GameGenLib.Logics;
using GameGenLib.Logics.Cells;
using GameGenLib.Logics.Comparison;
using GameGenLib.Logics.Pattern;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.GameParser {
    internal class LogicsParser {
        private const string AddCellsNodeName = "AddCells";
        private const string LogicNodeName = "Logic";

        private const string NameAttributeName = "Name";
        private const string WithPropertyNodeName = "WithProperty";
        private const string ValueAttributeName = "Value";
        private const string SaveResultNodeName = "SaveResult";
        private const string SetPropertyToCellsNodeName = "SetPropertyToCells";
        private const string GetPropertyNodeName = "GetProperty";
        private const string ObjAttributeName = "Obj";
        private const string IndexAttributeName = "Index";
        private const string DoNodeName = "Do";
        private const string PassParamNodeName = "PassParam";
        private const string IfElseNodeName = "IfElse";
        private const string ThenAttributeName = "Then";
        private const string ElseAttributeName = "Else";
        private const string EqualsNodeName = "Equals";
        private const string PatternElemsNodeName = "PatternElems";
        private const string PatternElemNodeName = "PatternElem";
        private const string DirectionAttributeName = "Direction";
        private const string TypeAttributeName = "Type";
        private const string ApplyPatternNodeName = "ApplyPattern";
        private const string SourceAttributeName = "Source";
        private const string SetPropertyNodeName = "SetProperty";

        

        private readonly XElement logicsNode;
        private readonly IDictionary<string, LogicAgregator> logics;
        private readonly IDictionary<string, IPattern> patterns;
        private readonly PropertiesMapping propertiesMapping;
        private readonly GameXmlParser.GlobalCollections globalCollections;
        private readonly GameContext context;

        // TODO carefully update this field
        private String[] currentArgTypes;

        public LogicsParser(XElement logicsNode, IDictionary<string, LogicAgregator> logics, PropertiesMapping mapping, GameXmlParser.GlobalCollections globalCollections, GameContext context) {
            this.logicsNode = logicsNode;
            this.logics = logics;
            this.context = context;
            this.propertiesMapping = mapping;
            this.globalCollections = globalCollections;
            this.patterns = new Dictionary<string, IPattern>();
        }

        public void ParseLogics() {
            ParsePatternElemsNames(logicsNode.Element(PatternElemsNodeName));
            foreach (var logicNode in logicsNode.Elements(LogicNodeName)) {
                LogicAgregator logic = logics[logicNode.Attribute(NameAttributeName).Value];
                ParseLogic(logic, logicNode);
            }

            ParsePatternElems(logicsNode.Element(PatternElemsNodeName));

            context.CurrMoveCells = globalCollections.CurrMoveCells;
            context.CurrFigurePossibleMoves = globalCollections.CurrFigurePossibleMoves;
        }

        private void ParsePatternElemsNames(XElement patternElemsNode) {
            foreach (var patternElemNode in patternElemsNode.Elements(PatternElemNodeName)) {
                string patternElemName = patternElemNode.Attribute(NameAttributeName).Value;
                patterns[patternElemName] = ParsePatternElem(patternElemNode, false);
            }
        }

        private void ParsePatternElems(XElement patternElemsNode) {
            foreach (var patternElemNode in patternElemsNode.Elements(PatternElemNodeName)) {
                string patternElemName = patternElemNode.Attribute(NameAttributeName).Value;
                PatternsAggregator pattern = patterns[patternElemName] as PatternsAggregator;
                if (pattern != null) {
                    ParsePatternElem(patternElemNode, pattern);
                }
            }
        }

        private void ParsePatternElem(XElement patternElemNode, PatternsAggregator pattern) {
            foreach (var nestedPatternElemNode in patternElemNode.Elements(PatternElemNodeName)) {
                string patternElemName = nestedPatternElemNode.Attribute(NameAttributeName)?.Value;
                IPattern nestedPattern = patternElemName != null 
                    ? patterns[patternElemName] 
                    : ParsePatternElem(nestedPatternElemNode, true);
                pattern.ChildPatterns.Add(nestedPattern);
            }

        }

        private IPattern ParsePatternElem(XElement patternElemNode, bool parseRecursevily) {
            ShiftDirection dir = ShiftDirection.None;
            var shiftAttribute = patternElemNode.Attribute(DirectionAttributeName);
            if (shiftAttribute != null) {
                dir = ShiftDirectionFromString(shiftAttribute.Value);
            }
            string patternType = patternElemNode.Attribute(TypeAttributeName)?.Value;
            IPattern pattern;
            if (patternType == "Check") {
                var constraint = ParsePropertyConstraint(patternElemNode.Element(WithPropertyNodeName));
                pattern = new PatternCheck(constraint, dir);
            } else if (patternType == "|") {
                pattern = new PatternOr(new List<IPattern>(), dir);
            } else {
                pattern = new PatternAnd(new List<IPattern>(), dir);
            }
            if (parseRecursevily && pattern is PatternsAggregator) {
                var patternsAggregator = (PatternsAggregator) pattern;
                ParsePatternElem(patternElemNode, patternsAggregator);
                string quantAttribute = patternElemNode.Attribute("Quant")?.Value;
                if (quantAttribute != null) {
                    int quant = int.Parse(quantAttribute);
                    int origSize = patternsAggregator.ChildPatterns.Count;
                    for (int i = 0; i < quant - 1; ++i) {
                        for (int j = 0; j < origSize; ++j) {
                            patternsAggregator.ChildPatterns.Add(patternsAggregator.ChildPatterns[j]);
                        }
                    }
                }
            }
            return pattern;
        }

        private void ParseLogic(LogicAgregator logic, XElement logicNode) {
            logic.AddPreLogic(new LocalBufferCleanerHelper(globalCollections.LocalBuffer));
            foreach (var element in logicNode.Elements()) {
                logic.AddInnerLogic(ParseLogicElement(element));
            }
        }

        private ILogic ParseLogicElement(XElement element) {
            string elementName = element.Name.LocalName;
            ILogic logicElement;
            switch (elementName) {
                case AddCellsNodeName:
                    logicElement = ParseAddCellsLogic(element);
                    break;
                case SaveResultNodeName:
                    logicElement = new CellsCopier(globalCollections.LocalBuffer, globalCollections.LastLogicResult);
                    break;
                case SetPropertyToCellsNodeName:
                    logicElement = ParseCellsPropertySetter(element);
                    break;
                case DoNodeName:
                    logicElement = ParseDo(element);
                    break;
                case IfElseNodeName:
                    logicElement = ParseIfElse(element);
                    break;
                case ApplyPatternNodeName:
                    logicElement = ParseApplyPattern(element);
                    break;
                case SetPropertyNodeName:
                    logicElement = ParseSetProperty(element);
                    break;
                default:
                    throw new Exception($"Unknown logic element {elementName}");
            }

            return logicElement;
        }

        private ILogic ParseSetProperty(XElement setPropertyNode) {
            return new PropertySetter(ParsePropertyAccessor(setPropertyNode), ParsePropertyAccessor(setPropertyNode.Element(GetPropertyNodeName)));
        }

        private ILogic ParseApplyPattern(XElement applyPatternNode) {
            bool asSequentialCells = true;
            string value = applyPatternNode.Attribute("Result")?.Value;
            if (value != null) {
                asSequentialCells = value == "AsSequentialCells";
            }
            return new ApplyPatternLogic(ParsePatternElem(applyPatternNode.Element(PatternElemNodeName), true), asSequentialCells, globalCollections.LocalBuffer);
        }

        private ILogic ParseIfElse(XElement ifElseNode) {
            string thenName = ifElseNode.Attribute(ThenAttributeName).Value;
            string elseName = ifElseNode.Attribute(ElseAttributeName).Value;
            IComparison c;
            var equalsNode = ifElseNode.Element(EqualsNodeName);
            if (equalsNode != null) {
                c = ParseEqualsNode(equalsNode);
            }
            else {
                throw new Exception("Unknown or absent comparison node.");
            }
            return new IfElse(c, logics[thenName], logics[elseName]);
        }

        private IComparison ParseEqualsNode(XElement equalsNode) {
            var propertyAccessorNodes = equalsNode.Elements(GetPropertyNodeName).GetEnumerator();
            propertyAccessorNodes.MoveNext();
            var leftNode = propertyAccessorNodes.Current;
            propertyAccessorNodes.MoveNext();
            var rightNode = propertyAccessorNodes.Current;
            return new Equals(ParsePropertyAccessor(leftNode), ParsePropertyAccessor(rightNode));
        }

        private ILogic ParseDo(XElement doNode) {
            // TODO here change currArgsTypes and args themselves
            string logicName = doNode.Attribute(NameAttributeName).Value;
            var newParams = doNode.Elements(PassParamNodeName);
            if (newParams.Any()) {
                // TODO parse new params
            }
            return logics[logicName];
        }

        private ILogic ParseCellsPropertySetter(XElement cellsPropertySetterNode) {
            string cellPropertyNameValue = cellsPropertySetterNode.Attribute(NameAttributeName).Value;
            int cellProperty = propertiesMapping.GetCellPropertyIndex(cellPropertyNameValue);
            return new CellsPropertySetter(globalCollections.LocalBuffer, cellProperty, ParsePropertyAccessor(cellsPropertySetterNode.Element(GetPropertyNodeName)));
        }

        private IPropertyAccessor ParsePropertyAccessor(XElement propertyAccessorNode) {
            var nameAttribute = propertyAccessorNode.Attribute(NameAttributeName);
            var objAttribute = propertyAccessorNode.Attribute(ObjAttributeName);
            var indexAttribute = propertyAccessorNode.Attribute(IndexAttributeName);
            var valueAttribute = propertyAccessorNode.Attribute(ValueAttributeName);
            var sourceAttribute = propertyAccessorNode.Attribute(SourceAttributeName);
            if (objAttribute != null) {
                IPropertyContainer globalEntity = GetGlobalEntity(objAttribute.Value);
                int propName = propertiesMapping.GetPropertyIndex(nameAttribute.Value, globalEntity.ContainerType);
                return new SpecificContainerPropertyAccessor(propName, globalEntity);
            }
            else if (indexAttribute != null) {
                int index = int.Parse(indexAttribute.Value);
                int propName = propertiesMapping.GetPropertyIndex(nameAttribute.Value, currentArgTypes[index]);
                return new ArgumentPropertyAccessor(propName, index);
            } else if (valueAttribute != null) {
                return new ConstantAccessor(int.Parse(valueAttribute.Value));
            } else if (sourceAttribute != null) {
                return new CellsCollectionSizeGetter(GetGlobalCellsCollection(sourceAttribute.Value));
            }
            else {
                throw new Exception("Invalid GetProperty node.");
            }
        }

        private ILogic ParseAddCellsLogic(XElement element) {
            AddCells addCellsLogic = new AddCells(globalCollections.LocalBuffer, context.Field);
            foreach (var withPropertyNode in element.Elements(WithPropertyNodeName)) {
                PropertyConstraint constraint = ParsePropertyConstraint(withPropertyNode);
                addCellsLogic.AddConstraint(constraint);
            }
            return addCellsLogic;
        }

        private PropertyConstraint ParsePropertyConstraint(XElement withPropertyNode) {
            string cellPropertyName = withPropertyNode.Attribute(NameAttributeName).Value;
            int propName = propertiesMapping.GetCellPropertyIndex(cellPropertyName);

            IPropertyAccessor accessor = ParsePropertyAccessor(withPropertyNode.Element(GetPropertyNodeName));
            return new PropertyConstraint(propName, accessor);
        }

        public IPropertyContainer GetGlobalEntity(string entityName) {
            switch (entityName) {
                case "CurrPlayer":
                    return context.CurrPlayer;
                case "CurrFigure":
                    return context.CurrFigure;
                case "Field":
                    return context.Field;
                case "Player0":
                    return context.Players[0];
                case "Player1":
                    return context.Players[1];
                default:
                    throw new Exception($"Unknown global entity name: {entityName}");
            }
        }

        public CellsCollectionHolder GetGlobalCellsCollection(string collectionName) {
            switch (collectionName) {
                case "LocalBuffer":
                    return globalCollections.LocalBuffer;
                case "LastLogicResult":
                    return globalCollections.LastLogicResult;
                case "CurrFigurePossibleMoves":
                    return globalCollections.CurrFigurePossibleMoves;
                case "CurrMoveCells":
                    return globalCollections.CurrMoveCells;
                default:
                    throw new Exception($"Unknown global cells collection name: {collectionName}");
            }
        }

        private ShiftDirection ShiftDirectionFromString(string value) {
            switch (value) {
                case "ul":
                    return ShiftDirection.LeftUp;
                case "u":
                    return ShiftDirection.Up;
                case "ur":
                    return ShiftDirection.RightUp;
                case "dl":
                    return ShiftDirection.LeftDown;
                case "d":
                    return ShiftDirection.Down;
                case "dr":
                    return ShiftDirection.RightDown;
                case "r":
                    return ShiftDirection.Right;
                case "l":
                    return ShiftDirection.Left;
                default:
                    return ShiftDirection.None;
            }
        }
    }
}