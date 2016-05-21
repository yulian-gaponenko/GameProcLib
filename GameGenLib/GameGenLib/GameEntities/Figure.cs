namespace GameGenLib.GameEntities {
    public class Figure : GameElement {
        public Figure(int name, FigureType type) {
            Name = name;
            Type = type;
        }

        public int Name { get; }
        public FigureType Type { get; }
    }
}
