using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<TestAuthenticationStateProvider>();
            builder.Services.AddSingleton<AuthenticationStateProvider>(s =>
            {
                return s.GetRequiredService<TestAuthenticationStateProvider>();
            });

            await builder.Build().RunAsync();
        }
    }
}
