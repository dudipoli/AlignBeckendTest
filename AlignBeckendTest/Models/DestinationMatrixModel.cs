using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Models
{
    public class DestinationMatrix
    {
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

    public class Element
    {
        public DistanceObj distance { get; set; }
        public string status { get; set; }
    }
    public class DistanceObj
    {
        public int value { get; set; }
    }
}
