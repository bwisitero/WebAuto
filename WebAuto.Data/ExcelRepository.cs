using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Interfaces;
using System.IO;
using WebAuto.Core;
using FileHelpers.DataLink;

namespace WebAuto.Data
{
    public class ExcelRepository : IRepository
    {
        public Dictionary<string, string> GetUIMap(string filename)
        {
            var result = new Dictionary<string, string>();
            var dataProvider = new ExcelStorage(typeof(UIMappingValueRaw));
            dataProvider.StartRow = 2;
            dataProvider.StartColumn = 1;
            dataProvider.FileName = filename;
            foreach (var sheetName in Utility.GetExcelSheetNames(filename))
            {
                dataProvider.SheetName = sheetName;
                var d1 = (UIMappingValueRaw[])dataProvider.ExtractRecords();
                foreach (var uiMappingValueRaw in d1)
                {
                    string key = dataProvider.SheetName.ToLower() + "|" + uiMappingValueRaw.Key.ToLower();
                    if (!result.ContainsKey(key))
                        result.Add(key, uiMappingValueRaw.Value);
                }
            }
            return result;
        }

        public DataBucket GetData(string filename) {
            var dataBucket = new DataBucket();
            if (!string.IsNullOrEmpty(filename))
            {
                foreach (var sheetName in Utility.GetExcelSheetNames(filename))
                {


					if (IsDataTable(filename, sheetName))
                    {
                        var dataProvider = new ExcelStorage(typeof(UIDataRaw));
                        dataProvider.FileName = filename;
                        dataProvider.StartRow = 1;
                        dataProvider.StartColumn = 1;
                        dataProvider.SheetName = sheetName;
                        var d1 = new UIData(dataProvider.SheetName, (UIDataRaw[])dataProvider.ExtractRecords());
                        if (!dataBucket.DataTables.ContainsKey(d1.DataName.ToLower()))
                        {
                            dataBucket.DataTables.Add(d1.DataName.ToLower(), d1);
                        }
                    }
                    else
                    {
                        var dataProvider = new ExcelStorage(typeof(UIMappingValueRaw));
                        dataProvider.SheetName = sheetName;
                        dataProvider.FileName = filename;
                        dataProvider.StartRow = 2;
                        dataProvider.StartColumn = 1;
                        dataBucket.DataValues.Add(dataProvider.SheetName.ToLower(), new Dictionary<string, string>());
                        var d1 = (UIMappingValueRaw[])dataProvider.ExtractRecords();
                        foreach (var uiMappingValueRaw in d1)
                        {
                            string key = uiMappingValueRaw.Key.ToLower().TrimEnd();
                            if (!dataBucket.DataValues[dataProvider.SheetName.ToLower()].ContainsKey(key))
                                dataBucket.DataValues[dataProvider.SheetName.ToLower()].Add(key, uiMappingValueRaw.Value);
                        }
                    }
                }
            }
            return dataBucket;
        }

		private bool IsDataTable(string filename, string sheetname)
		{
			var dataProvider = new ExcelStorage(typeof(UIMappingValueRaw));
			dataProvider.FileName = filename;
			dataProvider.StartRow = 1;
			dataProvider.StartColumn = 1;
			dataProvider.SheetName = sheetname;
			var d1 = (UIMappingValueRaw[]) dataProvider.ExtractRecords();
			return (d1[0].Key != "Key");
		}

        public SequenceGroup GetCommandSequences(string filename, string[] sequences)
        {

            var sg = new SequenceGroup();
            ExcelStorage dataProvider = new ExcelStorage(typeof(UICommand));
            dataProvider.StartRow = 2;
            dataProvider.StartColumn = 1;
            dataProvider.FileName = filename;

			foreach (var sheetName in sequences)
            {
                dataProvider.SheetName = sheetName;
                var commands = (UICommand[])dataProvider.ExtractRecords();
                sg.Sequences.Add(sheetName, new UICommandContainer(sheetName, commands.Where(x=>x.Enabled.ToLower()=="y" || x.Enabled.ToLower()=="yes").ToArray()));
            }

            return sg;
        }

