using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    public enum CommandResult
    {
        Success,
        CannotFindElement,
        VerifyFailed,
        TimedOut,
        Failed,
        NotRun,
        ResultYieldedMoreThanOne
    }

    public class CommandExecutionResult
    {
        public CommandResult CommandResult { get; set; }
        public string Message { get; set; }
    }
}
