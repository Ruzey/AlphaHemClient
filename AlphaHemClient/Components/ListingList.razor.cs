using AlphaHemClient.Model.DTO;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    @* Author: Christoffer *@
    public partial class ListingList
    {
        [Parameter] public List<ListingListDto> Listings { get; set; }
    }
}
