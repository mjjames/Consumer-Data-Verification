using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MKS.ConsumerDataVerification;
using Rsft.Net.Dns;
using Rsft.Net.Dns.Entities;
using System;

namespace MKS.MKEmailVerificationService
{
    public class MKEmailVerificationService : IEmailVerificationService
    {
        /// <summary>
        /// Email Validation Regex Taken From https://github.com/srkirkland/DataAnnotationsExtensions/blob/master/DataAnnotationsExtensions/EmailAttribute.cs
        /// </summary>
        private static readonly Regex _regex =
            new Regex(
                @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public async Task<EmailValidationResult> VerifyEmailAddress(string emailAddress)
        {
            //basic validation first
            if (string.IsNullOrWhiteSpace(emailAddress) || _regex.Match(emailAddress).Length == 0)
            {
                return new EmailValidationResult(emailAddress);
            }

            //now get the domain
            var domain = emailAddress.Substring(emailAddress.IndexOf('@') + 1);
            try
            {
                await Dns.GetHostEntryAsync(domain).ConfigureAwait(false);
                var mxResponse = await Dns.QueryAsync(domain, QType.MX).ConfigureAwait(false);
                return new EmailValidationResult(emailAddress, true, true, mxResponse.RecordsMX.Any());
            }
            catch (SocketException)
            {
                return new EmailValidationResult(emailAddress, true);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is SocketException)
                {
                    return new EmailValidationResult(emailAddress, true);
                }
                throw;
            }

        }
    }
}
