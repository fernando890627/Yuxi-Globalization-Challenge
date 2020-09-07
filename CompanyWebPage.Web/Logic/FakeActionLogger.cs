using CompanyWebPage.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyWebPage.Web.Logic
{
    public class FakeActionLogger : IActionLogger
    {
        public void Log(string controllerName, string actionName, IList<ActionParameter> actionParameters)
        {
            // do nothing
        }
    }
}
