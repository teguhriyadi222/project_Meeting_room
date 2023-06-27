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

        public IActionResult OnPost()
        {
            // Logika autentikasi di sini (verifikasi username dan password)

            if (Username == "admin" && Password == "admin123")
            {
                // Autentikasi berhasil, redirect ke halaman lain
                return RedirectToPage("/Home");
            }
            else
            {
                // Autentikasi gagal, tampilkan pesan kesalahan
                ModelState.AddModelError(string.Empty, "Username atau password salah.");
                return Page();
            }
        }
    }
}
