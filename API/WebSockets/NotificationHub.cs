using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public interface INotificationHub
    {
        Task SendNotice(string to, string messageContent);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendNotice(string to, string messageContent)
        {
            await Clients.Group(to).SendNotice(to, messageContent);
            //await Clients.Group(to).SendAsync("messageReceived", to);
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            string name = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}
