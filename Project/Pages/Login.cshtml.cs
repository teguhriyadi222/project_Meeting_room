using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserContext;

namespace Login.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

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
            using (var conteks = new UserConteks())
            {
                var user = conteks.UserLogin.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user != null)
                {
                    ViewData["Email"] = user.Email;
                    ViewData["Role"] = user.Role;

                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.Role);
                    return RedirectToPage("/Admin/Index");
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
}
