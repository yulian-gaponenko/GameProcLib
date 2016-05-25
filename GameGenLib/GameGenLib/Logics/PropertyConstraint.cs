using GameGenLib.GameEntities;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    internal class PropertyConstraint {
        private readonly int propName;
        private readonly IPropertyAccessor expectedValueAccessor;

        public PropertyConstraint(int propName, IPropertyAccessor expectedValueAccessor) {
            this.propName = propName;
            this.expectedValueAccessor = expectedValueAccessor;
        }

        public bool Check(Cell cell, IPropertyContainer[] args) {
            return cell.GetProperty(propName) == expectedValueAccessor.GetProperty(args);
        }
    }
}