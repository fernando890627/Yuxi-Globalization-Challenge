using CompanyWebPage.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyWebPage.Tests
{
    public class TestActionLogger : IActionLogger
    {
        public class LogItem
        {
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
            public IList<ActionParameter> ActionParameters { get; set; }
        }

        public static IList<LogItem> History { get; private set; } = new List<LogItem>();

        public void Log(string controllerName, string actionName, IList<ActionParameter> actionParameters)
        {
            History.Add(new LogItem() { ActionName = actionName, ControllerName = controllerName, ActionParameters = actionParameters });
        }
    }
}
