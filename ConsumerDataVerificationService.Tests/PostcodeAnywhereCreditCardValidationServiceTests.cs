using System;
using System.Threading.Tasks;
using AssertExLib;
using MKS.ConsumerDataVerification.Exceptions;
using MKS.ConsumerDataVerification.PaymentValidation;
using Xunit;

namespace ConsumerDataVerificationService.Tests
{
    public class PostcodeAnywhereCreditCardValidationServiceTests
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
        public async Task ValidCardNumberReturnsIsValid()
        {
            var service = new PostcodeAnywherePaymentValidationService("DA23-CE14-JH99-AW13");
            var result = await service.CreditCardValidation("675940141831560093");
            Assert.Equal(true, result.IsValid);
        }

        [Fact]
        public async Task ValidCardNumberReturnsCardType()
        {
            var service = new PostcodeAnywherePaymentValidationService("DA23-CE14-JH99-AW13");
            var result = await service.CreditCardValidation("675940141831560093");
            Assert.Equal("SWITCH", result.CardType);
        }

        [Fact]
        public async Task InvalidCardNumberReturnsInvalid()
        {
            var service = new PostcodeAnywherePaymentValidationService("DA23-CE14-JH99-AW13");
            var result = await service.CreditCardValidation("1234567894565456");
            Assert.Equal(false, result.IsValid);
        }
    }
}
