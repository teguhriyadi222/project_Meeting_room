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
        public Calendar Rooms = new Calendar();
        public string? RoomId {get; set;}
        public string Id {get; set;}
        [BindProperty]
        public string? Name {get; set;}
        [BindProperty]
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
                    Rooms.ETag = room.ETag;
                    Rooms.Kind = room.Kind;
                    break;
                }
            }

            if (Id == null)
            {
                return NotFound();
            }
            
            Rooms.Id = Id;
            Rooms.Summary = Name;
            Rooms.Description = Description;
            Console.WriteLine("summary = " + Name +" ||"+ Rooms.Summary );
            Console.WriteLine("description = " + Description + "||" + Rooms.Description );
            service.Calendars.Update(Rooms, Id).Execute();
            return RedirectToPage("/Admin/SelectRoom");
        }
    }
}