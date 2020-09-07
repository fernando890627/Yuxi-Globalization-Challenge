using CompanyWebPage.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebPage.Tests
{
    /// <summary>
    /// Custom factory for SensorApi
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> 
        where TStartup : class 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .BuildServiceProvider();

                services.AddTransient<IActionLogger, TestActionLogger>();
            });
        }
    }
}
