using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Login.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            // Menghapus pesan error dari TempData saat memuat halaman
            TempData.Remove("ErrorMessage");
            return Page();
        }

        public IActionResult OnPost()
        {
            using (var conteks = new UserContext())
            {
                return RedirectToPage("/Privacy");
            }
            else
            {
                // Menyimpan pesan error ke TempData
                TempData["ErrorMessage"] = "Username atau password salah.";
                return Page();
            }
        }
    }
}
