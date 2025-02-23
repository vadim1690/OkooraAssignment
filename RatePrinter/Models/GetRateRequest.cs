namespace RatePrinter.Models
{
    public class GetRateRequest
    {
        public required string BaseCurrencyCode { get; set; }
        public required string TargetCurrencyCode { get; set; }
    }
}
