using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;
using WebAuto.Data;
using WebAuto.Interfaces;


namespace WebAuto.Commands
{
    public class RunScriptCommand : Command, ICommand
    {
        public CommandExecutionResult Execute(OpenQA.Selenium.IWebDriver driver)
        {
            try
            {
                var  o = new Utility().ExecuteScript(driver, Target, Value);
                if (!string.IsNullOrEmpty(Value))
                {
                    if (!Value.Equals(o.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new CommandExecutionResult { CommandResult = CommandResult.CannotFindElement, Message = string.Format("Cannot find target:{0} value:{1}", Target, Value) };
                    }
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
							? string.Format("Run script {0}", !string.IsNullOrEmpty(Target) ? Value : "")
							: Description;

				return v;
			}
		}
    }
}
