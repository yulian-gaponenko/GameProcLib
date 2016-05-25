using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Pattern {
    internal interface IPattern {
        bool Find(CellSequences cellSequences, IPropertyContainer[] args);

        ShiftDirection NextCellDir { get; }
    }
}