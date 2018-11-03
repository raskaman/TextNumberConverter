using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ServiceContracts.Services
{
    public interface INumberConverterService
    {
        string DecimalNumberToWords(decimal number);
    }
}
