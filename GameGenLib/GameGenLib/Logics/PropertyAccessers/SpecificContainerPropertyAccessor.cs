using GameGenLib.GameEntities;

namespace GameGenLib.Logics.PropertyAccessers {
    internal class SpecificContainerPropertyAccessor : IPropertyAccessor {
        private readonly int propertyName;
        private readonly IPropertyContainer globalContainer;

        public SpecificContainerPropertyAccessor(int propertyName, IPropertyContainer globalContainer) {
            this.propertyName = propertyName;
            this.globalContainer = globalContainer;
        }

        public int GetProperty(IPropertyContainer[] args) {
            return globalContainer.GetProperty(propertyName);
        }

        public void SetProperty(int value, IPropertyContainer[] args) {
            globalContainer.SetProperty(propertyName, value);
        }
    }
}