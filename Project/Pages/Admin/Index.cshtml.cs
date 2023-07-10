using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GoogleAuth;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Pages.Admin
{
    public class Index : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set;}
        public SelectList? Rooms { get; set;}
        [BindProperty(SupportsGet = true)]
        public string? Room { get; set;}
        public string ListCalendar = "&src=id.indonesian%23holiday%40group.v.calendar.google.com";


        public async Task OnGetAsync()
        {
            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            IEnumerable<CalendarListEntry> roomList = calendarList.Items.Where(c => c.Description != null);
            roomList = roomList.Where(c => c.Description.ToLower().Contains("ruang"));

            if (!string.IsNullOrEmpty(Room))
            {
                // filter based on size
                foreach (CalendarListEntry room in roomList)
                {
                    if (room.Description.ToLower().Contains(Room.ToLower()))
                    {
                        ArrangeLink(room.Id);
                    }
                }
            }
            if(!string.IsNullOrEmpty(SearchString))
            {
                // filter 
                foreach (CalendarListEntry room in roomList)
                {
                    if (room.Description.ToLower().Contains(SearchString.ToLower()) || room.Summary.ToLower().Contains(SearchString.ToLower()))
                    {
                        ArrangeLink(room.Id);
                    }
                }
            }
            if (string.IsNullOrEmpty(Room)&&string.IsNullOrEmpty(SearchString)) {
                foreach (CalendarListEntry room in roomList)
                {
                    ArrangeLink(room.Id);
                }
            }

        }

        public void ArrangeLink(string link)
        {
            string src = "&src=";
            ListCalendar = ListCalendar + src + link;
        }
    }
    
}