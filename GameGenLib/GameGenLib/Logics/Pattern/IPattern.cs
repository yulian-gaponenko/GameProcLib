using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Pattern {
    public interface IPattern {
        ICells Find(Cell cell, params GameElement[] parameters);
    }
}