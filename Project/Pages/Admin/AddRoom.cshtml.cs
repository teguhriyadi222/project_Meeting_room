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
    public class AddRoom : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Name { get; set;}
        [BindProperty(SupportsGet = true)]
        public string Description { get; set;}

        public string Notifications { get; set;}

        public async Task<IActionResult> OnPostAsync()
        {

            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            foreach (CalendarListEntry calendar in calendarList.Items)
            {
                if (calendar.Summary == Name)
                {
                    TempData["Notifications"] = "Error: Room with the same name alredy exists";
                    TempData.Peek("Notifications");
                    Notifications = (String)TempData["Notifications"];
                    Console.WriteLine(TempData["Notifications"]);
                    Console.WriteLine("Room :" + Notifications);
                    return Page();
                }
            }

            Calendar newRoom = new Calendar();
            newRoom.Summary = Name;
            newRoom.Description = Description;
            service.Calendars.Insert(newRoom).Execute();
            TempData["Notifications"] = "Room has been created successfully";
            TempData.Peek("Notifications");
            Notifications = (String)TempData["Notifications"];
            return Page();
        } 
       
    }
}