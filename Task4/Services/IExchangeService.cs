using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Services
{
    /// <summary>
    /// The service handling the API requests implemented by <see cref="ExchangeService"/>.
    /// </summary>
    internal interface IExchangeService
    {
        /// <summary>
        /// The async method to the API to fetch the list of available currencies.
        /// </summary>
        /// <returns>The Task operation that returns the list of <see cref="string"/></returns>
        public Task<List<string>> GetCurrencies();

        /// <summary>
        /// The async method to the api to perform an exchange of the latest currency records based on arguments provided.
        /// </summary>
        /// <param name="from">Represents the code of the base currency.</param>
        /// <param name="to">Represents the code of the currency to exchange to.</param>
        /// <param name="amount">The amount of currency to exchange of type <see cref="float"/></param>
        /// <returns>The Task operation to be awaited by the caller.</returns>
        public Task Exchange(string from, string to, float amount);

        /// <summary>
        /// The async method to perform an exchange at certain point in time based on arguments provided.
        /// </summary>
        /// <param name="from">Represents the code of the base currency.</param>
        /// <param name="to">Represents the code of the currency to exchange to.</param>
        /// <param name="amount">The amount of currency to exchange of type <see cref="float"/></param>
        /// <param name="date">The date to check the records of type <see cref="DateOnly"/></param>
        /// <returns>The Task operation to be awaited by the caller.</returns>
        public Task HistoricalExchange(string from, string to, float amount, DateOnly date);
    }
}
