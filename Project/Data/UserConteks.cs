using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserData;
namespace UserContext
{
    public class UserConteks : DbContext
    {
        public UserConteks (DbContextOptions<UserConteks> options)
            : base(options)
        {
        }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source = User.db");
	}

        public DbSet<UserData.UserLogin> UserLogin { get; set; } = default!;
    }
}
