using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;
using OpenQA.Selenium;

namespace WebAuto.Commands
{
	public class TypeCommand : Command, ICommand
	{
		Random r = new Random();
		public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
			try
			{
				string v = Value.Contains("$randomNumber") ? Value.Replace("$randomNumber", r.Next(100000, 999999).ToString()) : Value;
				IWebElement[] elements;
				try
				{
					elements = new Utility().GetTargetElements(driver, Target);
				}
				catch (StaleElementReferenceException ex)
				{
					//retrying
					elements = new Utility().GetTargetElements(driver, Target);
				}

				if (elements.Length == 0)
					return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Cannot find target:{0} value:{1}", Target, Value) };
				if (elements.Length > 1)
					return new CommandExecutionResult { CommandResult = CommandResult.ResultYieldedMoreThanOne, Message = string.Format("More than one element found for target:{0} value:{1}", Target, Value) };

				elements.First().Clear();
				foreach (var ch in v.ToCharArray())
				{
					elements.First().SendKeys(ch.ToString());
					Thread.Sleep(100);
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
							? string.Format("Type {0}", !string.IsNullOrEmpty(Value) ? Value : "Text")
							: Description;

				return v;
			}
		}
	}
}