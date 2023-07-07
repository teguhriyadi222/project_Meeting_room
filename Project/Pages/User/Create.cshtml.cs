using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserData;
using UserContext;

namespace Project.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly UserConteks _context;

        public CreateModel(UserConteks context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserLogin UserLogin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserLogin == null || UserLogin == null)
            {
                return Page();
            }

            _context.UserLogin.Add(UserLogin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
