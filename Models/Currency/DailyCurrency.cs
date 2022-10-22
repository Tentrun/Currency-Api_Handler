namespace Currency_Api_Handler.Models.Currency
{
    public class DailyCurrency
    {
        public ValutaCountryModel? Valute { get; set; }
        public DateTime? Date { get; set; }
        public string? PreviousUrl { get; set; }
    }
}
