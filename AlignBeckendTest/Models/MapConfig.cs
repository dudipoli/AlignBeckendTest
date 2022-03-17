using AlignBeckendApi.Dal.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlignBeckendTest.Models
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            MapMissionModelToEntity();
            MapMissionEntityToModel();
        }

        private void MapMissionModelToEntity()
        {
            CreateMap<Mission, MissionEntity>()
                .ForMember(dst => dst.AgentAlias, opt => opt.MapFrom(src => src.Agent));
        }
        private void MapMissionEntityToModel()
        {
            CreateMap<MissionEntity, Mission>()
                .ForMember(dst => dst.Agent, opt => opt.MapFrom(src => src.AgentAlias));
        }
    }


}
