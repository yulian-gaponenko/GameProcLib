using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics {
    public class CellsCopier : ILogic {
        private readonly CellsCollectionHolder fromHolder;
        private readonly CellsCollectionHolder toHolder;

        public CellsCopier(CellsCollectionHolder fromHolder, CellsCollectionHolder toHolder) {
            this.fromHolder = fromHolder;
            this.toHolder = toHolder;
        }

        public void Execute(params IPropertyContainer[] args) {
            toHolder.Cells = fromHolder.Cells;
        }

        public IList<string> ArgsTypes { get; set; }
    }
}