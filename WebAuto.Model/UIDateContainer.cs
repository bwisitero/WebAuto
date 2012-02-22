using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    public class UIDataContainer : Dictionary<string, UICommandContainer>
    {
        public string Name { get; set; }
        public UIDataContainer(string name)
        {
            Name = name;
        }
    }
}
