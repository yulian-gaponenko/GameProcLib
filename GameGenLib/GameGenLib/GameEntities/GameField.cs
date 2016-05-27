using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class GameField : GameElement {
        private Cell[] FieldCells { get; }

        public GameField(int size) {
            int wholeSize = size * size;
            FieldCells = new Cell[wholeSize];
            Size = size;
            for (int i = 0; i < wholeSize; ++i) {
                FieldCells[i] = new Cell(i % size, i / size, this);
            }
        }

        public int Size { get; }

        public Cell GetCell(int x, int y) {
            if (x >= Size || y >= Size || x < 0 || y < 0) {
                return null;
            }
            return FieldCells[y * Size + x];
        }
        public override string ContainerType => "Field";
    }
}
