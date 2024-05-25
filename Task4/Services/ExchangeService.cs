using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Task4.Services
{
    /// <inheritdoc/>
    internal class ExchangeService : IExchangeService
    {
        private const string BaseAddress = "https://api.freecurrencyapi.com/v1";

        private static readonly HttpClient client = new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(10)
        })
        {
            BaseAddress = new Uri(BaseAddress),
            DefaultRequestHeaders = { 
                { "accept", "application/json" },
                { "apikey", "fca_live_blNryNVKvNJczY9IuNjT6qwldtZ3vlnCLrHelZXQ"} },
            Timeout = TimeSpan.FromMinutes(2)
        };
        
        public async Task Exchange(string from, string to, float amount)
        {
           
            string url = $"{BaseAddress}/latest/?currencies={to}&base_currency={from}";

            var result = await Get(url);

            JsonDocument jsonDoc = JsonDocument.Parse(result);
            JsonElement data = jsonDoc.RootElement.GetProperty("data").GetProperty($"{to}");

            var rate = data.GetDouble();
            var conversion = rate * amount;

            Console.WriteLine($"\n{amount} {from} ~ {conversion.ToString("F2")} {to}");
        }

        public async Task<List<string>> GetCurrencies()
        {
            string url = $"{BaseAddress}/currencies?currencies";
            string result = await Get(url);
            List<string > list = new List<string>();

            JsonDocument jsonDoc = JsonDocument.Parse(result);

            JsonElement data = jsonDoc.RootElement.GetProperty("data");

            foreach (JsonProperty currency in data.EnumerateObject())
            {
                list.Add($"{currency.Name}");
            }

            return list;
        }
        
        public async Task HistoricalExchange(string from, string to, float amount, DateOnly date)
        {
            string url = $"{BaseAddress}/historical?currencies={to}&base_currency={from}&date={date}";

            var result = await Get(url);
            
            JsonDocument jsonDoc = JsonDocument.Parse(result);
            
            JsonElement data = jsonDoc.RootElement.GetProperty("data");

            JsonElement innerObj = data.EnumerateObject().First().Value;

            var rate = innerObj.GetProperty($"{to}").GetDouble();

            var conversion = rate * amount;

            Console.WriteLine($"\nAs per {date.ToString("yyyy-MM-dd")} {amount} {from} ~ {conversion.ToString("F2")} {to}");
        }

        public async Task<string> Get(string url)
        {

            var response = await client.GetAsync(url);

           
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();

            }

            return "Errors occurred.";


        }
    }
}
