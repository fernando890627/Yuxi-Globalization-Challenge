using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CompanyWebPage.Tests
{
    public class BaseTest
    {
        protected static string CultureSpanish = "es";
        protected static string CultureGerman = "de";
        protected static string CultureEnglish = "en";

        protected HttpRequestMessage PrepareGetRequest(string url, string culture)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(culture))
                request.Headers.Add("accept-language", culture);

            return request;
        }

        protected HttpRequestMessage PreparePostRequest(string url, string culture, Dictionary<string, string> parameters)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (!string.IsNullOrEmpty(culture))
                request.Headers.Add("accept-language", culture);

            request.Content = new FormUrlEncodedContent(parameters);
            return request;
        }
    }
}
