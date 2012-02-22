using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenQA.Selenium;
using WebAuto.Core;
using WebAuto.Interfaces;

namespace WebAuto.Interfaces
{
	public class Command
	{
		public Command()
		{
			Result = new CommandExecutionResult { CommandResult = CommandResult.NotRun };
		}

		public Command(string commandName, string target, string value, string description,
					   Dictionary<string, string> runtimeValues)
			: base()
		{
			CommandName = commandName;
			Target = target;
			Value = value;
			Description = description;

		}

		public string CommandName { get; set; }
		public string Target { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }

		[XmlIgnore]
		public Dictionary<string, string> RuntimeValues { get; set; }

		public CommandExecutionResult Result { get; set; }
	}
}
