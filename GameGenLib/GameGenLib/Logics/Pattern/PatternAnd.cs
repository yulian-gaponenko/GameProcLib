using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Pattern {
    internal class PatternAnd : IPattern {
        public ShiftDirection NextCellDir { get; }
        public List<IPattern> ChildPatterns { get; }

        public PatternAnd(List<IPattern> childPatterns) : this(childPatterns, ShiftDirection.None) {
        }

        public PatternAnd(List<IPattern> childPatterns, ShiftDirection nextCellDir) {
            ChildPatterns = childPatterns;
            NextCellDir = nextCellDir;
        }

        public bool Find(CellSequences cellSequences, IPropertyContainer[] args) {
            CellSequences resultSequence = new CellSequences(cellSequences.FirstCell);
            CellSequences nextSequence = resultSequence;
            foreach (IPattern childPattern in ChildPatterns) {
                if (childPattern.NextCellDir != ShiftDirection.None) {
                    Cell nextCell = nextSequence.FirstCell.NextCell(childPattern.NextCellDir);
                    nextSequence = nextSequence.AddNextCell(nextCell);
                }

                if (!childPattern.Find(cellSequences, args)) {
                    return false;
                }
                
            }
            cellSequences.NextCells.AddRange(resultSequence.NextCells);
            return true;
        }
    }
}