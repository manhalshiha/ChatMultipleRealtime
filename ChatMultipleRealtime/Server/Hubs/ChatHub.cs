using ChatMultipleRealtime.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatMultipleRealtime.Server.Hubs
{
    [Authorize]
    public class ChatHub:Hub<IBlazingChatHubClient>,IBlazingChatHubServer
    {
        private static readonly IDictionary<int,UserDto> _onlineUsers = new Dictionary<int, UserDto>(); 
        public ChatHub() 
        {
             
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
            
        }
        public async Task SetUserOnline(UserDto user)
        {
            await Clients.Caller.OnlineUsersList(_onlineUsers.Values.Where(x=>x.Id!=user.Id));
            if (!_onlineUsers.ContainsKey(user.Id))
            {
                _onlineUsers.Add(user.Id,user);
                 await Clients.Others.UserIsOnline(user.Id);
            } 
         }
    }
}
