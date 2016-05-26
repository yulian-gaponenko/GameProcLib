using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.PropertyAccessers {
    internal class CellsCollectionSizeGetter : IPropertyAccessor {
        private readonly CellsCollectionHolder cHolder;

        public CellsCollectionSizeGetter(CellsCollectionHolder cHolder) {
            this.cHolder = cHolder;
        }

        public int GetProperty(IPropertyContainer[] args) {
            return cHolder.Cells.Size;
        }

        public void SetProperty(int value, IPropertyContainer[] args) {
            throw new System.InvalidOperationException();
        }
    }
}