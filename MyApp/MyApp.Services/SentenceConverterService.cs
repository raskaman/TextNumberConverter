using MyApp.ServiceContracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class SentenceConverterService : ISentenceConverterService
    {
        INumberConverterService _numberConverterService;
        public SentenceConverterService(INumberConverterService numberConverterService)
        {
            _numberConverterService = numberConverterService;
        }

        /// <summary>
        /// takes a sentence with numbers, extracts the numbers to convert them
        /// </summary>
        /// <param name="sentence">string with numbers</param>
        /// <returns>foramted sentence</returns>
        public string TextToNumberWords(string sentence)
        {
            // regex to extact decimals
            string pattern = @"[^0-9\.]+";

            // get all decimals in sentence
            List<decimal> digits = Regex.Split(sentence, pattern).Where(s => !string.IsNullOrEmpty(s)).Select(s => decimal.Parse(s)).ToList();

            // replace decimnals with the words for it
            foreach (var digit in digits)
            {
                sentence = sentence.Replace(digit.ToString(), _numberConverterService.DecimalNumberToWords(digit).ToUpper());
            }

            return sentence;
        }
    }
}
