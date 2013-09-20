using System;

namespace MKS.EmailValidation.Exceptions
{
    public class PostcodeAnywhereException : Exception
    {
        public PostcodeAnywhereException(int errorCode, string description, string cause) : base("The PostCode Anywhere service returned an error code, please check description and cause for more information")
        {
            ErrorCode = errorCode;
            Description = description;
            Cause = cause;
        }

        public int ErrorCode { get; private set; }
        public string Description { get; private set; }
        public string Cause { get; private set; }
    }
}
