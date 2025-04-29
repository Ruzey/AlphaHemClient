using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AlphaHemClient.Components
{
    // Author: Christoffer
    public partial class Pagination
    {
        [Parameter]
        public int CurrentPage { get; set; }
        [Parameter]
        public int TotalPages { get; set; }
        [Parameter]
        public EventCallback<int> OnPageSelected { get; set; }

        private async Task SelectPage(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                await OnPageSelected.InvokeAsync(page);
                await JSRuntime.InvokeVoidAsync("scrollToTop");
            }
        }
    }
}
