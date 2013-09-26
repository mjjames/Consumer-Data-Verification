namespace MKS.ConsumerDataVerification
{
    public class EmailValidationResult
    {
        public EmailValidationResult(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public EmailValidationResult(string emailAddress, bool isFormatValid, bool isDnsValid = false, bool hasMxRecord = false)
        {
            EmailAddress = emailAddress;
            IsFormatValid = isFormatValid;
            DomainHasDnsRecord = isDnsValid;
            DomainHasMxRecord = hasMxRecord;
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
