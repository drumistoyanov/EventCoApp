using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;

namespace EventCoApp.WebApp.Views.Events
{
    public class EditModel : PageModel
    {
        private readonly EventCoApp.DataAccessLibrary.DataAccess.EventCoContext _context;

        public EditModel(EventCoApp.DataAccessLibrary.DataAccess.EventCoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);

            if (Event == null)
            {
                return NotFound();
            }
           ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FirstName");
           ViewData["EventTypeId"] = new SelectList(_context.Set<EventType>(), "Id", "Name");
           ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Name");
           ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.ID == id);
        }
    }
}
