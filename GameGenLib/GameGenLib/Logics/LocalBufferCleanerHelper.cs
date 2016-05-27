using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics {
    public class LocalBufferCleanerHelper : ILogic {
        private readonly CellsCollectionHolder cHolder;

        public LocalBufferCleanerHelper(CellsCollectionHolder cHolder) {
            this.cHolder = cHolder;
        }

        public void Execute(params IPropertyContainer[] args) {
            cHolder.Cells = new CellsSet();
        }

        public IList<string> ArgsTypes { get; set; }
    }
}