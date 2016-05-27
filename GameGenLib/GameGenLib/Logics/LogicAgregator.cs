using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics {
    class LogicAgregator : ILogic {
        private readonly IList<ILogic> preLogics = new List<ILogic>();
        private readonly IList<ILogic> innerLogics = new List<ILogic>();
        private readonly IList<ILogic> postLogics = new List<ILogic>();

        public void AddInnerLogic(ILogic logic) {
            innerLogics.Add(logic);
        }

        public void Execute(params IPropertyContainer[] parameters) {
            foreach (ILogic logic in preLogics) {
               logic.Execute(parameters);
            }
            foreach (ILogic innerLogic in innerLogics) {
               innerLogic.Execute(parameters);
            }
            foreach (ILogic logic in postLogics) {
               logic.Execute(parameters);
            }
        }

        public IList<string> ArgsTypes { get; set; }

        public void AddPreLogic(ILogic preLogic) {
            preLogics.Insert(0, preLogic);
        }

        public void AddPostLogic(ILogic postLogic) {
            postLogics.Add(postLogic);
        }
    }
}