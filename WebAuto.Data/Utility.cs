using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAuto.Data
{
	internal delegate bool WaitFunction(IWebDriver _driver, string attributes);
    public class Utility
    {

        public static string[] GetExcelSheetNames(string excelFile)
        {
			if (!File.Exists(excelFile)) throw new FileNotFoundException("Excel file cannot be found. " + excelFile);
            OleDbConnection objConn = null;

            System.Data.DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you

                // will search.

                //string connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                //                    "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                   "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";

                // Create connection object by using the preceding connection string.

                objConn = new OleDbConnection(connString);
                // Open connection with the database.

                objConn.Open();
                // Get the data table containg the schema guid.

                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                if (dt == null)
                {
                    return null;
                }

                string[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {
					excelSheets[i] = row["TABLE_NAME"].ToString().Remove(row["TABLE_NAME"].ToString().Length - 1).Replace("'", string.Empty).Replace("$", string.Empty);
                    i++;
                }

                for (int j = 0; j < excelSheets.Length; j++)
                {
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }

                if (dt != null)
                {
                    dt.Dispose();
                }

            }
        }

        public static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]) && !Char.IsNumber(input[i]))
				return false;
            }

            return true;
        }

    	public void ResizeTest(IWebDriver driver)
    	{
			((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(1024, 800);");
    	}

		public static string UppercaseFirst(string s)
		{
			// Check for empty string.
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			// Return char and concat substring.
			return char.ToUpper(s[0]) + s.Substring(1);
		}

		int maxTry = 3;
		public Utility()
		{

		}

		public Utility(int _maxTry)
		{
			maxTry = _maxTry;
		}

		public object ExecuteScript(IWebDriver _driver, string target, string value)
		{
			return ((IJavaScriptExecutor)_driver).ExecuteScript(target);
		}

		public IWebElement[] GetTargetElements(IWebDriver _driver, string attributes)
		{
			List<IWebElement> elements = new List<IWebElement>();
			ISearchContext searchContext = _driver;

			string[] navigationKeyWords = new string[] { "goto" };

			for (int i = 0; i < maxTry; i++)
			{
				var elementAttributes = attributes.Split('|');
				for (int index = 0; index < elementAttributes.Length; index++)
				{
					var attribute = elementAttributes[index];
					string key = attribute.Split('=')[0];
					string value = attribute.Split('=')[1];

					if (elements.Count == 0)
					{
						if (navigationKeyWords.Contains(key))
							throw new Exception("Goto failed.  Need an element to navigate from.");
						if (searchContext == null)
							throw new Exception("SearchContxt is null.");
						elements.AddRange(SearchElements(searchContext, key, value));
					}
					else if (elements.Count == 1)
					{
						searchContext = elements[0];

						if (navigationKeyWords.Contains(key))
						{
							switch (value)
							{
								case "parent":
									elements.Clear();
									elements.Add(searchContext.FindElement(By.XPath("..")));
									break;
								default:
									break;
							}
						}
						else
						{
							var elementsToFilter = new List<IWebElement>(elements.ToArray());
							elements.Clear();
							var searchKeys = new string[] { "tag", "link", "partial_link" };
							elements.AddRange(searchKeys.Contains(key.ToLower())
												? SearchElements(searchContext, key, value)
												: FilterElements(elementsToFilter, key, value));
						}
					}
					else // greater than 1
					{
						if (navigationKeyWords.Contains(key))
							throw new Exception("Goto failed.  Need only 1 element to navigate from.");

						var elementsToFilter = new List<IWebElement>(elements.ToArray());
						elements.Clear();
						elements.AddRange(FilterElements(elementsToFilter, key, value));
					}
				}
				if (elements.Count > 0)
					break;
				if (maxTry > 0 && elements.Count == 0)
					Thread.Sleep(1000);
			}
			return elements.ToArray();
		}

		private IWebElement[] FilterElements(List<IWebElement> elements, string key, string value)
		{
			List<IWebElement> nonMatchingElements = new List<IWebElement>();
			for (int index = 0; index < elements.Count; index++)
			{
				var webElement = elements[index];
				switch (key.ToLower())
				{
					case "class":
						if (!webElement.GetAttribute("class").Contains(value)) nonMatchingElements.Add(webElement);
						break;
					case "text":
						if (webElement.Text != value) nonMatchingElements.Add(webElement);
						break;
					case "enabled":
						if (webElement.Enabled != bool.Parse(value)) nonMatchingElements.Add(webElement);
						break;
					case "selected":
						if (webElement.Selected != bool.Parse(value)) nonMatchingElements.Add(webElement);
						break;
					case "displayed":
						if (webElement.Displayed != bool.Parse(value)) nonMatchingElements.Add(webElement);
						break;
					case "index":
						int ctr = 0;
						if (value.ToLower() == "last" && index < elements.Count - 1)
						{
							nonMatchingElements.Add(webElement);
						}
						else if (value.ToLower() == "first" && index != 0)
						{
							nonMatchingElements.Add(webElement);
						}
						else if (int.TryParse(value, out ctr))
						{
							if (index != ctr - 1)
							{
								nonMatchingElements.Add(webElement);
							}
						}
						break;
					default:
						if (webElement.GetAttribute(key) != value) nonMatchingElements.Add(webElement);
						break;
				}
			}
			foreach (var nonMatchingElement in nonMatchingElements)
			{
				elements.Remove(nonMatchingElement);
			}
			return elements.ToArray();
		}

		private IWebElement[] SearchElements(ISearchContext searchContext, string key, string value)
		{
			List<IWebElement> elements = new List<IWebElement>();
			switch (key)
			{
				case "id":
					elements.Add(searchContext.FindElement(By.Id(value)));
					break;
				case "tag":
					elements.AddRange(searchContext.FindElements(By.TagName(value)).ToArray());
					break;
				case "class":
					elements.AddRange(searchContext.FindElements(By.ClassName(value)).ToArray());
					break;
				case "xpath":
					//elements.AddRange(searchContext.FindElements(By.XPath(value)).ToArray());
					elements.Add(searchContext.FindElement(By.XPath(value)));
					break;
				case "partial_link":
					elements.AddRange(searchContext.FindElements(By.PartialLinkText(value)).ToArray());
					break;
				case "link":
					elements.AddRange(searchContext.FindElements(By.LinkText(value)).ToArray());
					break;
				case "name":
					elements.AddRange(searchContext.FindElements(By.Name(value)).ToArray());
					break;
			}
			return elements.ToArray();
		}

		public IWebElement[] WaitForElement(IWebDriver _driver, string attributes, int timeout)
		{
			//Thread.Sleep(4000);
			//IWait<IWebDriver> wait = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5));
			//var elements = 

			//    wait.Until(driver => GetTargetElements(driver, attributes)); 
			List<IWebElement> elements = new List<IWebElement>();
			int interval = 1;
			int current = 0;
			bool first = true;
			while (elements.Count == 0 && current <= timeout)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					Thread.Sleep(interval * 1000);
				}
				try
				{
					elements.AddRange(GetTargetElements(_driver, attributes));
				}
				catch (WebDriverException ex)
				{
					//_driver.Navigate().Refresh();
				}
				current += interval;
				Console.WriteLine("Trying to find again.");
			}
			return elements.ToArray();
		}

		public bool WaitingForElement(IWebDriver _driver, string attributes, int timeout)
		{
			//Thread.Sleep(4000);
			IWait<IWebDriver> wait = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(4));
			bool found = wait.Until(driver => GetTargetElements(driver, attributes).Length > 0);
			return found;
		}

		public bool WaitingForElementNotPresent(IWebDriver _driver, string attributes, int timeout)
		{
			//Thread.Sleep(4000);
			IWait<IWebDriver> wait = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(4));
			return wait.Until(driver => GetTargetElements(driver, attributes).Length == 0);
		}

    }
}
