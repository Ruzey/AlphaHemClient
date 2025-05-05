using AlphaHemClient.Providers;
using AlphaHemClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AlphaHemClient
{
    //Author : ALL
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri("https://localhost:7109/") 
            });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AgencyService>(); //Author: Mattias

            // Author: Christoffer
            builder.Services.AddScoped<ListingService>();
            builder.Services.AddScoped<MunicipalityService>();
            builder.Services.AddScoped<JsLoggingService>();

            builder.Services.AddScoped<AuthService>(); // Author: ALL
            builder.Services.AddScoped<AlphaApiAuthenticationStateProvider>(); // Author: ALL
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AlphaApiAuthenticationStateProvider>()); // Author: ALL
            builder.Services.AddAuthorizationCore();
            // Author: Smilla
            builder.Services.AddScoped<RealtorService>();

            await builder.Build().RunAsync();
        }
    }
}
