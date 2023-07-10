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
    public class SelectRoom : PageModel
    {
        public IEnumerable<CalendarListEntry> Rooms { get; set;}
        public string? RoomId {get; set;}
        public string Id {get; set;}
        public async Task OnGetAsync()
        {
            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            IEnumerable<CalendarListEntry> roomList = calendarList.Items.Where(c => c.Description != null);
            Rooms = roomList.Where(c => c.Description.ToLower().Contains("ruang"));
            //IEnumerable<string> room = roomList.Select(c => c.Summary).Order();

        }
    }
}