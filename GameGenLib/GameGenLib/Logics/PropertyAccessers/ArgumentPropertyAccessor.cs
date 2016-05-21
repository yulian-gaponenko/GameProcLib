using GameGenLib.GameEntities;

namespace GameGenLib.Logics.PropertyAccessers {
    internal class ArgumentPropertyAccessor : IPropertyAccessor {
        private readonly int propertyName;
        private readonly int argsContainerIndex;

        public ArgumentPropertyAccessor(int propertyName, int argsContainerIndex) {
            this.propertyName = propertyName;
            this.argsContainerIndex = argsContainerIndex;
        }

        public int GetProperty(IPropertyContainer[] args) {
            return args[argsContainerIndex].GetProperty(propertyName);
        }

        public void SetProperty(int value, IPropertyContainer[] args) {
            args[argsContainerIndex].SetProperty(propertyName, value);
        }
    }
}