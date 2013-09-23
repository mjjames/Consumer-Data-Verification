using System.Threading.Tasks;

namespace MKS.EmailValidation
{
    interface IPaymentValidationService
    {
        Task<BankAccountValidationResult> BankAccountValidation(string sortcode, string account);
        Task<CreditCardValidationResult> CreditCardValidation(string cardnumber);
    }
}
