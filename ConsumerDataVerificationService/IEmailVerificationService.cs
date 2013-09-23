using System.Threading.Tasks;

namespace MKS.ConsumerDataVerification
{
    public interface IEmailVerificationService
    {
        Task<EmailValidationResult> VerifyEmailAddress(string emailAddress);
    }
}
