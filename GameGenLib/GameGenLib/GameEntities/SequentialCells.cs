using System.Collections;
using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class SequentialCells : ICells {
        public List<ICells> Sequences { get; }

        public SequentialCells(List<ICells> sequences) {
            Sequences = sequences;
        }
    }
}