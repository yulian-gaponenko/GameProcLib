using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics.Pattern {
    internal interface IPattern {
        bool Find(CellsSequences cellsSequences, IPropertyContainer[] args);

        ShiftDirection NextCellDir { get; }
    }
}