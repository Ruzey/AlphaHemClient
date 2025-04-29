using AlphaHemClient.Model.DTO;
using Microsoft.AspNetCore.Components;
using AlphaHemClient.Model.ComponentModels;

namespace AlphaHemClient.Components
{
    // Author: Christoffer   
    public partial class ListingList
    {
        [Parameter] public List<ListingListDto> Listings { get; set; }

        private string FormatCategory(string categoryString)
        {
            return categoryString switch
            {
                "Bostadsrättsradhus" => "Bostadsrätts-radhus",
                "Bostadsrättslägenhet" => "Bostadsrätts-lägenhet",
                "Villa" => "Villa",
                "Fritidshus" => "Fritids-hus",
                _ => categoryString
            };
        }
    }


}


