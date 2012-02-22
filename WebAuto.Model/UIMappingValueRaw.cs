using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace WebAuto.Core
{
    [DelimitedRecord("|")]
    public class UIMappingValueRaw
    {
        public string Key;
        public string Value;

        public UIMappingValueRaw()
        {
        }
    }
}
