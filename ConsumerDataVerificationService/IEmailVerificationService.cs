using System.Threading.Tasks;

namespace MKS.EmailValidation
{
    public interface IEmailVerificationService
    {
        Task<EmailValidationResult> VerifyEmailAddress(string emailAddress);
    }
}
