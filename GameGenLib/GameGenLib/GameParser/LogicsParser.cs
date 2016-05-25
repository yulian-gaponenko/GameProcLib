using System;
using System.Collections.Generic;
using System.Xml.Linq;
using GameGenLib.Logics;

namespace GameGenLib.GameParser {
    internal class LogicsParser {
        private const string AddCellsNodeName = "AddCells";
        private const string LogicNodeName = "Logic";

        private const string NameAttributeName = "Name";

        private readonly XElement logicsNode;
        private readonly IDictionary<string, LogicAgregator> logics;

        public LogicsParser(XElement logicsNode, IDictionary<string, LogicAgregator> logics) {
            this.logicsNode = logicsNode;
            this.logics = logics;
        }

        public void ParseLogics() {
            foreach (var logicNode in logicsNode.Elements(LogicNodeName)) {
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

            throw new NotImplementedException();
            return logicElement;
        }

        private ILogic ParseAddCellsLogic(XElement element) {
            throw new NotImplementedException();
        }
    }
}