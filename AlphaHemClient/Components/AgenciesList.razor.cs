using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    public partial class AgenciesList
    {
        private List<AgencyVM> AllAgenciesList = new();

        [Inject]
        public AgencyService AgencyService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        // Co-author: Conny
        protected override async Task OnInitializedAsync()
        {
            var response = await AgencyService.GetAllAgencies();
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            AllAgenciesList = response.Data;
        }
    }
}
