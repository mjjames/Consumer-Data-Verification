namespace MKS.EmailValidation
{
    public class CreditCardValidationResult
    {
        public CreditCardValidationResult(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public bool IsValid { get; internal set; }
        public string CardNumber { get; internal set; }
        public string CardType { get; internal set; }
    }
}
