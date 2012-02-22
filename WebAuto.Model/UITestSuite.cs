using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    public class UITestSuite : Dictionary<string, TestCase>
    {
        public string Name { get; set; }
	}
}
