using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    public partial class EditPen
    {
        [Parameter]
        public int ListingId { get; set; }

        [Parameter]
        public string Class { get; set; }

        private void NavigateToEdit()
        {
            NavigationManager.NavigateTo($"/editlisting/{ListingId}");
        }

    }
}
