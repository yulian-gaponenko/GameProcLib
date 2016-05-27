namespace GameGenLib.GameEntities {
    public class PlayerHolder : IPropertyContainer {
        public GamePlayer Player { get; set; }
        public int GetProperty(int propName) {
            return Player.GetProperty(propName);
        }

        public void SetProperty(int propName, int propValue) {
            Player.SetProperty(propName, propValue);
        }

        public string ContainerType {
            get { return "Player"; }
        }
    }
}