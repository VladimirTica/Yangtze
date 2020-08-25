using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yangtze.BLL.Models;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}
