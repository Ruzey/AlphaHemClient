namespace AlphaHemClient.HelperClasses
{
    public static class DecimalExtensions
    {
        public static string ToPriceString(this decimal value)
        {
            return value.ToString("N0").Replace(",", " ") + " kr";
        }
    }
}
