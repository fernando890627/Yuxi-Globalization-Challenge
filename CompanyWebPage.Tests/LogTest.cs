using CompanyWebPage.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebPage.Tests
{
    [TestClass]
    public class LogTest : BaseTest
    {
        [TestMethod]
        public void CheckIsHomeIndexLogged()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                lock (TestActionLogger.History)
                {
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    TestActionLogger.History.Clear();
                    var result = client.SendAsync(PrepareGetRequest($"/about", CultureEnglish));
                    result.Wait();
                    result.Result.EnsureSuccessStatusCode();

                    Assert.AreEqual(1, TestActionLogger.History.Count);
                    Assert.AreEqual("Index", TestActionLogger.History[0].ActionName);
                    Assert.IsTrue(TestActionLogger.History[0].ControllerName.Equals("Home") || TestActionLogger.History[0].ControllerName.Equals("HomeController"));
                    Assert.AreEqual(0, TestActionLogger.History[0].ActionParameters?.Count ?? 0);
                }
            }
        }

        [TestMethod]
        public void CheckIsHomeSubscribeLogged()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                lock (TestActionLogger.History)
                {
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    TestActionLogger.History.Clear();
                    var request = PreparePostRequest($"/newsletter", CultureEnglish, new Dictionary<string, string> { { "Age", "10" }, { "EmailAddress", "test@test.com" }, { "FirstName", "John" } });
                    var result = client.SendAsync(request);
                    result.Wait();
                    result.Result.EnsureSuccessStatusCode();

                    Assert.AreEqual(2, TestActionLogger.History.Count); // because we call /newsletter and if all data are correct the /about is called
                    Assert.AreEqual("Subscribe", TestActionLogger.History[0].ActionName);
                    Assert.AreEqual("Index", TestActionLogger.History[1].ActionName);
                    Assert.IsTrue(TestActionLogger.History[0].ControllerName.Equals("Newsletter") || TestActionLogger.History[0].ControllerName.Equals("NewsletterController"));
                    Assert.IsTrue(TestActionLogger.History[1].ControllerName.Equals("Home") || TestActionLogger.History[0].ControllerName.Equals("HomeController"));

                    Assert.AreEqual(1, TestActionLogger.History[0].ActionParameters?.Count ?? 0);
                    Assert.AreEqual(0, TestActionLogger.History[1].ActionParameters?.Count ?? 0);

                    Assert.IsTrue(TestActionLogger.History[0].ActionParameters.Any(a => a.ParameterName == "model" && a.ParameterType == typeof(ViewModel.NewsleterSubscribeViewModel)));
                    var model = TestActionLogger.History[0].ActionParameters[0].ParameterValue as ViewModel.NewsleterSubscribeViewModel;
                    Assert.AreEqual(10, model.Age);
                    Assert.AreEqual("test@test.com", model.EmailAddress);
                    Assert.AreEqual("John", model.FirstName);                   
                }
            }
        }
    }
}
