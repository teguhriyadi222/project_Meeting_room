using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserData;
using UserContext;

namespace Project.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly UserConteks _context;

        public EditModel(UserConteks context)
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

            var userlogin =  await _context.UserLogin.FirstOrDefaultAsync(m => m.Id == id);
            if (userlogin == null)
            {
                return NotFound();
            }
            UserLogin = userlogin;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(UserLogin.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserLoginExists(int id)
        {
          return (_context.UserLogin?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
