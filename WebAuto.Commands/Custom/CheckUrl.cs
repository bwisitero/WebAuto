using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;
using WebAuto.Interfaces;
using OpenQA.Selenium;

namespace WebAuto.Commands
{
	public class CheckUrlCommand : Command, ICommand
	{
		public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
			try
			{
				if (driver.Url.ToLower() != Target.ToLower())
				{
					return new CommandExecutionResult { CommandResult = CommandResult.Failed, Message = "Url not the same - expecting " + Target.ToLower() + " but was " + driver.Url.ToLower() };
				}
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
							? string.Format("Checking url if correct {0} ", !string.IsNullOrEmpty(Value) ? Value : "URL")
							: Description;

				return v;
			}
		}
	}
}