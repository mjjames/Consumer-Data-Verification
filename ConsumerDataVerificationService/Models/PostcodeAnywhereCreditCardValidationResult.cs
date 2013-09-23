namespace MKS.EmailValidation.Models
{
    public class PostcodeAnywhereCreditCardValidationResult
    {
        public bool IsValid { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
    }
}
