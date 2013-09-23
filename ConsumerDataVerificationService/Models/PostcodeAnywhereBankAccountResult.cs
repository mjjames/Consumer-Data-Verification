namespace MKS.EmailValidation.Models
{
    public class PostcodeAnywhereBankAccountResult
    {
        public bool IsCorrect { get; set; }
        public bool IsDirectDebitCapable { get; set; }
        public bool IsFasterPaymentsSupported { get; set; }
        public bool IsChapsSupported { get; set; }
        public string StatusInformation { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string Bank { get; set; }
        public string BankBIC { get; set; }
        public string Branch { get; set; }
        public string BranchBIC { get; set; }
        public string ContactAddressLine1 { get; set; }
        public string ContactAddressLine2 { get; set; }
        public string ContactPostTown { get; set; }
        public string ContactPostcode { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public bool FasterPaymentsSupported { get; set; }
        public bool CHAPSSupported { get; set; }
    }
}
