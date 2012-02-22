using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    [Serializable]
    public class UICommandContainer
    {
        public List<UICommand> Commands { get; set; }
        public string Name { get; set; }
        public string Screenshot { get; set; }
        public List<string> ErrorScreenshots { get; set; }
        public int ActualPasses { get; set; }
        public int NumberOfPasses { get; set; }
        public CommandExecutionResult Result { get; set; }
        public UICommandContainer(string name, UICommand[] commands)
        {
            Name = name;
            Commands = new List<UICommand>();
            Result = new CommandExecutionResult { CommandResult = CommandResult.NotRun, Message = string.Empty };
            ErrorScreenshots = new List<string>();
            Commands.AddRange(commands);
        }

        public UICommandContainer()
        {
            ErrorScreenshots = new List<string>();
            Commands = new List<UICommand>();
            Result = new CommandExecutionResult { CommandResult = CommandResult.NotRun, Message = string.Empty };
        }


    }
}
