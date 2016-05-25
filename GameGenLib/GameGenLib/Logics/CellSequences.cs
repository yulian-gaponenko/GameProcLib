using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics {
    public class CellSequences {
        public CellSequences(Cell firstCell) {
            FirstCell = firstCell;
            NextCells = new List<CellSequences>();
        }

        public List<CellSequences> NextCells { get; }
        public Cell FirstCell { get; }

        public CellSequences AddNextCell(Cell nextCell) {
            var cellSequences = new CellSequences(nextCell);
            NextCells.Add(cellSequences);
            return cellSequences;
        }

//        public int NumberOfCells {
            
//        }

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
    }
}