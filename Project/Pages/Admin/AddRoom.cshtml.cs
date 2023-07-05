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

        public async Task<IActionResult> OnPostAsync()
        {

            var service = GoogleCredential.CreateCredential();
            CalendarList calendarList = service.CalendarList.List().Execute();
            foreach (CalendarListEntry calendar in calendarList.Items)
            {
                if (calendar.Summary == Name)
                {
                    return RedirectToPage("/Admin/");
                }
            }


            Calendar newRoom = new Calendar();
            newRoom.Summary = Name;
            newRoom.Description = Description;
            service.Calendars.Insert(newRoom).Execute();

            return RedirectToPage("/Admin/Index");
        } 
    }
}