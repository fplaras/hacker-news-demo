using AutoMapper;
using HackerNewsDemo.Module.Domain;
using HackerNewsDemo.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.MappingProfiles
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<HackerNewsItem, HackerNewsItemDomain>()
                .ForMember(x => x.Kids, o => o.MapFrom(s => string.Join(",", s.Kids).ToString()))
                .ForMember(x => x.Parts, o => o.MapFrom(s => string.Join(",", s.Parts).ToString()));

            CreateMap<HackerNewsItem, HackerNewsItemDTO>()
                .ForMember(x => x.Kids, o => o.Ignore())
                .ForMember(x => x.Parts, o => o.Ignore());

            CreateMap<HackerNewsItemDomain, HackerNewsItemDTO>()
                .ForMember(x => x.Kids, o => o.Ignore())
                .ForMember(x => x.Parts, o => o.Ignore());
        }
    }
}
