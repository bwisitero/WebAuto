using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WebAuto.Core;
using WebAuto.Interfaces;
using OpenQA.Selenium;

namespace WebAuto.Commands
{
	public class WaitCommand : Command, ICommand
	{
		public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
			try
			{
				int waitTime = 0;
				bool result = int.TryParse(Value, out waitTime);
				Thread.Sleep(waitTime * 1000);
				return new CommandExecutionResult { CommandResult = CommandResult.Success };
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
							? string.Format("Wait for {0} seconds", !string.IsNullOrEmpty(Value) ? Value : "specified time")
							: Description;

				return v;
			}
		}
	}
}
