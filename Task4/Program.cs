using Microsoft.Extensions.DependencyInjection;
using Task4.Services;

namespace Task4
{
    internal class Program
    {
        private readonly IExchangeService _exchangeService;
        public Program(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }
        static async Task Main(string[] args)
        {
            IExchangeService exchangeService = new ExchangeService();
            var program = new Program(exchangeService);

            await program.InvokeMenu();

            Console.ReadLine();
        }

        public async Task InvokeMenu()
        {
            var currencies = await _exchangeService.GetCurrencies();
            var text = "\nChoose the command:" +
                "\r\nCheck the historical amount of exchange based on date - h" +
                "\r\nExchange any amount of currency per latest rates - e" +
                "\r\nGet the list of available currencies - c" +
                "\r\nExit - x";
            while (true)
            {
                Console.WriteLine(text);
                var command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "h":
                        var date = InputHelper.GetDateFromUser();
                        var amount = InputHelper.GetAmountFromUser();
                        var baseCurrency = InputHelper.GetCurrencyCodeFromUser(currencies, "base");
                        var foreignCurrency = InputHelper.GetCurrencyCodeFromUser(currencies, "foreign");
                        await _exchangeService.HistoricalExchange(baseCurrency, foreignCurrency, amount, date);
                        break;
                    case "e":
                        var xAmount = InputHelper.GetAmountFromUser();
                        var xBaseCurrency = InputHelper.GetCurrencyCodeFromUser(currencies,"base");
                        var xForeignCurrency = InputHelper.GetCurrencyCodeFromUser(currencies, "foreign");
                        await _exchangeService.Exchange(xBaseCurrency, xForeignCurrency, xAmount);
                        break;
                    case "c":
                        
                        Console.WriteLine("The list of available currencies:");
                        var index = 0;
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (index < currencies.Count)
                                {
                                    Console.Write($"{currencies[index]}   ");
                                    index++;
                                }

                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                }

            }
        }
        
    }
}
