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

namespace Project.Pages.Admin
{
    public class DetailRoom : PageModel
    {
        public Events events = new Events();
        public CalendarListEntry? Rooms { get; set;}
         public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id ==null)
            {
                return NotFound();
            }
            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            //alendarListEntry  roomToModify = null;
            foreach (CalendarListEntry room in calendarList.Items)
            {
                if (room.Id== id)
                {
                    Rooms = room;
                    events  = service.Events.List(room.Id).Execute();
                    break;
                }
            }

            if (events == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}