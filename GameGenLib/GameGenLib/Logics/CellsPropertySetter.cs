using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    internal class CellsPropertySetter : ILogic {
        private readonly int cellPropertyName;
        private readonly IPropertyAccessor valueGetter;
        private readonly CellsCollectionHolder cHolder;

        public CellsPropertySetter(CellsCollectionHolder cHolder, int cellPropertyName, IPropertyAccessor valueGetter) { 
            this.cHolder = cHolder;
            this.cellPropertyName = cellPropertyName;
            this.valueGetter = valueGetter;
        }

        public void Execute(params IPropertyContainer[] args) {
            IList<Cell> cells = cHolder.Cells.ToCellsSet().Cells;
            foreach (Cell cell in cells) {
                cell.SetProperty(cellPropertyName, valueGetter.GetProperty(args));
            }
        }

        public IList<string> ArgsTypes { get; set; }
    }
}