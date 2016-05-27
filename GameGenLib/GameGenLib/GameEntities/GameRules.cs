using System.Collections.Generic;
using GameGenLib.Logics;

namespace GameGenLib.GameEntities {
    public class GameRules {
        public GameRules(ILogic initFieldLogic, ILogic nextMoveEvent, IDictionary<int, int> playerWinProperty) {
            InitFieldLogic = initFieldLogic;
            NextMoveEvent = nextMoveEvent;
            PlayerWinProperty = playerWinProperty;
        }

        public ILogic InitFieldLogic { get; set; }
        public ILogic NextMoveEvent { get; }
        public IDictionary<int, int> PlayerWinProperty { get; }
    }
}