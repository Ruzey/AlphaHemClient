using Microsoft.AspNetCore.Components;
using AlphaHemClient.Model.ViewModel;

namespace AlphaHemClient.Components
{
    // Author: Christoffer
    public partial class MunicipalitySelector
    {
        [Parameter] public List<MunicipalityViewModel> Municipalities { get; set; } = new List<MunicipalityViewModel>();
        [Parameter] public EventCallback<string?> OnMunicipalitySelected { get; set; }

        private async Task OnMunicipalityChanged(ChangeEventArgs e)
        {
            var selected = e.Value?.ToString();

            if (string.IsNullOrWhiteSpace(selected))
            {
                await OnMunicipalitySelected.InvokeAsync(null);
            }
            else
            {
                await OnMunicipalitySelected.InvokeAsync(selected);
            }

        }
    }
}
