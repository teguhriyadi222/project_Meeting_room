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
    public class Booking : PageModel
    {
        public SelectList? Rooms { get; set;}
        [BindProperty(SupportsGet = true)]
        public string Title { get; set;}
        [BindProperty(SupportsGet = true)]
        public string RoomId { get; set;}
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set;}
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set;}
        [BindProperty(SupportsGet = true)]
        public string Atendee { get; set;}
        [BindProperty(SupportsGet = true)]
        public string Description { get; set; }
        private string Id { get; set; }

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
          

            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            foreach (CalendarListEntry calendar in calendarList.Items)
            {
                if (calendar.Summary == RoomId)
                {
                    Id = calendar.Id;
                    break;
                }
            }


            Event newEvent = new Event
            {
                Summary = Title,
                Start = new EventDateTime
                {
                    DateTime = DateTime.Parse(StartDate.ToString("yyyy-MM-dd HH:mm")),
                    TimeZone = "GMT+7"
                },
                End = new EventDateTime
                {
                    DateTime = DateTime.Parse(EndDate.ToString("yyyy-MM-dd HH:mm")),
                    TimeZone = "GMT+7"
                },
                Attendees = new List<EventAttendee>()
                {
                    new EventAttendee()
                    {
                        Email = Atendee,
                    }
                },
                Description = Description,
                Location = "Jakarta"
            };
            service.Events.Insert(newEvent, Id).Execute();

            return RedirectToPage("/Admin/Index");
        } 
    }
}