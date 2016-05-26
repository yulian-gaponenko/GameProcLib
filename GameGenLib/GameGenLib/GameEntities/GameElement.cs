using System.Collections.Generic;

namespace GameGenLib.GameEntities {
    public abstract class GameElement : IPropertyContainer {
        private readonly IDictionary<int, int> propertites = new Dictionary<int, int>();

        public bool HasProperty(int propName) {
            return propertites.ContainsKey(propName);
        }

        public int GetProperty(int propName) {
            return propertites[propName];
        }

        public void SetProperty(int propName, int propValue) {
            propertites[propName] = propValue;
        }

        public abstract string ContainerType { get; }
    }
}
