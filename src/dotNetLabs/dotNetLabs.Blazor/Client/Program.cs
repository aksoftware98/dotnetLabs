using Blazored.LocalStorage;
using BlazorFluentUI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AKSoftware.Localization.MultiLanguages;
using System.Reflection;
using MudBlazor.Services;

namespace dotNetLabs.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Initailzing the HttpClient 
            builder.Services.AddHttpClient("dotNetLabs.Api", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            }).AddHttpMessageHandler<AuthorizationMessageHandler>();
            builder.Services.AddTransient(sp => sp.GetService<IHttpClientFactory>().CreateClient("dotNetLabs.Api"));
            builder.Services.AddTransient<AuthorizationMessageHandler>();

            builder.Services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>(); 

            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly()); 

            await builder.Build().RunAsync();
        }
    }
}
