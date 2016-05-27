using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics;

namespace GameGenLib {
    public class GameContext {

        public GameContext(GameField field, IList<GamePlayer> players, GameRules gameRules) {
            Field = field;
            Players = players;
            GameRules = gameRules;
            CurrPlayer = new PlayerHolder();
            CurrFigure = new FigureHolder();
        }

        public GameField Field { get; }

        public IList<GamePlayer> Players { get; }
        public GameRules GameRules { get; set; }
        public PlayerHolder CurrPlayer { get; set; }
        public FigureHolder CurrFigure { get; set; }
    }
}
