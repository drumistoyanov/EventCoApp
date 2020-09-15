using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Hubs
{
    public class ChatHub : Hub
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected EventCoContext _context;
        public ChatHub(IHttpContextAccessor httpContextAccessor, EventCoContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendMessage(string user, string message)
        {

            int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            MessageViewModel messageDeserilized = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageViewModel>(message);
            messageDeserilized.CreatedById = userId;
            await _context.Messages.AddAsync(messageDeserilized.ToEntity());
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage", user, messageDeserilized.Content);
        }
    }
}
