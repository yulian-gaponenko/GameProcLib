using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Comparison;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    internal class IfElse : ILogic {
        private readonly IComparison comparison;
        private readonly ILogic thenLogic;
        private readonly ILogic elseLogic;

        public IfElse(IComparison comparison, ILogic thenLogic, ILogic elseLogic) {
            this.comparison = comparison;
            this.thenLogic = thenLogic;
            this.elseLogic = elseLogic;
        }

        public void Execute(params IPropertyContainer[] args) {
            if (comparison.Compare(args)) {
                thenLogic.Execute(args);
            } else {
                elseLogic.Execute(args);
            }
        }

        public IList<string> ArgsTypes { get; set; }
    }
}
