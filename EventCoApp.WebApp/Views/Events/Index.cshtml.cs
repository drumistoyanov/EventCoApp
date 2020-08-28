using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;

namespace EventCoApp.WebApp.Views.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventCoApp.DataAccessLibrary.DataAccess.EventCoContext _context;

        public IndexModel(EventCoApp.DataAccessLibrary.DataAccess.EventCoContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();
        }
    }
}
