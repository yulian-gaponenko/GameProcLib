using GameGenLib.GameEntities;

namespace GameGenLib.Logics {
    internal class IfElse : ILogic {
        private readonly IPropertyAccessor equalsLeft;
        private readonly IPropertyAccessor equalsRight;

        private readonly PropertySetter thenSetter;
        private readonly PropertySetter elseSetter;

        public IfElse(IPropertyAccessor equalsLeft, IPropertyAccessor equalsRight, PropertySetter thenSetter, PropertySetter elseSetter) {
            this.equalsLeft = equalsLeft;
            this.equalsRight = equalsRight;
            this.thenSetter = thenSetter;
            this.elseSetter = elseSetter;
        }

        public void Execute(params IPropertyContainer[] args) {
            int equalsValueLeft = equalsLeft.GetProperty(args);
            int equalsValueRight = equalsRight.GetProperty(args);
            if (equalsValueLeft == equalsValueRight) {
                thenSetter.Execute(args);
            } else {
                elseSetter.Execute(args);
            }
        }
    }
}
