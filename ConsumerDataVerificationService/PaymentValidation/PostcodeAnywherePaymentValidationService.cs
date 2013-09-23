using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MKS.EmailValidation.EmailVerificationServices;
using MKS.EmailValidation.Exceptions;
using MKS.EmailValidation.Models;
using Newtonsoft.Json;

namespace MKS.EmailValidation.PaymentValidation
{
    public class PostcodeAnywherePaymentValidationService : IPaymentValidationService
    {
        private readonly string _apiKey;

        public PostcodeAnywherePaymentValidationService(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Invalid API Key provided");
            }
            _apiKey = apiKey;
        }

        public async Task<BankAccountValidationResult> BankAccountValidation(string sortcode, string account)
        {
            using (var client = new HttpClient())
            {
                var url =
                    string.Format("http://services.postcodeanywhere.co.uk/BankAccountValidation/Interactive/Validate/v2.00/json3.ws?Key={0}&AccountNumber={1}&SortCode={2}",
                                        _apiKey,
                                        Uri.EscapeDataString(sortcode),
                                        Uri.EscapeDataString(account)
                                    );
                var result = await client.GetStringAsync(url);

                if (result.StartsWith("{\"Items\":[{\"Error\""))
                {
                    var error = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereError>>(result).Items.Single();
                    throw new PostcodeAnywhereException(error.Error, error.Description, error.Cause);
                }

                var remoteValidationResult = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereBankAccountResult>>(result).Items.Single();
                return new BankAccountValidationResult(sortcode, account)
                {
                    AccountNumber = remoteValidationResult.AccountNumber,
                    IBAN = remoteValidationResult.IBAN,
                    IsChapsSupported = remoteValidationResult.CHAPSSupported,
                    IsCorrect = remoteValidationResult.IsCorrect,
                    IsDirectDebitCapable = remoteValidationResult.IsDirectDebitCapable,
                    IsFasterPaymentsSupported = remoteValidationResult.FasterPaymentsSupported,
                    SortCode = remoteValidationResult.SortCode,
                    StatusInformation = (StatusInformation)Enum.Parse(typeof(StatusInformation), remoteValidationResult.StatusInformation, true),
                    Bank = new BankDetails
                        {
                            Address1 = remoteValidationResult.ContactAddressLine1,
                            Address2 = remoteValidationResult.ContactAddressLine2,
                            Bank = remoteValidationResult.Bank,
                            BankBIC = remoteValidationResult.BankBIC,
                            Branch = remoteValidationResult.Branch,
                            BranchBIC = remoteValidationResult.BankBIC,
                            Fax = remoteValidationResult.ContactFax,
                            Phone = remoteValidationResult.ContactPhone,
                            Postcode = remoteValidationResult.ContactPostTown,
                            Town = remoteValidationResult.ContactPostTown
                        }
                };
            }
        }

        public async Task<CreditCardValidationResult> CreditCardValidation(string cardnumber)
        {
            using (var client = new HttpClient())
            {
                var url =
                    string.Format("http://services.postcodeanywhere.co.uk/CardValidation/Interactive/Validate/v1.00/json3.ws?Key={0}&CardNumber={1}",
                                        _apiKey,
                                        Uri.EscapeDataString(cardnumber)
                                    );
                var result = await client.GetStringAsync(url);

                if (result.StartsWith("{\"Items\":[{\"Error\""))
                {
                    var error = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereError>>(result).Items.Single();
                    throw new PostcodeAnywhereException(error.Error, error.Description, error.Cause);
                }

                var remoteValidationResult = JsonConvert.DeserializeObject<PostcodeAnywhereResult<PostcodeAnywhereCreditCardValidationResult>>(result).Items.Single();
                return new CreditCardValidationResult(cardnumber)
                {
                    CardType = remoteValidationResult.CardType,
                    IsValid = remoteValidationResult.IsValid
                };
            }
        }
    }
}
