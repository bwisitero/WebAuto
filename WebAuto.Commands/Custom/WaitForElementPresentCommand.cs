using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;
using OpenQA.Selenium;
using System.Threading;

namespace WebAuto.Commands
{
	public class WaitForElementPresentCommand : Command, ICommand 
	{
		public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
			try
			{
				int timeout = 10;
				bool isFound = false;
				if (!string.IsNullOrEmpty(Value))
				{
					Target = Target + "|text=" + Value;
				}
				IWebElement[] elements;
				try
				{					
					isFound= new Utility().WaitingForElement(driver, Target, timeout);
				}
				catch (StaleElementReferenceException ex)
				{
					//retrying
					isFound = new Utility().WaitingForElement(driver, Target, timeout);
				}
				if (!isFound)
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
							? string.Format("Wait if {0} is present", !string.IsNullOrEmpty(Value) ? Value : "Element")
							: Description;

				return v;
			}
		}
	}
}
