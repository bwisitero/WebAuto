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
    public class StoreTextCommand : Command, ICommand
    {
        public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
        {
            try
            {              
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
                if (elements.Length > 1)
                    return new CommandExecutionResult { CommandResult = CommandResult.ResultYieldedMoreThanOne, Message = string.Format("More than one element found for target:{0} value:{1}", Target, Value) };
                if (elements.Length == 0)
                    return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Cannot find target:{0} value:{1}", Target, Value) };
                
                string text = elements.First().Text; 
                if (RuntimeValues.ContainsKey(Value))
                {
                    RuntimeValues[Value] = text;
                }
                else
                {
                    RuntimeValues.Add(Value, text);
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
                            ? string.Format("Storing Text into variable :{0} ", !string.IsNullOrEmpty(Value) ? Value : "Variable")
                            : Description;

                return v;
            }
        }
    }
}
