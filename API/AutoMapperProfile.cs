using API.Core;
using API.Data.Models;
using API.Data.Entities.Users;
using AutoMapper;
using System.Collections.Generic;
using API.Data.Entities.Messaging;

namespace API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthUserModel>();
            CreateMap<ServiceResponse<User>, ServiceResponse<AuthUserModel>>();
            CreateMap<ServiceResponse<List<User>>, ServiceResponse<List<AuthUserModel>>>();

            CreateMap<User, string>().ConvertUsing(new UserConverter());

            CreateMap<PrivateMessage, PrivateMessageModel>();
            CreateMap<ServiceResponse<PrivateMessage>, ServiceResponse<PrivateMessageModel>>();
            CreateMap<ServiceResponse<List<PrivateMessage>>, ServiceResponse<List<PrivateMessageModel>>>();

        }

        private class UserConverter : ITypeConverter<User, string>
        {
            public string Convert(User source, string destination, ResolutionContext context)
            {
                return source.Username;
            }
        }
    }
}