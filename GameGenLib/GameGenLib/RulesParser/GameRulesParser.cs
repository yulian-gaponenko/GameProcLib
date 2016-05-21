namespace GameGenLib.RulesParser {
    public class GameRulesParser {
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Game {

            private GameField fieldField;

            private GameCell cellField;

            private GameFigures figuresField;

            private GamePlayer[] playersField;

            private GameRules rulesField;

            private GameLogics logicsField;

            /// <remarks/>
            public GameField Field {
                get {
                    return this.fieldField;
                }
                set {
                    this.fieldField = value;
                }
            }

            /// <remarks/>
            public GameCell Cell {
                get {
                    return this.cellField;
                }
                set {
                    this.cellField = value;
                }
            }

            /// <remarks/>
            public GameFigures Figures {
                get {
                    return this.figuresField;
                }
                set {
                    this.figuresField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Player", IsNullable = false)]
            public GamePlayer[] Players {
                get {
                    return this.playersField;
                }
                set {
                    this.playersField = value;
                }
            }

            /// <remarks/>
            public GameRules Rules {
                get {
                    return this.rulesField;
                }
                set {
                    this.rulesField = value;
                }
            }

            /// <remarks/>
            public GameLogics Logics {
                get {
                    return this.logicsField;
                }
                set {
                    this.logicsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameField {

            private GameFieldProperty[] propertiesField;

            private byte sizeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Property", IsNullable = false)]
            public GameFieldProperty[] Properties {
                get {
                    return this.propertiesField;
                }
                set {
                    this.propertiesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Size {
                get {
                    return this.sizeField;
                }
                set {
                    this.sizeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameFieldProperty {

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameCell {

            private GameCellProperties propertiesField;

            /// <remarks/>
            public GameCellProperties Properties {
                get {
                    return this.propertiesField;
                }
                set {
                    this.propertiesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameCellProperties {

            private GameCellPropertiesProperty propertyField;

            /// <remarks/>
            public GameCellPropertiesProperty Property {
                get {
                    return this.propertyField;
                }
                set {
                    this.propertyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameCellPropertiesProperty {

            private string nameField;

            private string defaultField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Default {
                get {
                    return this.defaultField;
                }
                set {
                    this.defaultField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameFigures {

            private GameFiguresFigure figureField;

            /// <remarks/>
            public GameFiguresFigure Figure {
                get {
                    return this.figureField;
                }
                set {
                    this.figureField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameFiguresFigure {

            private string typeField;

            private string possibleMovesField;

            private string moveActionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type {
                get {
                    return this.typeField;
                }
                set {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PossibleMoves {
                get {
                    return this.possibleMovesField;
                }
                set {
                    this.possibleMovesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string MoveAction {
                get {
                    return this.moveActionField;
                }
                set {
                    this.moveActionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GamePlayer {

            private GamePlayerPlayerFigure playerFigureField;

            private string nameField;

            /// <remarks/>
            public GamePlayerPlayerFigure PlayerFigure {
                get {
                    return this.playerFigureField;
                }
                set {
                    this.playerFigureField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GamePlayerPlayerFigure {

            private string nameField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type {
                get {
                    return this.typeField;
                }
                set {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameRules {

            private GameRulesNextMoveEvent nextMoveEventField;

            private GameRulesPlayer[] victoryField;

            /// <remarks/>
            public GameRulesNextMoveEvent NextMoveEvent {
                get {
                    return this.nextMoveEventField;
                }
                set {
                    this.nextMoveEventField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Player", IsNullable = false)]
            public GameRulesPlayer[] Victory {
                get {
                    return this.victoryField;
                }
                set {
                    this.victoryField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameRulesNextMoveEvent {

            private GameRulesNextMoveEventDO doField;

            /// <remarks/>
            public GameRulesNextMoveEventDO Do {
                get {
                    return this.doField;
                }
                set {
                    this.doField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameRulesNextMoveEventDO {

            private GameRulesNextMoveEventDOPassParam passParamField;

            private string nameField;

            /// <remarks/>
            public GameRulesNextMoveEventDOPassParam PassParam {
                get {
                    return this.passParamField;
                }
                set {
                    this.passParamField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameRulesNextMoveEventDOPassParam {

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameRulesPlayer {

            private string nameField;

            private string fieldPropertyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string FieldProperty {
                get {
                    return this.fieldPropertyField;
                }
                set {
                    this.fieldPropertyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogics {

            private GameLogicsLogic[] logicField;

            private GameLogicsPatternElem[] patternElemsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Logic")]
            public GameLogicsLogic[] Logic {
                get {
                    return this.logicField;
                }
                set {
                    this.logicField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("PatternElem", IsNullable = false)]
            public GameLogicsPatternElem[] PatternElems {
                get {
                    return this.patternElemsField;
                }
                set {
                    this.patternElemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogic {

            private GameLogicsLogicIF ifField;

            private GameLogicsLogicDO[] doField;

            private GameLogicsLogicSetProperty setPropertyField;

            private GameLogicsLogicGetCells[] getCellsField;

            private GameLogicsLogicAddCells addCellsField;

            private string nameField;

            private string typeField;

            private byte paramsNumField;

            private bool paramsNumFieldSpecified;

            /// <remarks/>
            public GameLogicsLogicIF If {
                get {
                    return this.ifField;
                }
                set {
                    this.ifField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Do")]
            public GameLogicsLogicDO[] Do {
                get {
                    return this.doField;
                }
                set {
                    this.doField = value;
                }
            }

            /// <remarks/>
            public GameLogicsLogicSetProperty SetProperty {
                get {
                    return this.setPropertyField;
                }
                set {
                    this.setPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("GetCells")]
            public GameLogicsLogicGetCells[] GetCells {
                get {
                    return this.getCellsField;
                }
                set {
                    this.getCellsField = value;
                }
            }

            /// <remarks/>
            public GameLogicsLogicAddCells AddCells {
                get {
                    return this.addCellsField;
                }
                set {
                    this.addCellsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type {
                get {
                    return this.typeField;
                }
                set {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ParamsNum {
                get {
                    return this.paramsNumField;
                }
                set {
                    this.paramsNumField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ParamsNumSpecified {
                get {
                    return this.paramsNumFieldSpecified;
                }
                set {
                    this.paramsNumFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIF {

            private GameLogicsLogicIFEquals equalsField;

            private GameLogicsLogicIFThen thenField;

            private GameLogicsLogicIFElse elseField;

            /// <remarks/>
            public GameLogicsLogicIFEquals Equals {
                get {
                    return this.equalsField;
                }
                set {
                    this.equalsField = value;
                }
            }

            /// <remarks/>
            public GameLogicsLogicIFThen Then {
                get {
                    return this.thenField;
                }
                set {
                    this.thenField = value;
                }
            }

            /// <remarks/>
            public GameLogicsLogicIFElse Else {
                get {
                    return this.elseField;
                }
                set {
                    this.elseField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFEquals {

            private GameLogicsLogicIFEqualsGetProperty getPropertyField;

            private string valueField;

            /// <remarks/>
            public GameLogicsLogicIFEqualsGetProperty GetProperty {
                get {
                    return this.getPropertyField;
                }
                set {
                    this.getPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Value {
                get {
                    return this.valueField;
                }
                set {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFEqualsGetProperty {

            private string objField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFThen {

            private GameLogicsLogicIFThenSetProperty setPropertyField;

            /// <remarks/>
            public GameLogicsLogicIFThenSetProperty SetProperty {
                get {
                    return this.setPropertyField;
                }
                set {
                    this.setPropertyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFThenSetProperty {

            private GameLogicsLogicIFThenSetPropertyGetProperty getPropertyField;

            private string objField;

            private string nameField;

            /// <remarks/>
            public GameLogicsLogicIFThenSetPropertyGetProperty GetProperty {
                get {
                    return this.getPropertyField;
                }
                set {
                    this.getPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFThenSetPropertyGetProperty {

            private string objField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFElse {

            private GameLogicsLogicIFElseSetProperty setPropertyField;

            /// <remarks/>
            public GameLogicsLogicIFElseSetProperty SetProperty {
                get {
                    return this.setPropertyField;
                }
                set {
                    this.setPropertyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFElseSetProperty {

            private GameLogicsLogicIFElseSetPropertyGetProperty getPropertyField;

            private string objField;

            private string nameField;

            /// <remarks/>
            public GameLogicsLogicIFElseSetPropertyGetProperty GetProperty {
                get {
                    return this.getPropertyField;
                }
                set {
                    this.getPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicIFElseSetPropertyGetProperty {

            private string objField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicDO {

            private GameLogicsLogicDOPassParam[] passParamField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("PassParam")]
            public GameLogicsLogicDOPassParam[] PassParam {
                get {
                    return this.passParamField;
                }
                set {
                    this.passParamField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicDOPassParam {

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicSetProperty {

            private GameLogicsLogicSetPropertyGetProperty getPropertyField;

            private string nameField;

            /// <remarks/>
            public GameLogicsLogicSetPropertyGetProperty GetProperty {
                get {
                    return this.getPropertyField;
                }
                set {
                    this.getPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicSetPropertyGetProperty {

            private string objField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCells {

            private GameLogicsLogicGetCellsWithPattern withPatternField;

            private GameLogicsLogicGetCellsWithProperty withPropertyField;

            /// <remarks/>
            public GameLogicsLogicGetCellsWithPattern WithPattern {
                get {
                    return this.withPatternField;
                }
                set {
                    this.withPatternField = value;
                }
            }

            /// <remarks/>
            public GameLogicsLogicGetCellsWithProperty WithProperty {
                get {
                    return this.withPropertyField;
                }
                set {
                    this.withPropertyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCellsWithPattern {

            private GameLogicsLogicGetCellsWithPatternPatternElem patternElemField;

            /// <remarks/>
            public GameLogicsLogicGetCellsWithPatternPatternElem PatternElem {
                get {
                    return this.patternElemField;
                }
                set {
                    this.patternElemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCellsWithPatternPatternElem {

            private GameLogicsLogicGetCellsWithPatternPatternElemPatternElem[] patternElemField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("PatternElem")]
            public GameLogicsLogicGetCellsWithPatternPatternElemPatternElem[] PatternElem {
                get {
                    return this.patternElemField;
                }
                set {
                    this.patternElemField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type {
                get {
                    return this.typeField;
                }
                set {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCellsWithPatternPatternElemPatternElem {

            private GameLogicsLogicGetCellsWithPatternPatternElemPatternElemPatternElem patternElemField;

            private byte quantField;

            /// <remarks/>
            public GameLogicsLogicGetCellsWithPatternPatternElemPatternElemPatternElem PatternElem {
                get {
                    return this.patternElemField;
                }
                set {
                    this.patternElemField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Quant {
                get {
                    return this.quantField;
                }
                set {
                    this.quantField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCellsWithPatternPatternElemPatternElemPatternElem {

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicGetCellsWithProperty {

            private string nameField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Value {
                get {
                    return this.valueField;
                }
                set {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsLogicAddCells {

            private string formatField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Format {
                get {
                    return this.formatField;
                }
                set {
                    this.formatField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsPatternElem {

            private GameLogicsPatternElemWithProperty withPropertyField;

            private string nameField;

            private string directionField;

            /// <remarks/>
            public GameLogicsPatternElemWithProperty WithProperty {
                get {
                    return this.withPropertyField;
                }
                set {
                    this.withPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string direction {
                get {
                    return this.directionField;
                }
                set {
                    this.directionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsPatternElemWithProperty {

            private GameLogicsPatternElemWithPropertyGetProperty getPropertyField;

            private string nameField;

            /// <remarks/>
            public GameLogicsPatternElemWithPropertyGetProperty GetProperty {
                get {
                    return this.getPropertyField;
                }
                set {
                    this.getPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name {
                get {
                    return this.nameField;
                }
                set {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GameLogicsPatternElemWithPropertyGetProperty {

            private string objField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Obj {
                get {
                    return this.objField;
                }
                set {
                    this.objField = value;
                }
            }
        }


    }
}
