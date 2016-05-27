namespace GameGenLib.GameEntities {
    public class GameFigure : GameElement {
        public GameFigure(FigureType type) {
            Type = type;
        }

        public FigureType Type { get; }
        public override string ContainerType => "Figure";
    }
}