		public string[] GetSequencesFromMaster(string filename)
		{
			var dataProvider = new ExcelStorage(typeof(UIDataRaw));
			dataProvider.FileName = filename;
			dataProvider.StartRow = 1;
			dataProvider.StartColumn = 1;
			var testSequences = new List<string>();
			foreach (var sheetName in Utility.GetExcelSheetNames(filename))
			{
				dataProvider.SheetName = sheetName;
				var data = (UIDataRaw[])dataProvider.ExtractRecords();
				var props = data.FirstOrDefault().GetType().GetProperties();
				for (int i = 0; i < data.Length; i++)
				{
					//get the list of test sequences
					for (int x = 0; x < props.Count(); x++)
					{
						object val = data[i].GetType().GetProperty(props[x].Name).GetValue(data[i], null);
						if (i == 0 && x > 1)
						{
							if (val != null)
							{
								testSequences.Add(val.ToString());
							}
						}
					}
				}
			}
			return testSequences.ToArray();
		}

		public UITestSuite[] GetTestSuites(string filename, string filename2) {
		    List<UITestSuite> testSuites = new List<UITestSuite>();
		    var dataProvider = new ExcelStorage(typeof(UIDataRaw));
		    dataProvider.FileName = filename;
		    dataProvider.StartRow = 2;
		    dataProvider.StartColumn = 1;

			var sequenceList = new List<UICommandContainer>();
			var list = GetSequencesFromMaster(filename);
			var sequences = GetCommandSequences(filename2, list);
			foreach (var sequence in sequences.Sequences)
			{
				sequenceList.Add(sequence.Value);
			}

		    foreach (var sheetName in Utility.GetExcelSheetNames(filename))
		    {
		        dataProvider.SheetName = sheetName;
		        var data = (UIDataRaw[])dataProvider.ExtractRecords();
				var props = data.FirstOrDefault().GetType().GetProperties();
				var orderedSequences = new SerializableDictionary<int, UICommandContainer>();
				UITestSuite testSuite = null;
				for (int i = 0; i < data.Length; i++)
				{
					TestCase testCase = null;
					for (int x = 0; x < props.Count(); x++)
					{
						object val = data[i].GetType().GetProperty(props[x].Name).GetValue(data[i], null);
						if (x==0 && val != null )
						{
							if (Utility.IsAllUpper(val.ToString()))
							{
								// add the last test case before creating new test suite and test case
								if (testCase != null && !string.IsNullOrEmpty(testCase.GroupName))
								{
									testSuite.Add(testCase.GroupName, testCase);
									testCase = new TestCase();
								}

								// add the previous test suite before creating a new one
								if (testSuite != null) testSuites.Add(testSuite);
								// get the suite name								
								testSuite = new UITestSuite();
								testSuite.Name = val.ToString();
								break;
							}

							object testcaseEnabled = data[i].GetType().GetProperty(props[x+1].Name).GetValue(data[i], null);
							if (testcaseEnabled == null || ( testcaseEnabled.ToString().ToLower() != "y" && testcaseEnabled.ToString().ToLower() != "yes"))
								continue; // this testcase is NOT enabled, so we skip processing

							// get the testcase name
							testCase = new TestCase();
							testCase.GroupName = val.ToString();
						}
						if (x>1 && val != null && testCase!=null)
						{
							int sequencePosition = -1;
							if (int.TryParse(val.ToString(), out sequencePosition))
							{
								// get the ordered sequences for the test case
								var seq = sequenceList[x-(dataProvider.StartColumn+1)];
								//testCase.CommandGroups.Sequences.Add(seq.Name,new UICommandContainer(seq.Name, seq.Commands.ToArray()));
								orderedSequences.Add(sequencePosition, seq);
							}
						}
					}
					// add testcase after constructed.  Skip test suites
					if (testCase!=null && !string.IsNullOrEmpty(testCase.GroupName)) {

						foreach (var uiCommandContainer in orderedSequences.OrderBy(x=>x.Key))
						{
							testCase.CommandGroups.Sequences.Add(uiCommandContainer.Value.Name, uiCommandContainer.Value);
						}
						orderedSequences = new SerializableDictionary<int, UICommandContainer>();
					    testSuite.Add(testCase.GroupName, testCase);
					    testCase = new TestCase();
					}
				}
				// add the last test suite 
				if (testSuite != null&&testSuite.Count>0) testSuites.Add(testSuite);
		    }
		    return testSuites.ToArray();
		}


    }
}
