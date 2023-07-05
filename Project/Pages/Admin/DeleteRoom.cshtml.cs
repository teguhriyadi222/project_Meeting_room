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
    public class DeleteRoom : PageModel
    {
        public CalendarListEntry? Rooms { get; set;}
        public string? RoomId {get; set;}
        public string Id {get; set;}
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
                    break;
                }
            }

            if (Rooms == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            //alendarListEntry  roomToModify = null;
            foreach (CalendarListEntry room in calendarList.Items)
            {
                if (room.Id == id)
                {
                    Rooms = room;
                    break;
                }
            }

            if (Rooms == null)
            {
                return NotFound();
            }
            service.Calendars.Delete(Rooms.Id).Execute();
            return RedirectToPage("/Admin/SelectRoom");
        }
    }
}