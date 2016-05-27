namespace GameGenLib.GameEntities {
    public class FigureHolder : IPropertyContainer {
        public GameFigure Figure { get; set; }
        public int GetProperty(int propName) {
            return Figure.GetProperty(propName);
        }

        public void SetProperty(int propName, int propValue) {
            Figure.SetProperty(propName, propValue);
        }

        public string ContainerType { get { return "Figure"; } }
    }
}