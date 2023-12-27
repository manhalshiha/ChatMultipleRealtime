using ChatMultipleRealtime.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatMultipleRealtime.Server.Hubs
{
    [Authorize]
    public class ChatHub:Hub<IBlazingChatHubClient>,IBlazingChatHubServer
    {
        private static readonly IDictionary<int,UserDto> connectedUsers = new Dictionary<int, UserDto>(); 
        public ChatHub() 
        {
             
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
            
        }
        public async Task ConnectUser(UserDto user)
        {
            await Clients.Caller.UserConnectedList(connectedUsers.Values.Where(x=>x.Id!=user.Id));
            if (!connectedUsers.ContainsKey(user.Id))
            {
                connectedUsers.Add(user.Id,user);
                 await Clients.Others.UserConnected(user);
            } 
         }
    }
}
