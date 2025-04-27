using Microsoft.AspNetCore.Components;
using AlphaHemClient.Model.ComponentModels;

namespace AlphaHemClient.Components
{
    // Author: Christoffer
    public partial class SortSelector
    {
        [Parameter] public List<SortOption> SortOptions { get; set; } = new List<SortOption>();
        [Parameter] public EventCallback<string> OnSortSelected { get; set; }

        private async Task OnSortChanged(ChangeEventArgs e)
        {
            var selectedSort = e.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(selectedSort))
            {
                await OnSortSelected.InvokeAsync(selectedSort);
            }
        }
    }
}
