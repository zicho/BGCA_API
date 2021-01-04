﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public interface INotificationHub
    {
        Task SendNotice(string to, string messageContent);
        Task SendPrivateMessage(string to, string messageSubject, string messageContent);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendNotice(string to, string messageContent)
        {
            await Clients.Group(to).SendNotice(to, messageContent);
        }

        public async Task SendPrivateMessage(string to, string messageSubject, string messageContent)
        {
            await Clients.Group(to).SendPrivateMessage(to, messageSubject, messageContent);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            string name = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}