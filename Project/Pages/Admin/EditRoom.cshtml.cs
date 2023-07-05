using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Project.Pages.Admin
{
    public class EditRoom : PageModel
    {
        private readonly ILogger<EditRoom> _logger;

        public EditRoom(ILogger<EditRoom> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}