using System.Threading.Tasks;
using MKS.MKEmailVerificationService;
using Xunit;

namespace ConsumerDataVerificationService.Tests
{
    public class MKEmailVerificationServiceTests
    {
        [Fact]
        public async Task NullValueIsInvalid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress(null);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task EmptyValueIsInvalid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("");
            Assert.False(result.IsValid);

        }

        [Fact]
        public async Task WhiteSpaceValueIsInvalid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress(" ");
            Assert.False(result.IsValid);

        }

        [Fact]
        public async Task InvalidFormatValueIsInvalid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("mmmmmm");
            Assert.False(result.IsValid);

        }

        [Fact]
        public async Task BasicFormatValueIsValidFormat()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("m@j.com");
            Assert.True(result.IsFormatValid);
        }

        [Fact]
        public async Task PlusAddressingFormatValueIsValid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("mike+test@j.com");
            Assert.True(result.IsFormatValid);
        }

        [Fact]
        public async Task BasicFormatInvalidDomainIsNotValid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("m@j.com");
            Assert.False(result.IsValid);
            Assert.False(result.DomainHasDnsRecord);
        }

        [Fact]
        public async Task BasicFormatValidDomainNoMxRecordIsNotValid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("test@emmanuelnetherton.org.uk");
            Assert.False(result.IsValid);
            Assert.False(result.DomainHasMxRecord);
        }

        [Fact]
        public async Task ValidEmailIsValid()
        {
            var service = new MKEmailVerificationService();
            var result = await service.VerifyEmailAddress("test@mksoftwaresolutions.co.uk");
            Assert.True(result.IsValid);
        }
    }
}
