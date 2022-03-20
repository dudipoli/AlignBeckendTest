using AlignBeckendApi.Dal;
using AlignBeckendApi.Dal.Entities;
using AlignBeckendTest.Accessors;
using AlignBeckendTest.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace AlignBeckendTest.Modules
{
    public class MissionModule : IMissionModule
    {
        private readonly MI6DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGoogleApiAccessor _googleApiAccessor;

        public MissionModule(MI6DbContext dbContext, IMapper mapper, IGoogleApiAccessor googleApiAccessor)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _googleApiAccessor = googleApiAccessor;
        }

        public async Task AddMission(List<Mission> missions)
        {
            await _dbContext.AddRangeAsync(_mapper.Map<List<MissionEntity>>(missions));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CountryIsolation> GetCountryByIsolation()
        {
            var isolatedAgents = (await _dbContext.Missions
                .Select(a => new { a.AgentAlias, a.Country })
                .Distinct().ToListAsync())
                .GroupBy(x => x.AgentAlias)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Country)
                                                 .OrderBy(v => v)
                                                 .ToList());
            Dictionary<string, List<string>> aggByIsolation = new();
            return TraverseMissions(isolatedAgents);
        }

        public async Task<Mission> GetClosestMission(string address)
        {
            var missions = _mapper.Map<List<Mission>>(await _dbContext.Set<MissionEntity>().ToListAsync());
            string locationsToSearch = BuildLocationsToSearch(missions);
            string closestAddress = await GetClosestAddress(address, locationsToSearch);
            return missions.Find(miss => miss.Address.Contains(closestAddress.Split(',')[0]));
        }

        private static CountryIsolation TraverseMissions(Dictionary<string, List<string>> isolatedAgents)
        {
            Dictionary<string, List<string>> aggByIsolation = new();
            foreach (var agent in isolatedAgents.Keys)
            {
                if (isolatedAgents[agent].Count == 1)
                {
                    string country = isolatedAgents[agent].FirstOrDefault();
                    if (aggByIsolation.ContainsKey(country))
                    {
                        aggByIsolation[country].Add(agent);
                    }
                    else
                    {
                        List<string> agents = new();
                        agents.Add(agent);
                        aggByIsolation.Add(country, agents);
                    }
                }
            }
            CountryIsolation res = new();
            res.Country = aggByIsolation.Aggregate((l, r) => l.Value.Count > r.Value.Count ? l : r).Key;
            res.IsolationDegree = aggByIsolation[res.Country].Count;
            return res;
        }

        private static string BuildLocationsToSearch(List<Mission> missions)
        {
            string concatString = string.Empty;
            foreach (Mission mission in missions)
            {
                concatString += mission.Address + "|";
            }
            if (concatString.EndsWith("|"))
            {
                concatString = concatString.Remove(concatString.Length - 1);
            }
            return concatString;
        }
        private async Task<string> GetClosestAddress(string address, string locationsToSearch)
        {
            var distancesRes = await _googleApiAccessor.GetDistanceMatrixAsync(HttpUtility.UrlEncode(address), HttpUtility.UrlEncode(locationsToSearch));
            DestinationMatrix destinationMatrix = await JsonSerializer.DeserializeAsync<DestinationMatrix>(await distancesRes.Content.ReadAsStreamAsync());
            var closestElement = destinationMatrix?.rows.FirstOrDefault()?.elements.Select(e => e)
                .Where(d => d.status.ToUpper() != "ZERO_RESULTS" &&
                d.distance?.value == destinationMatrix?.rows.FirstOrDefault()?.elements.Min(origin => origin?.distance?.value)).FirstOrDefault();
            if (closestElement == null)
                throw new AggregateException();
            int indexOfElement = destinationMatrix?.rows.FirstOrDefault()?.elements.IndexOf(closestElement) ?? 0;
            var originAddresses = locationsToSearch.Split("|").ToList();
            return originAddresses[indexOfElement];
        }
    }
}
