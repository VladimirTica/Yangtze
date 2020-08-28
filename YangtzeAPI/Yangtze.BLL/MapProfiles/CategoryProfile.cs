using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.MapProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryForUpdateDto, Category>();
        }
    }
}
