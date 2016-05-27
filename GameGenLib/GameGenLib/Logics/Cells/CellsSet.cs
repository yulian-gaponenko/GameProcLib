using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Cells {
    public class CellsSet : ICells {
        public CellsSet() {
            Cells = new List<Cell>();
        }

        public IList<Cell> Cells { get; }
        public CellsSequences ToCellsSequences() {
            if (Cells.Count == 0) {
                throw new InvalidOperationException("Cannot convert empty set to CellsSequences.");
            }
            CellsSequences cellsSequences = new CellsSequences(null);
            foreach (Cell cell in Cells) {
                cellsSequences.AddNextCell(cell);
            }
            return cellsSequences;
        }

        public CellsSet ToCellsSet() {
            return this;
        }

        public ICells AddNextCell(Cell nextCell) {
            Cells.Add(nextCell);
            return this;
        }

        public int NumberOfCells {
            get { return Cells.Count; }
        }

        public int Size { get { return NumberOfCells; } }
    }
}