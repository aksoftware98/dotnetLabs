using AutoMapper;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotNetLabs.Server.Models.Profiles
{
    public class VideoProfile : Profile
    {

        public VideoProfile(EnvironmentOptions env)
        {
            CreateMap<Video, VideoDetail>()
                .ForMember(dest => dest.Tags, map => map.MapFrom(v => v.Tags.Select(t => t.Name).ToList()))
                .ForMember(dest => dest.ThumpUrl, map => map.MapFrom(v => $"{env.ApiUrl}/{v.ThumpUrl}"));
        }

    }
}
