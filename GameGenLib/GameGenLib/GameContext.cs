using System.Collections.Generic;
using GameGenLib.GameEntities;

namespace GameGenLib {
    public class GameContext {

        public GameContext(GameField field, IList<Player> players) {
            Field = field;
            Players = players;
        }

        public GameField Field { get; }
        public IList<Player> Players { get; }


    }
}
