using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Cells {
    public class CellsSequences : ICells {
        public CellsSequences(Cell firstCell) {
            FirstCell = firstCell;
            NextCells = new List<CellsSequences>();
        }

        public List<CellsSequences> NextCells { get; }
        public Cell FirstCell { get; }

        public ICells AddNextCell(Cell nextCell) {
            var cellSequences = new CellsSequences(nextCell);
            NextCells.Add(cellSequences);
            return cellSequences;
        }

        public int Size { get { return NumberOfSequences;} }

        public CellsSequences ToCellsSequences() {
            return this;
        }

        public CellsSet ToCellsSet() {
            CellsSet cellsSet = new CellsSet();
            AddCellsToSet(cellsSet);
            return cellsSet;
        }

        public int NumberOfCells {
            get {
                int totalNumber = FirstCell != null ? 1 : 0;
                foreach (var nextCell in NextCells) {
                    totalNumber += nextCell.NumberOfCells;
                }
                return totalNumber;;
            }
        }

        public int NumberOfSequences {
            get {
                if (NextCells.Count == 0) {
                    return 1;
                }

                int size = 0;
                foreach (var next in NextCells) {
                    size += next.NumberOfSequences;
                }

                return size;
            }
        }

        private void AddCellsToSet(CellsSet cellsSet) {
            if (FirstCell != null) {
                cellsSet.AddNextCell(FirstCell);
            }
            foreach (var nextCell in NextCells) {
                nextCell.AddCellsToSet(cellsSet);
            }
        }
    }
}