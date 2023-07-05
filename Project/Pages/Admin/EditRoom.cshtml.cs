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
    public class EditRoom : PageModel
    {
        public CalendarListEntry Rooms = new CalendarListEntry();
        public string? RoomId {get; set;}
        public string Id {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
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
                    Name = room.Summary;
                    Description = room.Description;
                    Id = room.Id;
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
                if (room.Id == id)
                {
                    Id = room.Id;
                    Rooms = room;
                    break;
                }
            }

            if (Id == null)
            {
                return NotFound();
            }
            Rooms.Summary = Name;
            Rooms.Description = Description;
            service.CalendarList.Update(Rooms, Id).Execute();
            return RedirectToPage("/Admin/SelectRoom");
        }
    }
}