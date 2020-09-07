using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebPage.Tests
{
    [TestClass]
    public class ErrorPageTest : BaseTest
    {
        private string ErrorPageSomeContext = "<h1>There is an error on the page or a page not exists!</h1>";

        [TestMethod]
        public async Task CheckErrorUrlTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest($"/error", CultureEnglish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(ErrorPageSomeContext));             
            }
        }

        [TestMethod]
        public async Task CheckErrorWhenNotExistingAbout123UrlTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest($"/about123", CultureEnglish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(ErrorPageSomeContext));
            }
        }
    }
}
