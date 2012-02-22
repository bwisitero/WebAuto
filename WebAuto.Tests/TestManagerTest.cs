using System.Configuration;
using NUnit.Framework;
using WebAuto.Core;
using WebAuto.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WebAuto.Library;

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
				string masterfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//master.xlsx");
				string sequenceFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//sequences.xlsx");
				string uimapFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//uimap.xlsx");
				string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//Data");
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
