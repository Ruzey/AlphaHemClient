using AlphaHemClient.Services;
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

            // Author: Christoffer
            builder.Services.AddScoped<ListingService>();
            builder.Services.AddScoped<MunicipalityService>();

            await builder.Build().RunAsync();
        }
    }
}
