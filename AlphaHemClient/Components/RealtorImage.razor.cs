using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
    //Author: Dominika
{
    public partial class RealtorImage
    {
        [Parameter] public string ImageUrl { get; set; }
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        private string GetImageUrl()
        {
            return string.IsNullOrWhiteSpace(ImageUrl)
                ? "https://i.imgur.com/KXP6Z5L.png" : ImageUrl;
        }
    }
}
