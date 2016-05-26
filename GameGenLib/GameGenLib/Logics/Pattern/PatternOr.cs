using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.Pattern {
    internal class PatternOr : PatternsAggregator {

        public PatternOr(List<IPattern> childPatterns) : this(childPatterns, ShiftDirection.None) {
        }

        public PatternOr(List<IPattern> childPatterns, ShiftDirection nextCellDir) : base(childPatterns, nextCellDir) {
        }

        public override bool Find(CellsSequences cellsSequences, IPropertyContainer[] parameters) {
            bool any = false;
            foreach (var childPattern in ChildPatterns) {
                any |= childPattern.Find(cellsSequences, parameters);
            }
            return any;
        }

    }
}