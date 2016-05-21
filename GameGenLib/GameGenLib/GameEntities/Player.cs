using System.Collections.Generic;

namespace GameGenLib.GameEntities {
    public class Player : GameElement {
        public Player(int name, IList<Figure> playerFigures) {
            Name = name;
            PlayerFigures = playerFigures;
        }

        public int Name { get; }
        public IList<Figure> PlayerFigures { get; }
    }
}
