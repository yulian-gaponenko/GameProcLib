using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics.Cells;

namespace GameGenLib.Logics {
    internal class AddCells : ILogic {
        private readonly CellsCollectionHolder cHolder;
        private readonly GameField field;
        private readonly IList<PropertyConstraint> constraints;

        public AddCells(CellsCollectionHolder cells, GameField field) {
            this.cHolder = cells;
            this.field = field;
            constraints = new List<PropertyConstraint>();
        }

        public void AddConstraint(PropertyConstraint pc) {
            constraints.Add(pc);
        }

        public void Execute(params IPropertyContainer[] args) {
            int size = field.Size;
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j) {
                    Cell c = field.GetCell(i, j);
                    bool isOk = true;
                    foreach (PropertyConstraint constraint in constraints) {
                        isOk = isOk && constraint.Check(c, args);
                    }
                    if (isOk) {
                        cHolder.Cells.AddNextCell(c);
                    }
                }
            }
        }

        public IList<string> ArgsTypes { get; set; }
    }
}