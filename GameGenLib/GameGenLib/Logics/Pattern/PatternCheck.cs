using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.Pattern {
    internal class PatternCheck  : IPattern {
        private readonly PropertyConstraint constraint;

        public ShiftDirection NextCellDir { get; }


        public PatternCheck(PropertyConstraint constraint) : this(constraint, ShiftDirection.None) {
        }

        public PatternCheck(PropertyConstraint constraint, ShiftDirection nextCellDir) {
            this.constraint = constraint;
            NextCellDir = nextCellDir;
        }

        public bool Find(CellsSequences cellsSequences, IPropertyContainer[] args) {
            return constraint.Check(cellsSequences.FirstCell, args);
        }

    }
}