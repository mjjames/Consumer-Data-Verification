namespace MKS.EmailValidation
{
    public class BankAccountValidationResult
    {
        public BankAccountValidationResult(string sortcode, string account)
        {
            SortCode = sortcode;
            AccountNumber = account;
        }
        public bool IsCorrect { get; set; }
        public bool IsDirectDebitCapable { get; set; }
        public bool IsFasterPaymentsSupported { get; set; }
        public bool IsChapsSupported { get; set; }
        public StatusInformation StatusInformation { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public BankDetails Bank { get; set; }
    }
}
