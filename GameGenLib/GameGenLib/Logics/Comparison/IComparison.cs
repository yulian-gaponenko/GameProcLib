using GameGenLib.GameEntities;
using GameGenLib.Logics.PropertyAccessers;

namespace GameGenLib.Logics.Comparison {
    internal interface IComparison {
        IPropertyAccessor EqualsLeft { get; }
        IPropertyAccessor EqualsRight { get; }

        bool Compare(IPropertyContainer[] args);
    }
}