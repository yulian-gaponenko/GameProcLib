using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class GameField : GameElement {
        public SeparateCells FieldCells { get; }

        public GameField(int size) {
            FieldCells = new SeparateCells(new List<Cell>(size));
            Size = size;
            for (int i = 0; i < size; ++i) {
                FieldCells.Cells.Add(new Cell());;
            }
        }

        public int Size { get; }
    }
}
