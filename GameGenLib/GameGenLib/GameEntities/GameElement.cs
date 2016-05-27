using System.Collections.Generic;

namespace GameGenLib.GameEntities {
    public abstract class GameElement : IPropertyContainer {
        private int[] properties;

        public int GetProperty(int propName) {
            return properties[propName];
        }

        public void SetProperty(int propName, int propValue) {
            properties[propName] = propValue;
        }

        public abstract string ContainerType { get; }

        public void InitPropertiesForType(int size) {
            properties = new int[size];
        }
    }
}
