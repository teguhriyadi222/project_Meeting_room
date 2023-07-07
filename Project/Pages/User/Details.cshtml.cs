using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserData;
using UserContext;

namespace Project.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly UserConteks _context;

        public DetailsModel(UserConteks context)
        {
            _context = context;
        }

      public UserLogin UserLogin { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserLogin == null)
            {
                return NotFound();
            }

            var userlogin = await _context.UserLogin.FirstOrDefaultAsync(m => m.Id == id);
            if (userlogin == null)
            {
                return NotFound();
            }
            else 
            {
                UserLogin = userlogin;
            }
            return Page();
        }
    }
}
