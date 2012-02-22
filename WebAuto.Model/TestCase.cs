using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WebAuto.Core
{

    [XmlInclude(typeof(SequenceGroup))]
    public class TestCase
    {
        public TestCase()
        {
            CommandGroups = new SequenceGroup();
        }

        public SequenceGroup CommandGroups { get; set; }
        public string GroupName { get; set; }
        public CommandExecutionResult Result { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeEnded { get; set; }
    }

}
