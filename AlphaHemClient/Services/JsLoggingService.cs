using Microsoft.JSInterop;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class JsLoggingService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsLoggingService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task LogToConsole(string message)
        {
            await _jsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}
