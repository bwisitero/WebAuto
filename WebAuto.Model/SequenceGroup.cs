using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{

    public class SequenceGroup
    {
        public SequenceGroup()
        {
            Sequences = new SerializableDictionary<string, UICommandContainer>();
        }

        public SerializableDictionary<string, UICommandContainer> Sequences { get; set; }

    }
    
}
