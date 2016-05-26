using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenLib.GameEntities {
    public interface IPropertyContainer {
        int GetProperty(int propName);
        void SetProperty(int propName, int propValue);
        string ContainerType { get; }
    }
}
