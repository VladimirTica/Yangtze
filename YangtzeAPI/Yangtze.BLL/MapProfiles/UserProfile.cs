using AutoMapper;
using System;
using Yangtze.BLL.Models.DtoModels;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.MapProfiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegisterDto, User>().ReverseMap();
            CreateMap<User, UserForDisplayDto>();
        }
    }
}
