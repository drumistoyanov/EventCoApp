using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;

namespace EventCoApp.WebApp.Hubs
{
    public class ChatHub : Hub
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected EventCoContext _context;
        public ChatHub(IHttpContextAccessor httpContextAccessor,EventCoContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(string user, string message)
        {

            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var messageDeserilized = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageViewModel>(message);
            messageDeserilized.CreatedById = userId;
            await _context.Messages.AddAsync(messageDeserilized.ToEntity());
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage", user, messageDeserilized.Content);
        }
    }
}
