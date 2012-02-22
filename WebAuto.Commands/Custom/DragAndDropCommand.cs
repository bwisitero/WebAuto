using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;


namespace WebAuto.Commands
{
    public class DragAndDropCommand : Command, ICommand 
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
                IWebElement[] elements2;
                try
                {
                    elements2 = new Utility().GetTargetElements(driver, Target);
                }
                catch (StaleElementReferenceException ex)
                {
                    //retrying
                    elements2 = new Utility().GetTargetElements(driver, Target);
                }

                if (elements.Length == 0 || elements2.Length == 0)
                    return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Error Command: {1} Cannot find targets.", "DragAndDrop") };
				if (elements.Length > 1 || elements2.Length > 1)
					return new CommandExecutionResult { CommandResult = CommandResult.ResultYieldedMoreThanOne, Message = string.Format("More than one element found for targets") };
				(new Actions(driver)).DragAndDrop(elements.First(), elements2.First()).Perform();
                return new CommandExecutionResult { CommandResult = CommandResult.Success, Message = string.Empty };
            }
            catch (TimeoutException ex)
            {
                return new CommandExecutionResult { CommandResult = CommandResult.TimedOut, Message = ex.Message + "\n" + ex.StackTrace };
            }
            catch (Exception ex)
            {
                return new CommandExecutionResult { CommandResult = CommandResult.Failed, Message = ex.Message + "\n" + ex.StackTrace };
            }
        }

        public string Text
        {
            get
            {
                string v = string.IsNullOrEmpty(Description)
                            ? string.Format("Dragging {0} To {1}", !string.IsNullOrEmpty(Target) ? Target : " Element1", !string.IsNullOrEmpty(Value) ? Value : "Element2")
                            : Description;

                return v;
            }
        }
    }
}
