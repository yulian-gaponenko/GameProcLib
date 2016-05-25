using GameGenLib.GameEntities;

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

        public bool Find(CellSequences cellSequences, IPropertyContainer[] args) {
            return constraint.Check(cellSequences.FirstCell, args);
        }

    }
}