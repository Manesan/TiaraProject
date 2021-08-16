using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tiara_api.DTOs;
using tiara_api.Models;

namespace tiara_api.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Thought, ThoughtDto>().ReverseMap();
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<BaseEntity, BaseDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Milestone, MileStoneDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
