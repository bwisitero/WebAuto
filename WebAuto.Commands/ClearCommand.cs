using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;

namespace WebAuto.Commands
{
	public class ClearCommand : Command, ICommand
	{
        public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
		{
            try
            {
                IWebElement[] elements = new IWebElement[0];
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
                        try
                        {
                            elements = new Utility().GetTargetElements(driver, Target);
                            elements.First().Clear();
                        }
                        catch (ElementNotVisibleException ex)
                        {
                            //retrying
                            elements = new Utility().GetTargetElements(driver, Target);
                            elements.First().Clear();
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
							? string.Format("Clear {0}", !string.IsNullOrEmpty(Value) ? Value : "Text")
							: Description;

				return v;
			}
		}
	}
}
