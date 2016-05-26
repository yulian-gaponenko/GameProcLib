using System;
using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    public class LogicInvocatorWithSpecificArgs : ILogic {
        private ILogic logic;
        private IList<IPropertyContainer> ownArguments;
        // positive integers for passing arguments; negative integers for passing own arguments
        private readonly IList<int> argumentsList;

        public LogicInvocatorWithSpecificArgs(ILogic logic, IList<int> argumentsList, IList<IPropertyContainer> ownArguments) {
            this.logic = logic;
            this.argumentsList = argumentsList;
            this.ownArguments = ownArguments;
        }

        public void Execute(params IPropertyContainer[] args) {
            IPropertyContainer[] resultantArguments = new IPropertyContainer[argumentsList.Count];
            for (int i = 0; i < argumentsList.Count; ++i) {
                if (argumentsList[i] < 0) {
                    resultantArguments[i] = ownArguments[-argumentsList[i] - 1];
                }
                else {
                    resultantArguments[i] = args[argumentsList[i]];
                }
            }

            logic.Execute(resultantArguments);
        }

        public IList<string> ArgsTypes { get; set; }
    }
}