using System;
using System.Security.Claims;
using AutoMapper;
using Parxlab.Data.Dtos;
using Parxlab.Data.Dtos.Reserved;
using Parxlab.Entities;
using Parxlab.Entities.Identity;

namespace Parxlab.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<CreateReservedDto, Reserved>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<RefreshTokenDto, RefreshToken>();
            CreateMap<RoleClaim, Claim>();
        }
    }
}