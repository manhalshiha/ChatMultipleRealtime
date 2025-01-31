﻿using ChatMultipleRealtime.Server;
using ChatMultipleRealtime.Server.Data;
using ChatMultipleRealtime.Server.Data.Entities;
using ChatMultipleRealtime.Server.Hubs;
using ChatMultipleRealtime.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatMultipleRealtime.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ChatContext chatContext;
        private readonly TokenService tokenService;
        private readonly IHubContext<ChatHub, IBlazingChatHubClient> hubContext;

        public AccountController(ChatContext chatContext, TokenService tokenService,IHubContext<ChatHub,IBlazingChatHubClient> hubContext)
        {
            this.chatContext = chatContext;
            this.tokenService = tokenService;
            this.hubContext = hubContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var isUserNameExist = await chatContext.Users
                                .AsNoTracking()
                                .AnyAsync(x => x.Username == registerDto.Username, cancellationToken);
            if (isUserNameExist)
            {
                return BadRequest($"The username:\"{nameof(registerDto.Username)}\" is already exists!!");
            }
            var user = new User
            {
                Username = registerDto.Username,
                AddedOn = DateTime.Now,
                Name = registerDto.Name,
                Password = registerDto.Password
            };
            await chatContext.AddAsync(user, cancellationToken);
            await chatContext.SaveChangesAsync(cancellationToken);
            await hubContext.Clients.All.UserConnected(new UserDto(user.Id, user.Name));
            return Ok(tokenService.GenerateToken(user));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await chatContext.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password, cancellationToken);
            if (user is null)
            {
                return BadRequest("there is error either username or password");
            }
            return Ok(tokenService.GenerateToken(user));
        }
        
    }
}
