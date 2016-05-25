using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Pattern {
    internal class PatternOr : IPattern {
        public ShiftDirection NextCellDir { get; }
        public List<IPattern> ChildPatterns { get; }

        public PatternOr(List<IPattern> childPatterns) : this(childPatterns, ShiftDirection.None) {
        }

        public PatternOr(List<IPattern> childPatterns, ShiftDirection nextCellDir) {
            ChildPatterns = childPatterns;
            NextCellDir = nextCellDir;
        }

        public bool Find(CellSequences cellSequences, IPropertyContainer[] parameters) {
            bool any = false;
            foreach (var childPattern in ChildPatterns) {
                any |= childPattern.Find(cellSequences, parameters);
            }
            return any;
        }

    }
}