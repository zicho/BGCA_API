using API.Core;
using API.Data.Models;
using API.Data.Entities.Users;
using AutoMapper;
using System.Collections.Generic;

namespace API
{
    public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<User, AuthUserModel>();
                CreateMap<ServiceResponse<User>, ServiceResponse<AuthUserModel>>();
                CreateMap<ServiceResponse<List<User>>, ServiceResponse<List<AuthUserModel>>>();
            }        
        }   
}