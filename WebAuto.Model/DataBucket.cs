using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    public class DataBucket
    {
        public Dictionary<string, Dictionary<string, string>> DataValues { get; set; }
        public Dictionary<string, UIData> DataTables { get; set; }
        public DataBucket()
        {
            DataValues = new Dictionary<string, Dictionary<string, string>>();
            DataTables = new Dictionary<string, UIData>();
        }
    }
}
