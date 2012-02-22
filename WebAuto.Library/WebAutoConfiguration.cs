using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Library
{
	public class WebAutoConfiguration
	{
		public string DataDirectory { get; set; }
		public string FileExtension { get; set; }
		public string UIMapFile { get; set; }
		public string ResultsFolder { get; set; }

		public string Browser { get; set; }
	}
}
