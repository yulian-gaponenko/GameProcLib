using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics {
    class LogicAgregator : ILogic {
        private readonly IList<ILogic> innerLogics = new List<ILogic>();

        public LogicAgregator(IList<string> argsTypes) {
            ArgsTypes = argsTypes;
        }

        public void AddInnerLogic(ILogic logic) {
            innerLogics.Add(logic);
        }

        public void Execute(params IPropertyContainer[] parameters) {
            foreach (ILogic innerLogic in innerLogics) {
               innerLogic.Execute(parameters);
            }
        }

        public IList<string> ArgsTypes { get; set; }
    }
}