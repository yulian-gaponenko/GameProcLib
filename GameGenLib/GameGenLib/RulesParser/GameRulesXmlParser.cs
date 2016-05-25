using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GameGenLib.GameEntities;
using GameGenLib.Logics;

namespace GameGenLib.RulesParser {
    internal class GameRulesXmlParser {
        private const string FieldNodeName = "Field";
        private const string SizeAttributeName = "Size";
        private const string LogicsNodeName = "Logics";
        private const string LogicNodeName = "Logic";
        private const string NameAttributeName = "Name";
        private const string AddCellsNodeName = "AddCells";

        private readonly XDocument doc;
        private readonly PropertiesMapping propertiesMapping;
        private readonly IDictionary<string, LogicAgregator> logics = new Dictionary<string, LogicAgregator>();

        public GameRulesXmlParser(TextReader rulesXmlReader) {
            doc = XDocument.Load(rulesXmlReader);
            if (doc == null) {
                throw new FileLoadException("Could not load xml rules.");
            }

            propertiesMapping = new PropertiesMapping();
        }

        public GameContext Parse() {
            ParseLogics();
            GameField field = ParseField();
//            Player p = new Player();

            return new GameContext(field, new List<Player>());
        }

        private void ParseLogics() {
            var logicsNode = doc.Root.Element(LogicsNodeName);
            foreach (var logicName in logicsNode.Elements(LogicNodeName).Select(node => node.Attribute(NameAttributeName).Value)) {
                logics[logicName] = new LogicAgregator();
            }

            foreach (var logicNode in logicsNode.Elements(LogicsNodeName)) {
                LogicAgregator logic = logics[logicNode.Attribute(NameAttributeName).Value];
                ParseLogic(logic, logicNode);
            }
        }

        private void ParseLogic(LogicAgregator logic, XElement logicNode) {
            // TODO Process specific types of logics
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
            }

            return logicElement;
        }

        private ILogic ParseAddCellsLogic(XElement element) {
            throw new NotImplementedException();
        }

        private GameField ParseField() {
            var fieldElement = doc.Root.Element(FieldNodeName);
            string sizeString = fieldElement.Attribute(SizeAttributeName).Value;

//            var properties = fieldElement.Element(PropertiesNodeName);
//            if (properties != null) {
//                foreach (var propertyElement in properties.Elements()) {
//                    propertiesMapping. propertyElement.Name
//                }
//            }
            return new GameField(Convert.ToInt32(sizeString));
        }

        /*
            <Game>
                <Field>
                <Logics>
            </Game>

            <Field Size="INT"/>

            <Logics>
                <Logic> +
            </Logics>

            <Logic>
                ( <AddCells> | <SetProperty> | <Do> | <ApplyPattern> | <If> ) *
            </Logic>

            <AddCells>
                <WithProperty> *
            </AddCells>

            <WithProperty Name="STRING" Value="STRING"/>
        */
    }
}