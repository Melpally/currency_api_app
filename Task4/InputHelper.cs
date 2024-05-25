using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Task4.Services;

namespace Task4
{
    /// <summary>
    /// The helper class to perform checks of the user input.
    /// </summary>
    internal class InputHelper
    {
        public static string GetCurrencyCodeFromUser(List<string> codes, string type)
        {
            string input;
            bool isValid;
            do
            {
                Console.Write($"Please enter the {type} currency (the 3 letter code): ");
                input = Console.ReadLine().Trim().ToUpper();
                isValid = codes.Contains(input);
            }
            while (input == string.Empty || !isValid);
            return input;
        }

        public static float GetAmountFromUser()
        {
            string input;
            float amount;
            bool isNum;
            do
            {
                Console.Write("Please enter the amount to convert: ");
                input = Console.ReadLine().Trim();
                isNum = float.TryParse(input, out amount);
            }
            while (input == string.Empty || !isNum);
            return amount;
        }

        public static DateOnly GetDateFromUser()
        {
            string input;
            DateOnly date;
            bool isDate;
            do
            {
                Console.Write("Please enter the date (yyyy-mm-dd): ");
                input = Console.ReadLine().Trim();
                isDate = DateOnly.TryParse(input, out date);
            }
            while (input == string.Empty || !isDate);
            return date;
        }
    }
}
