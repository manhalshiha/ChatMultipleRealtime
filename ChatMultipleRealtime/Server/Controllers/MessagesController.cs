using ChatMultipleRealtime.Server.Data;
using ChatMultipleRealtime.Server.Data.Entities;
using ChatMultipleRealtime.Server.Hubs;
using ChatMultipleRealtime.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatMultipleRealtime.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly ChatContext chatContext;
        private readonly IHubContext<ChatHub, IBlazingChatHubClient> hubContext;

        public MessagesController(ChatContext chatContext, IHubContext<ChatHub,IBlazingChatHubClient> hubContext)
        {
            this.chatContext = chatContext;
            this.hubContext = hubContext;
        }
        [HttpPost("")]
        public async Task<IActionResult> SendMessage(MessageSendDto messageDto,CancellationToken cancellationToken)
        {
            if (messageDto.ToUserId <= 0 || string.IsNullOrWhiteSpace(messageDto.Message))
                return BadRequest();
            var message = new Message
            {
                FromId = UserId,
                ToId=messageDto.ToUserId,
                Contetn=messageDto.Message,
                SentOn=DateTime.Now
            };
            await chatContext.Messages.AddAsync(message,cancellationToken);
            if(await chatContext.SaveChangesAsync(cancellationToken) > 0)
            {
                await hubContext.Clients.User(messageDto.ToUserId.ToString()).MessageRecived(base.UserId, messageDto.Message);
                return Ok();
            }
            else
            {
                return StatusCode(500, "Unable to send message");
            }
        }
        [HttpGet("{otherUserId:int}")]
        public async Task<IEnumerable<MessageDto>> GetMessages(int otherUserId,CancellationToken cancellationToken)
        {
            var messages=await chatContext.Messages
                            .AsNoTracking()
                            .Where(m=>
                                (m.FromId==otherUserId&&m.ToId==UserId)
                                ||(m.ToId==otherUserId&&m.FromId==UserId)
                                )
                            .Select(m=>new MessageDto(m.ToId,m.FromId,m.Contetn))
                            .ToListAsync(cancellationToken);  
            return messages;
        }
    }
}
