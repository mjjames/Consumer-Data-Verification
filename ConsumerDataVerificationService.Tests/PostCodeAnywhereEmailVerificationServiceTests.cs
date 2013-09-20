using System;
using System.Threading.Tasks;
using AssertExLib;
using MKS.EmailValidation.EmailVerificationServices;
using MKS.EmailValidation.Exceptions;
using Xunit;

namespace ConsumerDataVerificationService.Tests
{
    public class PostCodeAnywhereEmailVerificationServiceTests
    {
        [Fact]
        public void EmptyApiKeyThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new PostCodeAnywhereEmailVerificationService(""));
        }

        [Fact]
        public void WhitespaceApiKeyThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new PostCodeAnywhereEmailVerificationService("     "));
        }

        [Fact]
        public void InvalidApiKeyThrowsPostcodeAnywhereException()
        {
            var service = new PostCodeAnywhereEmailVerificationService("234234234");
            var exception = AssertEx.TaskThrows<PostcodeAnywhereException>(async () => await service.VerifyEmailAddress("m@j.com"));
            Assert.Equal(2, exception.ErrorCode);
        }

        [Fact]
        public async Task ValidEmailReturnsValidFormat()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@kslgarageservices.co.uk");
            Assert.Equal(true, result.IsFormatValid);
        }

        [Fact]
        public async Task ValidEmailReturnsIsValid()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@kslgarageservices.co.uk");
            Assert.Equal(true, result.IsValid);
        }

        [Fact]
        public async Task ValidEmailReturnsDomainHasDnsRecord()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@kslgarageservices.co.uk");
            Assert.Equal(true, result.DomainHasDnsRecord);
        }

        [Fact]
        public async Task ValidEmailReturnsDomainHasMxRecord()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@kslgarageservices.co.uk");
            Assert.Equal(true, result.DomainHasMxRecord);
        }

        [Fact]
        public async Task EmailWithInvalidFormatReturnsInvalidFormat()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.jameskslgarageservices.co.uk");
            Assert.Equal(false, result.IsFormatValid);
        }

        [Fact]
        public async Task EmailWithInvalidDomainReturnsInvalidDns()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@2kslgarageservices.co.uk");
            Assert.Equal(false, result.DomainHasDnsRecord);
        }

        [Fact]
        public async Task EmailWithInvalidDomainReturnsInvalidMx()
        {
            var service = new PostCodeAnywhereEmailVerificationService("DA23-CE14-JH99-AW13");
            var result = await service.VerifyEmailAddress("michael.james@2kslgarageservices.co.uk");
            Assert.Equal(false, result.DomainHasMxRecord);
        }
    }
}
