using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MKS.ConsumerDataVerification.Exceptions;
using MKS.ConsumerDataVerification.Models;
using Newtonsoft.Json;

namespace MKS.ConsumerDataVerification.EmailVerificationServices
{
    public class PostCodeAnywhereEmailVerificationService : IEmailVerificationService
    {
        private readonly string _apiKey;

        public PostCodeAnywhereEmailVerificationService(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Invalid API Key provided");
            }
            _apiKey = apiKey;
        }

        public async Task<EmailValidationResult> VerifyEmailAddress(string emailAddress)
        {
            using (var client = new HttpClient())
            {
                var url =
                    string.Format("http://services.postcodeanywhere.co.uk/EmailValidation/Interactive/Validate/v1.10/json3.ws?key={0}&email={1}",
                                        _apiKey, 
                                        Uri.EscapeDataString(emailAddress)
                                    );
                var result = await client.GetStringAsync(url);

                if (result.StartsWith("{\"Items\":[{\"Error\""))
                {
                    var error = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereError>>(result).Items.Single();
                    throw new PostcodeAnywhereException(error.Error, error.Description, error.Cause);
                }

                var remoteValidationResult = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereEmailResult>>(result).Items.Single();
                return new EmailValidationResult(emailAddress)
                    {
                        IsFormatValid = remoteValidationResult.ValidFormat,
                        DomainHasDnsRecord = remoteValidationResult.FoundDnsRecord,
                        DomainHasMxRecord = !String.IsNullOrWhiteSpace(remoteValidationResult.MailServer)
                    };
            }
        }
    }
}
