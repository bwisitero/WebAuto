using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;
using OpenQA.Selenium;

namespace WebAuto.Commands
{
	public class ClickCommand : Command, ICommand
	{
		public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
			try
			{
				if (!string.IsNullOrEmpty(Value))
				{
					Target = Target + "|text=" + Value;
				}
				IWebElement[] elements = new IWebElement[0];
				try
				{
					elements = new Utility().GetTargetElements(driver, Target);
				}
				catch (StaleElementReferenceException ex)
				{
					//retrying
					System.Diagnostics.Debug.WriteLine("Retrying..");
					elements = new Utility().GetTargetElements(driver, Target);
				}
				finally
				{
					if (elements.Length == 1)
					{
						try
						{
							elements.First().Click();
						}
						catch (ElementNotVisibleException ex)
						{
							elements = new Utility(0).GetTargetElements(driver, Target);
							elements.First().Click();
						}

					}
				}
				if (elements.Length > 1)
					return new CommandExecutionResult { CommandResult = CommandResult.ResultYieldedMoreThanOne, Message = string.Format("More than one element found for target:{0} value:{1}", Target, Value) };
				if (elements.Length == 0)
					return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Cannot find target:{0} value:{1}", Target, Value) };

				return new CommandExecutionResult { CommandResult = CommandResult.Success, Message = string.Empty };
			}
			catch (TimeoutException ex)
			{
				return new CommandExecutionResult { CommandResult = CommandResult.TimedOut, Message = ex.Message };
			}
			catch (Exception ex)
			{
				return new CommandExecutionResult { CommandResult = CommandResult.Failed, Message = ex.Message };
			}

		}

		public string Text
		{
			get
			{
				string v = string.IsNullOrEmpty(Description)
							? string.Format("{0} {1}", "Click", !string.IsNullOrEmpty(Value) ? Value : "Element")
							: Description;

				return v;
			}
		}


	}
}
