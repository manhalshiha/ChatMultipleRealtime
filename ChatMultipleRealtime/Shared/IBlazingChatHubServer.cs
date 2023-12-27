using ChatMultipleRealtime.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMultipleRealtime.Shared
{
    public interface IBlazingChatHubServer
    {
        Task ConnectUser(UserDto user);
    }
}
