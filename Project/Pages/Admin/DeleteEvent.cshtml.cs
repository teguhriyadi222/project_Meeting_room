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
    public class DeleteEvent : PageModel
    {
        public Events events = new Events();
        public string eventName { get; set; }
        public string eventId { get; set; }
        public string calId;
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
                foreach (var e in service.Events.List(room.Id).Execute().Items)
                {
                    if (e.Id == id)
                    {
                        eventId = e.Id;
                        eventName = e.Summary;
                        calId = room.Id;

                    }
                    break;
                }
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
                foreach (var e in service.Events.List(room.Id).Execute().Items)
                {
                    if (e.Id == id)
                    {
                        eventId = e.Id;
                        eventName = e.Summary;
                        calId = room.Id;

                    }
                    break;
                }
            }
            service.Events.Delete(calId,eventId).Execute();
            return RedirectToPage("/Admin/SelectRoom");
        }
    }
}