using ApplicationCore.Product;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.MappingProfile
{
    public class DtoProfile: Profile
    {
        public DtoProfile()
        {
            CreateMap<Products, ProductDto>()
                .ForMember(dst => dst.ProductId, src => src.MapFrom(sourceMember => sourceMember.Id));
        }
    }
}
