﻿using API.Core;
using API.Data.Entities.Messaging;
using API.Data.Entities.Users;
using API.Data.Models;
using API.Data.Models.User;
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

            CreateMap<User, string>().ConvertUsing(new UserConverter());

            CreateMap<Notification, NotificationModel>();
            CreateMap<ServiceResponse<Notification>, ServiceResponse<NotificationModel>>();
            CreateMap<ServiceResponse<List<Notification>>, ServiceResponse<List<NotificationModel>>>();

            CreateMap<PrivateMessage, PrivateMessageModel>();
            CreateMap<ServiceResponse<PrivateMessage>, ServiceResponse<PrivateMessageModel>>();
            CreateMap<ServiceResponse<List<PrivateMessage>>, ServiceResponse<List<PrivateMessageModel>>>();

            CreateMap<UserInfo, UserInfoModel>();
        }

        private class UserConverter : ITypeConverter<User, string>
        {
            public string Convert(User source, string destination, ResolutionContext context)
            {
                return source?.Username;
            }
        }
    }
}