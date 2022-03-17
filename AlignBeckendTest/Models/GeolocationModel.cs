using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Models
{
    public class Geolocation
    {
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string formatted_address { get; set; }
    }
}
