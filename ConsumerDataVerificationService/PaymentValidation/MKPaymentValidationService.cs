using System;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.ConsumerDataVerification.PaymentValidation
{
    public class MKPaymentValidationService : IPaymentValidationService
    {
        public Task<BankAccountValidationResult> BankAccountValidation(string sortcode, string account)
        {
            throw new NotSupportedException();
        }

        public Task<CreditCardValidationResult> CreditCardValidation(string cardnumber)
        {
            var result = new CreditCardValidationResult(cardnumber)
                {
                    IsValid = IsCreditCardFormattedCorrectly(cardnumber) && IsLunnCheckValid(cardnumber)
                };
            return TaskEx.FromResult(result);
        }

        private static bool IsCreditCardFormattedCorrectly(string cardnumber)
        {
            return !string.IsNullOrWhiteSpace(cardnumber)
                    && cardnumber.Length >= 12
                    && cardnumber.Length <= 19
                    && cardnumber.ToCharArray().All(char.IsNumber);
        }

        private static bool IsLunnCheckValid(string cardnumber)
        {
            if (string.IsNullOrWhiteSpace(cardnumber))
            {
                return false;
            }
            return cardnumber.ToCharArray().Select((c, i) => (c - '0') << ((cardnumber.Length - i - 1) & 1)).Sum(n => n > 9 ? n - 9 : n) % 10 == 0;
        }
    }
}
