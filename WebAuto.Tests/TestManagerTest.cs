using System.Configuration;
using NUnit.Framework;
using WebAuto.Core;
using WebAuto.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WebAuto.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuto.Tests
{
	[TestClass()]	
	public class TestManagerTest
	{
		[TestMethod()]
		[Test]
		public void Execute()
		{
			var browsers = ConfigurationManager.AppSettings["browsers"].Split(',');
			foreach (var browser in browsers)
			{

				ExcelRepository target = new ExcelRepository();
				TestManager manager = new TestManager(target);
				string masterfile = Path.IsPathRooted(ConfigurationManager.AppSettings["masterfile"]) ? ConfigurationManager.AppSettings["masterfile"] :
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["masterfile"]);
				string sequenceFilename = Path.IsPathRooted(ConfigurationManager.AppSettings["sequencefile"]) ? ConfigurationManager.AppSettings["sequencefile"] :
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["sequencefile"]);
				string uimapFilename = Path.IsPathRooted(ConfigurationManager.AppSettings["uimapfile"]) ? ConfigurationManager.AppSettings["uimapfile"] :
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["uimapfile"]);
				string dataDirectory = Path.IsPathRooted(ConfigurationManager.AppSettings["datadirectory"]) ? ConfigurationManager.AppSettings["datadirectory"] :
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["datadirectory"]);
				string resultsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Results");
				var config = new WebAutoConfiguration();
				config.DataDirectory = dataDirectory;
				config.UIMapFile = uimapFilename;
				config.ResultsFolder = resultsFolder;
				config.Browser = browser.Trim();
				config.FileExtension = ".xlsx";

				manager.Execute(target.GetTestSuites(masterfile, sequenceFilename), config);
			}
		}
	}
}
