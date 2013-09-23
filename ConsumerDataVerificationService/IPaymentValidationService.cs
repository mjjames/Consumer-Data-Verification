using System.Threading.Tasks;

namespace MKS.ConsumerDataVerification
{
    interface IPaymentValidationService
    {
        Task<BankAccountValidationResult> BankAccountValidation(string sortcode, string account);
        Task<CreditCardValidationResult> CreditCardValidation(string cardnumber);
    }
}
