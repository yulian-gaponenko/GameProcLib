using GameGenLib.GameEntities;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    internal class PropertySetter : ILogic {
        private readonly IPropertyAccessor propertySetter;
        private readonly IPropertyAccessor valueGetter;

        public PropertySetter(ArgumentPropertyAccessor propertySetter, ArgumentPropertyAccessor valueGetter) {
            this.valueGetter = valueGetter;
            this.propertySetter = propertySetter;
        }

        public void Execute(params IPropertyContainer[] args) {
            propertySetter.SetProperty(valueGetter.GetProperty(args), args);
        }
    }
}
