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
    public class WaitForElementNotPresentCommand : Command, ICommand
    {
        public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
        {
            try
            {
                int timeout = 10;
                bool notFound = false;
                if (!string.IsNullOrEmpty(Value))
                {
                    Target = Target + "|text=" + Value;
                }
                IWebElement[] elements;
                try
                {
                    notFound = new Utility(1).WaitingForElementNotPresent(driver, Target, timeout);
                }
                catch (StaleElementReferenceException ex)
                {
                    //retrying
                    notFound = new Utility(1).WaitingForElementNotPresent(driver, Target, timeout);
                }
                if (!notFound)
                    return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Target exist:{0} value:{1}", Target, Value) };

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
                            ? string.Format("Check if {0} is not present", !string.IsNullOrEmpty(Value) ? Value : "Element")
                            : Description;

                return v;
            }
        }
    }
}
