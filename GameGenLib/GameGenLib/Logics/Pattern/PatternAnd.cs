using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.Pattern {
    internal class PatternAnd : PatternsAggregator {
        public PatternAnd(List<IPattern> childPatterns) : this(childPatterns, ShiftDirection.None) {
        }

        public PatternAnd(List<IPattern> childPatterns, ShiftDirection nextCellDir) : base(childPatterns, nextCellDir) {
        }

        public override bool Find(CellsSequences cellsSequences, IPropertyContainer[] args) {
            CellsSequences resultSequence = new CellsSequences(cellsSequences.FirstCell);
            CellsSequences nextSequence = resultSequence;
            foreach (IPattern childPattern in ChildPatterns) {
                if (childPattern.NextCellDir != ShiftDirection.None) {
                    Cell nextCell = nextSequence.FirstCell.NextCell(childPattern.NextCellDir);
                    nextSequence = nextSequence.AddNextCell(nextCell).ToCellsSequences();
                }

                if (!childPattern.Find(cellsSequences, args)) {
                    return false;
                }
                
            }
            cellsSequences.NextCells.AddRange(resultSequence.NextCells);
            return true;
        }
    }
}