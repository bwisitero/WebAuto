using WebAuto.Core;
using WebAuto.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WebAuto.Library;

namespace WebAuto.Tests
{
    
    
    /// <summary>
    ///This is a test class for ExcelRepositoryTest and is intended
    ///to contain all ExcelRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExcelRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetUIMap
        ///</summary>
        [TestMethod()]
        public void GetUIMap()
        {
            ExcelRepository target = new ExcelRepository(); // TODO: Initialize to an appropriate value
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//uimap.xlsx");
            Assert.IsTrue(File.Exists(filename), "file doesnt exist " + filename);
            var result = target.GetUIMap(filename);
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod()]
        public void GetData()
        {
            ExcelRepository target = new ExcelRepository(); // TODO: Initialize to an appropriate value
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//datasample.xlsx");
            Assert.IsTrue(File.Exists(filename), "file doesnt exist " + filename);
            var result = target.GetData(filename);
            Assert.AreNotEqual(result.DataTables.Count, 0);
            Assert.AreNotEqual(result.DataValues.Count, 0);
        }

        [TestMethod()]
        public void GetSequences()
        {
            ExcelRepository target = new ExcelRepository(); // TODO: Initialize to an appropriate value
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//sequences.xlsx");
            Assert.IsTrue(File.Exists(filename), "file doesnt exist " + filename);
            var result = target.GetCommandSequences(filename, new string[]{ });
            Assert.AreNotEqual(result.Sequences.Count,0);
        }

        [TestMethod()]
		public void GetSequencesFromMaster()
        {
            ExcelRepository target = new ExcelRepository(); // TODO: Initialize to an appropriate value
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//master.xlsx");
            Assert.IsTrue(File.Exists(filename), "file doesnt exist " + filename);
			var result = target.GetSequencesFromMaster(filename);
            Assert.AreNotEqual(result.Length, 0);
        }

		[TestMethod()]
		public void GetTestSuites()
		{
			ExcelRepository target = new ExcelRepository(); // TODO: Initialize to an appropriate value
			string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//master.xlsx");
			string filename2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..//..//TestFiles//sequences.xlsx");
			Assert.IsTrue(File.Exists(filename), "file doesnt exist " + filename);
			var result = target.GetTestSuites(filename, filename2);
			Assert.AreNotEqual(result.Length, 0);
		}

		
    }
}
