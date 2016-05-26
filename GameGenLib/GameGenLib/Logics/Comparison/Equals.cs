using GameGenLib.GameEntities;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics.Comparison {
    internal class Equals : IComparison {
        public IPropertyAccessor EqualsLeft { get; }
        public IPropertyAccessor EqualsRight { get; }

        public Equals(IPropertyAccessor equalsLeft, IPropertyAccessor equalsRight) {
            this.EqualsLeft = equalsLeft;
            this.EqualsRight = equalsRight;
        }

        public bool Compare(IPropertyContainer[] args) {
            return EqualsLeft.GetProperty(args) == EqualsRight.GetProperty(args);
        }
    }
}