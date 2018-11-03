using MyApp.ServiceContracts.Services;
using NUnit.Framework;

namespace MyApp.Services.Tests
{
    [TestFixture]
    public class NumberConverterTests
    {
        INumberConverterService _numberConverterService;


        public void Setup()
        {
            _numberConverterService = new NumberConverterService();
        }

        /// <summary>
        /// tests multiple number up to a billion, also number smaller that one dollar
        /// </summary>
        /// <param name="number">the decimal number</param>
        /// <param name="expected">the english currency representation</param>
        [Test]
        [TestCase(123.45, "one hundred and twenty-three dollars and forty-five cents")]
        [TestCase(600000003, "six hundred million, three dollars")]
        [TestCase(50000000.20, "fifty million, dollars and twenty cents")]
        [TestCase(305789.5, "three hundred five thousand, seven hundred and eighty-nine dollars and fifty cents")]
        [TestCase(20450.50, "twenty thousand, four hundred and fifty dollars and fifty cents")]
        [TestCase(0.52, "fifty-two cents")]
        [TestCase(.32, "thirty-two cents")]
        [TestCase(100.12, "one hundred dollars and twelve cents")]
        [TestCase(1125.25, "one thousand, one hundred and twenty-five dollars and twenty-five cents")]
        [TestCase(5258500.25, "five million, two hundred and fifty-eight thousand, five hundred dollars and twenty-five cents")]
        [TestCase(7000500050.2, "seven billion, five hundred thousand, fifty dollars and twenty cents")]
        public void TestDoubleConversionSucess(decimal number, string expected)
        {
            // Arrange
            Setup();

            // Act
            var result = _numberConverterService.DecimalNumberToWords(number);

            // Assert
            Assert.AreEqual(result, expected);
            
        }
    }
}
