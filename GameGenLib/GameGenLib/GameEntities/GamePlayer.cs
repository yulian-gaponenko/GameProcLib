using System.Collections.Generic;

namespace GameGenLib.GameEntities {
    public class GamePlayer : GameElement {
        public GamePlayer(int name, IList<GameFigure> playerFigures) {
            Name = name;
            PlayerFigures = playerFigures;
        }

        public int Name { get; }
        public IList<GameFigure> PlayerFigures { get; }
        public override string ContainerType => "Player";
    }
}
