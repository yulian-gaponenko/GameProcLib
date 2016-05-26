using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Cells {
    public interface ICells {
        CellsSequences ToCellsSequences();
        CellsSet ToCellsSet();
        ICells AddNextCell(Cell nextCell);
        int Size { get; }
    }
}