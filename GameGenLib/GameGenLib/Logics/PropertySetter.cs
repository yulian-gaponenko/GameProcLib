using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics {
    internal class PropertySetter : ILogic {
        private readonly IPropertyAccessor propertySetter;
        private readonly IPropertyAccessor valueGetter;

        public PropertySetter(IPropertyAccessor propertySetter, IPropertyAccessor valueGetter) {
            this.valueGetter = valueGetter;
            this.propertySetter = propertySetter;
        }

        public void Execute(params IPropertyContainer[] args) {
            propertySetter.SetProperty(valueGetter.GetProperty(args), args);
        }

        public IList<string> ArgsTypes { get; set; }
    }
}
