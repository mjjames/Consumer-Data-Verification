using System;
using System.Threading.Tasks;
using AssertExLib;
using MKS.ConsumerDataVerification.Exceptions;
using MKS.ConsumerDataVerification.PaymentValidation;
using Xunit;

namespace ConsumerDataVerificationService.Tests
{
    public class PostcodeAnywhereBACSValidationServiceTests
    {
        [Fact]
        public void EmptyApiKeyThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new PostcodeAnywherePaymentValidationService(""));
        }

        [Fact]
        public void WhitespaceApiKeyThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new PostcodeAnywherePaymentValidationService("     "));
        }

        [Fact]
        public void InvalidApiKeyThrowsPostcodeAnywhereException()
        {
            var service = new PostcodeAnywherePaymentValidationService("234234234");
            var exception = AssertEx.TaskThrows<PostcodeAnywhereException>(async () => await service.CreditCardValidation("123456789"));
            Assert.Equal(2, exception.ErrorCode);
        }

        [Fact]
        public async Task ValidSortCodeAndAccountNumberReturnsValid()
        {
            var service = new PostcodeAnywherePaymentValidationService("GE98-CH68-AB88-KU93");
            var result = await service.BankAccountValidation("40-14-18", "31560093");
            Assert.True(result.IsCorrect);
        }

        [Fact]
        public async Task InvalidSortCodeAndAccountNumberReturnsInValid()
        {
            var service = new PostcodeAnywherePaymentValidationService("GE98-CH68-AB88-KU93");
            var result = await service.BankAccountValidation("40-24-18", "58560093");
            Assert.False(result.IsCorrect);
        }

        [Fact]
        public async Task ValidSortCodeAndInvalidAccountNumberReturnsBankDetails()
        {
            var service = new PostcodeAnywherePaymentValidationService("GE98-CH68-AB88-KU93");
            var result = await service.BankAccountValidation("40-14-18", "58560093");
            Assert.Equal("HSBC BANK PLC", result.Bank.Bank);
        }
    }
}
