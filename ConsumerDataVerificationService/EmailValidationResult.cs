namespace MKS.ConsumerDataVerification
{
    public class EmailValidationResult
    {
        public EmailValidationResult(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }
        public bool IsValid
        {
            get { return IsFormatValid && DomainHasDnsRecord && DomainHasMxRecord; }
        }
        public bool IsFormatValid { get; internal set; }
        public bool DomainHasDnsRecord { get; internal set; }
        public bool DomainHasMxRecord { get; internal set; }
    }
}
