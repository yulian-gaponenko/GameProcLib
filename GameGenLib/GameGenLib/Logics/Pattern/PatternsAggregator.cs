using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.Pattern {
    internal abstract class PatternsAggregator : IPattern {
        protected PatternsAggregator(IList<IPattern> childPatterns, ShiftDirection nextCellDir) {
            ChildPatterns = childPatterns;
            NextCellDir = nextCellDir;
        }

        public abstract bool Find(CellsSequences cellsSequences, IPropertyContainer[] args);

        public ShiftDirection NextCellDir { get; }
        public IList<IPattern> ChildPatterns { get; }
    }
}