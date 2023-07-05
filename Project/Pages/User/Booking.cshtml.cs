using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GoogleAuth;
using Microsoft.AspNetCore.Mvc.Rendering;
using Google.Apis.Calendar.v3.Data;

namespace Project.Pages.User
{
    public class Booking : PageModel
    {
        public SelectList? Rooms { get; set;}
        public Event NewEvent {get; set;}
        public async Task OnGetAsync()
        {
            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            IEnumerable<CalendarListEntry> roomList = calendarList.Items.Where(c => c.Description != null);
            roomList = roomList.Where(c => c.Description.Contains("Ruang"));
            IEnumerable<string> room = roomList.Select(c => c.Summary);

            Rooms = new SelectList(room);

        }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || NewEvent == null)
            {
                return Page();
            }

            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();

            return RedirectToPage("./Index");
        } 
    }
}