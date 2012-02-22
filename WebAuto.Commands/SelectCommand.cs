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
	public class SelectCommand : Command, ICommand
	{
        public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
            try
            {
                bool isFound = false;
                IWebElement[] elements = null;
                try
                {
                    elements = new Utility().GetTargetElements(driver, Target);
                }
                catch (StaleElementReferenceException ex)
                {
                    //retrying
                    elements = new Utility().GetTargetElements(driver, Target);
                }
               finally
                {
					if (elements.Length == 1)
					{
						var elements2 = elements.First().FindElements(By.TagName("option"));
                        foreach (IWebElement el in elements2)
						{
							if (el.Text == Value)
							{
								el.Click();
								isFound = true;
								break;
							}
						}
					}
                }
				if (elements.Length==0)
					return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Cannot find target:{0} value:{1}", Target, Value) };

				if (isFound)
					return new CommandExecutionResult { CommandResult = CommandResult.Success, Message = string.Empty };
				else 
					return new CommandExecutionResult { CommandResult = CommandResult.Failed, Message = string.Empty };

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
							? string.Format("Select {0}", !string.IsNullOrEmpty(Value) ? Value : "Text")
							: Description;

				return v;
			}
		}
	}
}