using Microsot.EntityFrameworkCore;
using UserContext;

namespace UserData;

public static class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using (var context = new UserConteks(
					serviceProvider.GetRequiredService<DbContextOptions<UserConteks>>()))
		{
			if (context == null || context.UserLogin == null)
			{
				throw new ArgumentNullException("Null UserConteks");
			}

			if (context.UserLogin.Any())
			{
				return;
			}

			context.UserLogin.AddRange(
					new UserLogin
					{
					Id = 1,
					Email = "Teguh",
					Password = "123",
					Role = "Admin"
					},

					new UserLogin
					{
					Id = 2,
					Email = "Adam",
					Password = "!23",
					Role "User"
					}
					);
			context.SaveChanges();
		}
	}
}
