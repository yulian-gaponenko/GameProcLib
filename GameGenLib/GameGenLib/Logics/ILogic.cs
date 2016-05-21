using GameGenLib.GameEntities;

namespace GameGenLib.Logics {
    public interface ILogic {
        void Execute(params IPropertyContainer[] args);
    }
}