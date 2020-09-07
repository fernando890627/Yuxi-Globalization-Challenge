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
    public class SubscribePageTest : BaseTest
    {
        private string SubscribeCharacteristicEglishContent = "<h1>Subscribe to newsletter</h1>";
        private string SubscribeCharacteristicSpanishContent = "<h1>Suscríbase al boletín";

        [TestMethod]
        public async Task CheckSubscribeUrlEnglishCultureInHeaderTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter", CultureEnglish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SubscribeCharacteristicEglishContent));
            }
        }

        [TestMethod]
        public async Task CheckSubscribeUrlGermanCultureInHeaderTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter", CultureGerman));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SubscribeCharacteristicEglishContent)); // for german culture should return english page
            }
        }

        [TestMethod]
        public async Task CheckSubscribeUrlEnglishCultureInUrlTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest($"/newsletter?culture={CultureEnglish}", ""));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SubscribeCharacteristicEglishContent));
            }
        }

        [TestMethod]
        public async Task CheckSubscribeUrlSpanishCultureInHeaderTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest("/newsletter", CultureSpanish));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SubscribeCharacteristicSpanishContent));
            }
        }

        [TestMethod]
        public async Task CheckSubscribeUrlSpanishCultureInUrlTest()
        {
            using (var factory = new CustomWebApplicationFactory<CompanyWebPage.Web.Startup>())
            using (var client = factory.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var result = await client.SendAsync(PrepareGetRequest($"/newsletter?culture={CultureSpanish}", ""));
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains(SubscribeCharacteristicSpanishContent));
            }
        }
    }
}
