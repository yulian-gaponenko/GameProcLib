using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics;

namespace GameGenLib {
    public class GameContext {

        public GameContext(GameField field, IList<Player> players, GameRules gameRules) {
            Field = field;
            Players = players;
            GameRules = gameRules;
        }

        public GameField Field { get; }

        public IList<Player> Players { get; }
        public GameRules GameRules { get; set; }
        public Player CurrPlayer { get; set; }
        public Player CurrFigure { get; set; }
    }
}
