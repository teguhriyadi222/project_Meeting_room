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
    public class IndexModel : PageModel
    {
        private readonly UserConteks _context;

        public IndexModel(UserConteks context)
        {
            _context = context;
        }

        public IList<UserLogin> UserLogin { get;set; } = default!;

	[BindProperty(SupportGet = true)]
	public string SearchString { get; set; }

	public SelectList? Email { get; set; }

	[BindProperty(SupportGet = true)]
	public string? UserLoginEmail { get; set; }

	
	[BindProperty(SupportGet = true)]
        public string SearchString { get; set; }

        public SelectList? Role { get; set; }

        [BindProperty(SupportGet = true)]
        public string? UserLoginRole { get; set; }

	
//	[BindProperty(SupportGet = true)]
//        public string SearchString { get; set; }

//        public SelectList? Id { get; set; }

//        [BindProperty(SupportGet = true)]
//        public string? UserLoginId { get; set; }

        public async Task OnGetAsync()
        {
		IQueryable<string> emailQuery = from m in _context.UserLogin
			orderby m.Email
			select m.Email;
		
		var userlogin = from m in _context.UserLogin
			select m;
		if (!string.IsNullOrEmpty(SearchString))
		{
			userlogin = userlogin.Where(s => s.Email.Contains(SearchString));
		}
		Emails = new SelectList(await emailQuery.Distinct().ToListAsync());

		UserLogin = await userlogin.ToListAsync();

		
//            if (_context.UserLogin != null)
//            {
//                UserLogin = await _context.UserLogin.ToListAsync();
//            }
        }
    }
}
