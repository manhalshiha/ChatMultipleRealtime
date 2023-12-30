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
        public async Task<IEnumerable<UserDto>> GetUsers()=> await chatContext.Users.AsNoTracking().Where(u=>u.Id!=UserId).Select(u => new UserDto(u.Id, u.Name)).ToListAsync();
        
    }
}
