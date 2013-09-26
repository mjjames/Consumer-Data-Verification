using System.Threading.Tasks;
using MKS.ConsumerDataVerification.PaymentValidation;
using Xunit;

namespace ConsumerDataVerificationService.Tests
{
    public class MKPaymentValidationServiceTests
    {
        [Fact]
        public async Task ValidCardNumberIsValid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("1234567812345670");
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ValidCardLengthButInvalidCardLunhReturnsInvalid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("1234567812345678");
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task ValidMaestroCardNumberReturnsCorrectlyFormatted()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("675940543258754468");
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CardNumberTooShortReturnsInvalid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("456789");
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CardNumberTooLongReturnsInvalid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("456789789456789456123456789");
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task EmptyCardNumberReturnsInvalid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("");
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CardNumberWithLettersReturnsInvalid()
        {
            var service = new MKPaymentValidationService();
            var result = await service.CreditCardValidation("67594054AS58754468");
            Assert.False(result.IsValid);
        }

    }
}
