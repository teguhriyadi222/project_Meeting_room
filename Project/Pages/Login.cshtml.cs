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
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("Role", user.Role);
                    TempData["UserName"] = user.UserName;
                    TempData["Role"] = user.Role;
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
