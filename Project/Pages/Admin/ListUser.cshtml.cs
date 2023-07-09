using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserData;

namespace ListUser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public IndexModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> Users { get; set; }

        public void OnGet()
        {
            Users = _userRepository.GetAllUsers();
        }

        [BindProperty]
        public User NewUser { get; set; }
        public bool IsUserDeleted { get; set; }

        [HttpPost]
        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                _userRepository.CreateUser(NewUser);
                return RedirectToPage();
            }

            return Page();
        }
        [HttpPost]
        public IActionResult OnPostDelete(int userId)
        {
            if(ModelState.IsValid)
            {
            _userRepository.DeleteUser(userId);
            return RedirectToPage();

            }
            return Page();
        }

        [HttpPost]
        public IActionResult OnPostUpdateRole(User updatedUser)
        {
            var user = _userRepository.GetUser(updatedUser.UserId);
            if (user != null)
            {
                user.UserName = updatedUser.UserName;
                user.Password = updatedUser.Password;
                user.Role = updatedUser.Role;

                _userRepository.UpdateUser(user);
                return RedirectToPage();
            }

            return NotFound();
        }

    }
}
