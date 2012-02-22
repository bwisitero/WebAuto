using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using System.Xml.Serialization;

namespace WebAuto.Core
{
    [DelimitedRecord("|")]
    public class UICommand
    {
        [XmlAttribute]
        public string Enabled;
        [XmlAttribute]
        public string CommandName;
        [XmlAttribute]
        public string Target;
        [XmlAttribute]
        public string Value;
        [XmlAttribute]
        public string Description;
        [XmlElement]
        public string Result;
        public UICommand()
        {
            Result = "NotRun";
        }
    }
}
