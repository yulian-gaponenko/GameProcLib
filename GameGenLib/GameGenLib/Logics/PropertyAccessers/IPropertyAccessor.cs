using GameGenLib.GameEntities;

namespace GameGenLib.Logics.PropertyAccessers {
    internal interface IPropertyAccessor {
        int GetProperty(IPropertyContainer[] args);
        void SetProperty(int value, IPropertyContainer[] args);
    }
}