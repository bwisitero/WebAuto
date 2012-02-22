using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebAuto.Core;

namespace WebAuto.Interfaces
{

    public interface ICommand
    {
        CommandExecutionResult Execute(IWebDriver driver);
        string Text { get; }
        CommandExecutionResult Result { get; set; }
    }
}
