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
    public class DeleteModel : PageModel
    {
        private readonly UserConteks _context;

        public DeleteModel(UserConteks context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserLogin == null)
            {
                return NotFound();
            }
            var userlogin = await _context.UserLogin.FindAsync(id);

            if (userlogin != null)
            {
                UserLogin = userlogin;
                _context.UserLogin.Remove(UserLogin);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
