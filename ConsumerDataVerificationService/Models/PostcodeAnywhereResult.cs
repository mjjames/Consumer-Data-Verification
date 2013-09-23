using System.Collections.Generic;

namespace MKS.ConsumerDataVerification.Models
{
    public class PostcodeAnywhereResult<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
