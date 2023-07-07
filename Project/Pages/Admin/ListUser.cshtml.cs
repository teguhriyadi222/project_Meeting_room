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
    }
}
