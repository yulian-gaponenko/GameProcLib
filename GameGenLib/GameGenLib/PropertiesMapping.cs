using System;
using System.Collections.Generic;

namespace GameGenLib {
    internal class PropertiesMapping {
        private int fieldPropertiesCount = 0;
        private int playerPropertiesCount = 0;
        private int cellPropertiesCount = 0;
        private int figurePropertiesCount = 0;
        private readonly IDictionary<string, int> propertiesMapping = new Dictionary<string, int>();

        public int GetFieldPropsCount() {
            return fieldPropertiesCount;;
        }
        public int GetPlayerPropsCount() {
            return playerPropertiesCount;;
        }
        public int GetCellPropsCount() {
            return cellPropertiesCount;;
        }
        public int GetFigurePropsCount() {
            return figurePropertiesCount;;
        }

        public int GetFieldPropertyIndex(string propertyName) {
            int propertyIndex;
            if (propertiesMapping.TryGetValue(propertyName, out propertyIndex)) {
                return propertyIndex;
            }
            propertiesMapping[propertyName] = fieldPropertiesCount;
            return fieldPropertiesCount++;
        }

        public int GetPlayerPropertyIndex(string propertyName) {
            int propertyIndex;
            if (propertiesMapping.TryGetValue(propertyName, out propertyIndex)) {
                return propertyIndex;
            }
            propertiesMapping[propertyName] = playerPropertiesCount;
            return playerPropertiesCount++;
        }

        public int GetCellPropertyIndex(string propertyName) {
            int propertyIndex;
            if (propertiesMapping.TryGetValue(propertyName, out propertyIndex)) {
                return propertyIndex;
            }
            propertiesMapping[propertyName] = cellPropertiesCount;
            return cellPropertiesCount++;
        }

        public int GetFigurePropertyIndex(string propertyName) {
            int propertyIndex;
            if (propertiesMapping.TryGetValue(propertyName, out propertyIndex)) {
                return propertyIndex;
            }
            propertiesMapping[propertyName] = figurePropertiesCount;
            return figurePropertiesCount++;
        }

        public int GetPropertyIndex(string propertyName, string type) {
            switch (type) {
                case "Field":
                    return GetFieldPropertyIndex(propertyName);
                case "Player":
                    return GetPlayerPropertyIndex(propertyName);
                case "Cell":
                    return GetCellPropertyIndex(propertyName);;
                case "Figure":
                    return GetFigurePropertyIndex(propertyName);
                default:
                    throw new Exception("Unknown property container type.");
            }
        }
    }
}
