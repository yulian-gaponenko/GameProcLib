using System.Collections;
using System.Collections.Generic;

namespace GameGenLib.GameEntities {
    public class Cell : GameElement {
        public Cell(int x, int y, GameField field) {
            Field = field;
            X = x;
            Y = y;
        }

        public GameField Field { get; }

        public int X { get; }
        public int Y { get; }


        public Cell NextCell(ShiftDirection nextCellDir) {
            int nextX = X;
            int nextY = Y;
            switch (nextCellDir) {
                case ShiftDirection.Left:
                    nextX -= 1;
                    break;
                case ShiftDirection.LeftUp:
                    nextX -= 1;
                    nextY -= 1;
                    break;
                case ShiftDirection.Up:
                    nextY -= 1;
                    break;
                case ShiftDirection.RightUp:
                    nextX += 1;
                    nextY -= 1;
                    break;
                case ShiftDirection.Right:
                    nextX += 1;
                    break;
                case ShiftDirection.RightDown:
                    nextX += 1;
                    nextY += 1;
                    break;
                case ShiftDirection.Down:
                    nextY += 1;
                    break;
                case ShiftDirection.LeftDown:
                    nextX -= 1;
                    nextY += 1;
                    break;
            }

            return Field.GetCell(nextX, nextY);
        }

        public override string ContainerType => "Cell";
    }
}
