using AlignBeckendTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Modules
{
    public interface IMissionModule
    {
        Task AddMission(List<Mission> missions);
        Task<CountryIsolation> GetCountryByIsolation();
        Task<Mission> GetClosestMission(string address);
    }
}
