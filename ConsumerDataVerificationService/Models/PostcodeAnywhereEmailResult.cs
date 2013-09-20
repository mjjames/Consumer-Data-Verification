namespace MKS.EmailValidation.EmailVerificationServices
{
    public class PostcodeAnywhereEmailResult
    {
        public string Email { get; set; }
        public string MailServer { get; set; }
        public bool ValidFormat { get; set; }
        public bool FoundDnsRecord { get; set; }
    }
}
