using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Models
{
    public class CountryIsolation
    {
        public string Country { get; set; }
        public int IsolationDegree { get; set; }
    }

    public class CountryIso
    {
        public List<Agent> Agents { get; set; }
    }

    public class Agent
    {
        public string Alias { get; set; }
        public bool IsIsolated { get; set; }
    }
}
