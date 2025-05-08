using AlphaHemClient.Model.ViewModel;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    // Christoffer
    public partial class CategorySelector
    {
        [Parameter] public EventCallback<string?> OnCategorySelected { get; set; }

        private async Task OnCategoryChanged(ChangeEventArgs e)
        {
            var selected = e.Value?.ToString();

            if (string.IsNullOrWhiteSpace(selected))
            {
                await OnCategorySelected.InvokeAsync(null);
            }
            else
            {
                await OnCategorySelected.InvokeAsync(selected);
            }

        }
    }
}
