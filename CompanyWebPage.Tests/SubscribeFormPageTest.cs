using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebPage.Tests
{
    [TestClass]
    public class SubscribeFormPageTest : BaseTest
    {
        private string EglishLabelAge = "Your age";
        private string EglishLabelEmail = "Your e-mail";
        private string EglishLabelFirstName = "Your name";
        private string SpanishLabelAge = "Tu edad";
        private string SpanishLabelEmail = "Tu e-mail";
        private string SpanishLabelFirstName = "Tu nombre";

        private string EglishErrorAge = "Age is incorrect (1-99)";
        private string EglishErrorEmail = "You must provide a email";
        private string EglishErrorFirstName = "You must provide a name";
        private string SpanishErrorAge = "La edad es incorrecta (1-99)";
        private string SpanishErrorEmail = "Debes proporcionar un e-mail";
        private string SpanishErrorFirstName = "Debes proporcionar un nombre";

        [TestMethod]
        public async Task CheckSubscribeFormEnglishLabelsTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter", CultureEnglish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(EglishLabelAge));
                Assert.IsTrue(content.Contains(EglishLabelEmail));
                Assert.IsTrue(content.Contains(EglishLabelFirstName));
            }
        }

        /// <summary>
        /// For gernam should be english culture used.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckSubscribeFormGermanLabelsTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter/Subscribe", CultureGerman));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(EglishLabelAge));
                Assert.IsTrue(content.Contains(EglishLabelEmail));
                Assert.IsTrue(content.Contains(EglishLabelFirstName));
            }
        }


        [TestMethod]
        public async Task CheckSubscribeFormSpanishLabelsTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter", CultureSpanish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SpanishLabelAge));
                Assert.IsTrue(content.Contains(SpanishLabelEmail));
                Assert.IsTrue(content.Contains(SpanishLabelFirstName));
            }
        }

        [TestMethod]
        public async Task CheckSubscribeFormValidationEnglishTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var request = PreparePostRequest("/newsletter", CultureEnglish, new Dictionary<string, string> { { "Age", "0" }, { "EmailAddress", "" }, { "FirstName", "" } });
                var result = await client.SendAsync(request);

                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(EglishErrorAge));
                Assert.IsTrue(content.Contains(EglishErrorEmail));
                Assert.IsTrue(content.Contains(EglishErrorFirstName));
            }
        }

        [TestMethod]
        public async Task CheckSubscribeFormValidationSpanishTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var request = PreparePostRequest("/newsletter", CultureSpanish, new Dictionary<string, string> { { "Age", "0" }, { "EmailAddress", "" }, { "FirstName", "" } });
                var result = await client.SendAsync(request);

                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SpanishErrorAge));
                Assert.IsTrue(content.Contains(SpanishErrorEmail));
                Assert.IsTrue(content.Contains(SpanishErrorFirstName));
            }
        }
    }
}
