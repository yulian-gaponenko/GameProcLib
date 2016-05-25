using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class FigureType {
        public FigureType(ILogic possibleMovesLogic, ILogic moveActionLogic) {
            PossibleMovesLogic = possibleMovesLogic;
            MoveActionLogic = moveActionLogic;
        }

        public ILogic PossibleMovesLogic { get; }
        public ILogic MoveActionLogic { get; }
    }
}
