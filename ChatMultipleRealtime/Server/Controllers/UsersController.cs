using ChatMultipleRealtime.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatMultipleRealtime.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ChatContext chatContext;

        public UsersController(ChatContext chatContext )
        {
            this.chatContext = chatContext;
        }
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()=>
            await chatContext.Users.AsNoTracking()
                    .Where(u=>u.Id!=UserId)
                    .Select(u => new UserDto(u.Id, u.Name,false))
                    .ToListAsync();

        [HttpGet("chats")]
        public async Task<IEnumerable<UserDto>> GetUserChats(CancellationToken cancellationToken)
        {
            IEnumerable<UserDto> ChatUsers=new List<UserDto>();
            var UniqueUsers = await chatContext.Messages
                            .AsNoTracking()
                            .Where(m => m.FromId == UserId || m.ToId == UserId)
                            .Select(m => new { From = m.FromId,To= m.ToId })
                            .Distinct()
                            .ToListAsync(cancellationToken);
            var UniqueIds = new HashSet<int>();
            UniqueUsers.ForEach(u =>
            {
                if (u.From != UserId)
                {
                    UniqueIds.Add(u.From);
                }
                if (u.To != UserId)
                {

                    UniqueIds.Add(u.To);
                }
            });
            if(UniqueIds.Count>0)
            {
                ChatUsers = await chatContext.Users
                                .AsNoTracking()
                                .Where(u => UniqueIds.Contains(u.Id))
                                .Select(u => new UserDto(u.Id, u.Name, false))
                                .ToListAsync(cancellationToken);
            }
            return ChatUsers;
        }
            
    }

}
