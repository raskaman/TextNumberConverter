using MyApp.ServiceContracts.Services;
using System;
using System.Text.RegularExpressions;

namespace MyApp.Services
{
    public class NumberConverterService : INumberConverterService
    {
        public string DecimalToWords(decimal number)
        {
            return DecimalToWords(number).ToLower();
        }

        /// <summary>
        /// takes a decimal and return the english currency representaion
        /// </summary>
        /// <param name="value">a decimal value</param>
        /// <returns></returns>
        public string DecimalNumberToWords(decimal value)
        {
            string decimals = "";
            string stringDecimal = Math.Round(value, 2).ToString("#0.00");

            if (stringDecimal.Contains("."))
            {
                decimals = stringDecimal.Substring(stringDecimal.IndexOf(".") + 1);

                // remove decimal 
                stringDecimal = stringDecimal.Remove(stringDecimal.IndexOf("."));
            }

            string numberWords = string.Empty;

            // if has dollars
            if (value > 1)
            {
                numberWords = GetWords(stringDecimal);
                if (Regex.IsMatch(numberWords, @"\s+$"))
                {
                    numberWords = numberWords.TrimEnd();
                }
                numberWords += " Dollars";
            }
                
            // if decimals are more than zero
            if (decimals.Length > 0 && int.Parse(decimals) > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                if (value > 1)
                {
                    numberWords += " and " + GetWords(decimals) + " Cents";
                }
                else
                {
                    numberWords += GetWords(decimals) + " Cents";
                }
            }

            return numberWords;
        }

        /// <summary>
        /// get the words for the whole number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetWords(string input)
        {
            // these are seperators for each 3 digit in numbers.
            string[] seperators = { "", " Thousand, ", " Million, ", " Billion, " };

            // Counter is indexer for seperators. each 3 digit converted this will count.
            int i = 0;

            string strWords = "";

            while (input.Length > 0)
            {
                // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
                string _triplet = input.Length < 3 ? input : input.Substring(input.Length - 3);
                // remove the 3 last digits from input. if there is not 3 numbers just remove it.
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int no = int.Parse(_triplet);
                // Convert 3 digit number into words.
                _triplet = GetTripletWord(no);

                // apply the seperator.
                if (!string.IsNullOrEmpty(_triplet))
                {
                    _triplet = _triplet.TrimEnd() + seperators[i];
                }
                // since we are getting numbers from right to left then we must append resault to strWords like this.
                strWords = _triplet + strWords;

                // 3 digits converted. count and go for next 3 digits
                i++;
            }
            return strWords;
        }

        /// <summary>
        /// convert triplet number into words
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>word representaion</returns>
        private string GetTripletWord(int number)
        {
            string[] Ones =
            {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"
        };

            string[] Tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

            string word = "";

            bool hasHundreds = false;
            if (number > 99 && number < 1000)
            {
                int i = number / 100;
                word = word + Ones[i - 1] + " Hundred ";
                number = number % 100;
                hasHundreds = true;
            }

            bool hasTens = false;

            if (number > 19 && number < 100)
            {
                int i = number / 10;
                word = word + Tens[i - 1] + "*";
                number = number % 10;
                hasTens = true;
            }

            bool hasOnes = false;
            if (number > 0 && number < 20)
            {
                word = word + Ones[number - 1];
                hasOnes = true;
            }

            if(hasHundreds && hasTens)
            {
                word = word.Replace(" Hundred ", " Hundred and ");
            }

            if (hasTens && hasOnes)
            {
                word = word.Replace('*', '-');
            }
            else
            {
                word = word.Replace('*', ' ');
            }

            return word;
        }
    }
}
