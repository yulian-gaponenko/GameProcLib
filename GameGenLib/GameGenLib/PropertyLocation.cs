using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenLib {
    public class PropertyLocation {
        public PropertyLocation(string obj, string propName) {
            PropName = propName;
            Obj = obj;
        }

        public string Obj { get; private set; }
        public string PropName { get; private set; }
    }
}
