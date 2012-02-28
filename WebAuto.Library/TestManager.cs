using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;

namespace WebAuto.Library
{
	public class TestManager
	{
		public IRepository Repository { get; set; }
		public TestManager(IRepository repository)
		{
			Repository = repository;
		}

		public CommandExecutionResult Execute(UITestSuite[] testSuites, WebAutoConfiguration configuration)
		{
			if (!Directory.Exists(configuration.ResultsFolder))
				Directory.CreateDirectory(configuration.ResultsFolder);

			CommandExecutionResult result = new CommandExecutionResult { CommandResult = CommandResult.Success, Message = string.Empty };
			Assembly assembly = Assembly.LoadFrom("WebAuto.Commands.dll");

			var uiMap = Repository.GetUIMap(configuration.UIMapFile);
			foreach (var testSuite in testSuites)
			{
				Console.WriteLine(testSuite.Name);
				foreach (var testcase in testSuite)
				{
					Console.WriteLine(string.Format(" {0}", testcase.Value.GroupName));
					var dataBucket = Repository.GetData(Path.Combine(configuration.DataDirectory, testcase.Value.GroupName + configuration.FileExtension));

					IWebDriver driver = null;
					switch (configuration.Browser)
					{
						case "firefox":
							driver = new FirefoxDriver();
							break;
						case "chrome":
							driver = new ChromeDriver();
							break;
						case "ie":
							driver = new InternetExplorerDriver();
							break;
						case "htmlunit":
							driver = new RemoteWebDriver(DesiredCapabilities.HtmlUnitWithJavaScript());
							break;
						default:
							driver = new RemoteWebDriver(DesiredCapabilities.HtmlUnit());
							break;
					}
					driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 20));
					new Utility().ResizeTest(driver);

					var usedData = new Dictionary<string, int>();
					foreach (var sequence in testcase.Value.CommandGroups.Sequences)
					{
						var commandContainer = new UICommandContainer();
						commandContainer.Name = sequence.Value.Name;

						Console.WriteLine(string.Format("  {0}", sequence.Value.Name));

						if (!usedData.ContainsKey(sequence.Value.Name))
						{
							usedData.Add(sequence.Value.Name, 1);
						}
						else
						{
							usedData[sequence.Value.Name]++;
						}

						var dataName = usedData[sequence.Value.Name] > 1
						               	? string.Format("{0}({1})", sequence.Value.Name.ToLower(), usedData[sequence.Value.Name])
						               	: sequence.Value.Name.ToLower();

						// check if sequence uses a list table
						if (dataBucket.DataTables.ContainsKey(dataName))
						{
							var table = dataBucket.DataTables[dataName];
							foreach (var dataValue in table)
							{
								foreach (var command in sequence.Value.Commands)
								{
									var c = new UICommand()
									{
										CommandName = command.CommandName,
										Description = command.Description ?? string.Empty,
										Target = command.Target ?? string.Empty,
										Value = command.Value ?? string.Empty
									};
									var cmd = PrepareCommand(c, dataValue, uiMap);
									Console.WriteLine(string.Format("   {0} {1} {2}",
										cmd.CommandName,
										cmd.Target,
										cmd.Value));

									string className = "WebAuto.Commands." + Utility.UppercaseFirst(cmd.CommandName) + "Command";
									Type t = assembly.GetType(className);
									var cmd2 = (WebAuto.Interfaces.Command)Activator.CreateInstance(t);
									cmd2.CommandName = cmd.CommandName;
									cmd2.Description = cmd.Description;
									cmd2.Target = cmd.Target;
									cmd2.Value = cmd.Value;

									((ICommand) cmd2).Execute(driver);
								}
							}
						}
						else
						{
							foreach (var command in sequence.Value.Commands)
							{
								var c = new UICommand()
								{
									CommandName = command.CommandName,
									Description = command.Description ?? string.Empty,
									Target = command.Target ?? string.Empty,
									Value = command.Value ?? string.Empty
								};
								var cmd = PrepareCommand(command, dataBucket.DataValues[dataName], uiMap);
								Console.WriteLine(string.Format("   {0} {1} {2}",
										cmd.CommandName,
										cmd.Target,
										cmd.Value));

								string className = "WebAuto.Commands." + Utility.UppercaseFirst(cmd.CommandName) + "Command";
								Type t = assembly.GetType(className);
								var cmd2 = (WebAuto.Interfaces.Command)Activator.CreateInstance(t);
								cmd2.CommandName = cmd.CommandName;
								cmd2.Description = cmd.Description;
								cmd2.Target = cmd.Target;
								cmd2.Value = cmd.Value;

								((ICommand)cmd2).Execute(driver);
							}
						}
					}
					driver.Close();
				}
			}
			return result;
		}

		private UICommand PrepareCommand(UICommand cmd, Dictionary<string, string> uid, Dictionary<string, string> uimap)
		{
			bool needsRandomNumber = false;
			if (!string.IsNullOrEmpty(cmd.CommandName))
			{
				cmd.CommandName = cmd.CommandName.TrimEnd();
			}
			if (!string.IsNullOrEmpty(cmd.Value) && cmd.Value.Contains("$randomNumber"))
			{
				cmd.Value = cmd.Value.Replace("$randomNumber", "");
				needsRandomNumber = true;
			}
			if (!string.IsNullOrEmpty(cmd.Target) && cmd.Target.StartsWith("$map(") &&
				cmd.Target.Trim().EndsWith(")"))
			{
				string t = cmd.Target.Trim().ToLower();
				t = t.Replace("$map(", "");
				t = t.Remove(t.Length - 1);
				try
				{
					cmd.Target = uimap[t];
				}
				catch (Exception)
				{
					System.Diagnostics.Debug.WriteLine(string.Format("Key does not exist : {0}", t));
					throw;
				}
			}
			if (!string.IsNullOrEmpty(cmd.Value) && cmd.Value.StartsWith("$map(") &&
				cmd.Value.Trim().EndsWith(")"))
			{
				string t = cmd.Value.Trim().ToLower();
				t = t.Replace("$map(", "");
				t = t.Remove(t.Length - 1);
				try
				{
					cmd.Value = uimap[t];
				}
				catch (Exception)
				{
					System.Diagnostics.Debug.WriteLine(string.Format("Key does not exist : {0}", t));
					throw;
				}
			}
			if (!string.IsNullOrEmpty(cmd.Target) && cmd.Target.StartsWith("$data(") &&
				cmd.Target.Trim().EndsWith(")"))
			{
				string t = cmd.Target.Trim().ToLower();
				t = t.Replace("$data(", "");
				t = t.Remove(t.Length - 1);
				cmd.Target = uid.ContainsKey(t) ? uid[t] : string.Empty;
			}
			if (!string.IsNullOrEmpty(cmd.Value) && cmd.Value.StartsWith("$data(") &&
				cmd.Value.Trim().EndsWith(")"))
			{
				string t = cmd.Value.Trim().ToLower();
				t = t.Replace("$data(", "");
				t = t.Remove(t.Length - 1);
				cmd.Value = uid.ContainsKey(t) ? uid[t] : string.Empty;
			}

			if (!string.IsNullOrEmpty(cmd.Target))
			{
				//used for evaluating the embedded $data() values inside targets
				string pattern = @"\$data\((\w+)\)";
				var matches = Regex.Matches(cmd.Target, pattern, RegexOptions.IgnoreCase);
				foreach (var match in matches)
				{
					string key = match.ToString().Replace("$data(", "").Replace(")", "");
					cmd.Target = cmd.Target.Replace(match.ToString(), uid[key]);
				}
			}

			if (needsRandomNumber)
			{
				cmd.Value = cmd.Value + "$randomNumber";
				needsRandomNumber = false;
			}
			return cmd;
		}
	}
}
