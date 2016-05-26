using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class GameField : GameElement {
        private Cell[] FieldCells { get; }

        public GameField(int size) {
            FieldCells = new Cell[size];
            Size = size;
            for (int i = 0; i < size; ++i) {
                FieldCells[i] = new Cell(i % size, i / size, this);
            }
        }

        public int Size { get; }

        public Cell GetCell(int x, int y) {
            return FieldCells[y * Size + x];
        }
        public override string ContainerType => "Field";
    }
}
