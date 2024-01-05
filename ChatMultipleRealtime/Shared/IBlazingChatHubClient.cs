using ChatMultipleRealtime.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMultipleRealtime.Shared
{
    public interface IBlazingChatHubClient
    {
       Task UserConnected(UserDto user);
       Task OnlineUsersList(IEnumerable<UserDto> users);
       Task UserIsOnline(int userId);
       Task MessageRecived(MessageDto messageDto);
    }
}
