using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib.Logics.Pattern {
    public class PatternOr : IPattern {
        public PatternOr(List<IPattern> childPatterns) {
            ChildPatterns = childPatterns;
        }

        public List<IPattern> ChildPatterns { get; }
        public ICells Find(Cell cell, params GameElement[] parameters) {
            List<ICells> results = new List<ICells>();
            foreach (var childPattern in ChildPatterns) {
                var result = childPattern.Find(cell, parameters);
                if (result != null) {
                    results.Add(result);
                }
            }
            return results.Count > 0 ? new SequentialCells(results) : null;
        }
    }
}