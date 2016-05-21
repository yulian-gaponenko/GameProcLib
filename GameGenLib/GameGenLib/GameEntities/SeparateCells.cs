using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class SeparateCells : ICells {
        public List<Cell> Cells { get; }
        public SeparateCells(List<Cell> cells) {
            Cells = cells;
        }

    }
}