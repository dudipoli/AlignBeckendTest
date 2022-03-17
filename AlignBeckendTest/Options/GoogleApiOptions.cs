using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Options
{
    public class GoogleApiOptions
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string DistanceMatrix { get; set; }
        public string GeoCoding { get; set; }
    }
}
