using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;
using GameGenLib.Logics.Pattern;

namespace GameGenLib.Logics {
    internal class ApplyPatternLogic : ILogic {
        private readonly CellsCollectionHolder cHolder;
        private readonly bool asSequentialCells;
        private readonly IPattern pattern;

        public ApplyPatternLogic(IPattern pattern, bool asSequentialCells, CellsCollectionHolder cHolder) {
            this.pattern = pattern;
            this.asSequentialCells = asSequentialCells;
            this.cHolder = cHolder;
        }

        public void Execute(params IPropertyContainer[] args) {
            CellsSequences result = new CellsSequences(null);
            IList<Cell> cells = cHolder.Cells.ToCellsSet().Cells;
            foreach (Cell cell in cells) {
                var cellsSequences = new CellsSequences(cell);
                if (pattern.Find(cellsSequences, args)) {
                    result.NextCells.Add(cellsSequences);
                }
            }
            if (asSequentialCells) {
                cHolder.Cells = result;
            }
            else {
                cHolder.Cells = result.ToCellsSet();
            }
        }

        public IList<string> ArgsTypes { get; set; }
    }
}