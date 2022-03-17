using AlignBeckendTest.Models;
using AlignBeckendTest.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MI6Controller : ControllerBase
    {
        private readonly IMissionModule _missionModule;
        public MI6Controller(IMissionModule missionModule)
        {
            _missionModule = missionModule;
        }

        [Route("Mission")]
        [HttpPost]
        public async Task<IActionResult> AddMission([FromBody] List<Mission> mission)
        {
            try
            {
                await _missionModule.AddMission(mission);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Route("CountryByIsolation")]
        [HttpGet]
        public async Task<CountryIsolation> GetCountryByIsolation()
        {
            try
            {
                return await _missionModule.GetCountryByIsolation();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("FindClosest")]
        [HttpPost]
        public async Task<Mission> FindClosestMission([FromBody] MissionAddress request)
        {
            try
            {
                return await _missionModule.GetClosestMission(request.TargetLocation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
