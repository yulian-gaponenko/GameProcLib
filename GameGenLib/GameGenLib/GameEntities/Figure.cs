namespace GameGenLib.GameEntities {
    public class Figure : GameElement {
        public Figure(FigureType type) {
            Type = type;
        }

        public FigureType Type { get; }
        public override string ContainerType => "Figure";
    }
}
