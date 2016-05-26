using GameGenLib.GameEntities;

namespace GameGenLib.Logics.PropertyAccessers {

    // TODO probably remove
    internal class ConstantAccessor : IPropertyAccessor {
        private readonly int value;

        public ConstantAccessor(int value) {
            this.value = value;
        }

        public int GetProperty(IPropertyContainer[] args) {
            return value;
        }

        public void SetProperty(int value, IPropertyContainer[] args) {
            throw new System.InvalidOperationException();
        }
    }
}