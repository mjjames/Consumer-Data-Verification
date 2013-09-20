using System.Collections.Generic;

namespace MKS.EmailValidation.Models
{
    public class PostcodeAnywhereResult<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
