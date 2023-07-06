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
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == Username && u.Password == Password);
                if (user != null)
                {
                    ViewData["Username"] = user.UserName;
                    ViewData["Role"] = user.Role;

                    if (user.Role == "Admin")
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else
                    {
                        return RedirectToPage("/User/Index");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Username atau password salah.";
                    return Page();
                }
            }
        }

    }
}
