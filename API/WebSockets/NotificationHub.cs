using API.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public interface INotificationHub
    {
        Task SendNotice(NotificationModel model);

        Task SendPrivateMessage(PrivateMessageModel model);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendNotice(NotificationModel model)
        {
            await Clients.Group(model.Recipient).SendNotice(model);
        }

        public async Task SendPrivateMessage(PrivateMessageModel model)
        {
            await Clients.Group(model.Recipient).SendPrivateMessage(model);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            string name = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}