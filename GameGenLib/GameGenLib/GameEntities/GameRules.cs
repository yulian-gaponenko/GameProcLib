using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class GameRules {
        public GameRules(ILogic nextMoveEvent, IDictionary<int, int> playerWinProperty) {
            NextMoveEvent = nextMoveEvent;
            PlayerWinProperty = playerWinProperty;
        }

        public ILogic NextMoveEvent { get; }
        public IDictionary<int, int> PlayerWinProperty { get; }
    }
}