using Currency_Api_Handler.Models.Currency;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Currency_Api_Handle.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClientHandler;
        private readonly string _requestUrl = "https://www.cbr-xml-daily.ru/daily_json.js";

        public CurrencyService()
        {
            _httpClientHandler = new HttpClient();
            _httpClientHandler.DefaultRequestHeaders.Accept.Clear();
            _httpClientHandler.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<ValutaModel>> GetDataFromRequestAsync()
        {
            var httpResponseMessage = await _httpClientHandler.GetAsync(_requestUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var _dailyCurrency = await JsonSerializer.DeserializeAsync<DailyCurrency>(stream);

                IEnumerable<ValutaModel?>? currencies = _dailyCurrency?.Valute?.GetType().GetProperties().Select(item => _dailyCurrency.Valute?
                    .GetType()?.GetProperty(item.Name)?.GetValue(_dailyCurrency.Valute, null) as ValutaModel);

                return currencies;
            }
            throw new ArgumentNullException(httpResponseMessage.ToString());
        }
    }
}
