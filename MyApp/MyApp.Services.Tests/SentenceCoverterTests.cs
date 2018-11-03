using System;
using NUnit.Framework;
using MyApp.ServiceContracts.Services;

namespace MyApp.Services.Tests
{
    [TestFixture]
    public class SentenceCoverterTests
    {
        ISentenceConverterService _textConverterService;

        public void Setup()
        {
            _textConverterService = new SentenceConverterService(new NumberConverterService());
        }

        /// <summary>
        /// tests a sentence that included words and decimal numbers
        /// </summary>
        /// <param name="text">the sentence</param>
        /// <param name="expected">the converted sentence</param>
        [Test]
        [TestCase(@"John Smith ""123.45""", @"John Smith ""ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS""")]
        [TestCase(@"50 John Mondragon 100.12", @"FIFTY DOLLARS John Mondragon ONE HUNDRED DOLLARS AND TWELVE CENTS")]
        public void TestSentenceConversionSucess(string text, string expected)
        {
            // Arrange
            Setup();

            // Act
            var result = _textConverterService.TextToNumberWords(text);

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}
