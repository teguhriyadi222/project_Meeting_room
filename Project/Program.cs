using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserData;

namespace Meeting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddRazorPages(options =>
                        {
                            options.RootDirectory = "/Pages";
                            options.Conventions.AddPageRoute("/Login", "");
                        });

                        services.AddSession(options =>
                        {

                        });
                        services.AddSingleton<UserRepository>();
                    })
                    .Configure((hostContext, app) =>
                    {
                        var env = hostContext.HostingEnvironment;

                        if (!env.IsDevelopment())
                        {
                            app.UseExceptionHandler("/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseSession();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapRazorPages();
                            endpoints.MapPost("/Index/DeleteUser", async context =>
                            {
                                var userRepository = context.RequestServices.GetRequiredService<UserRepository>();
                                var userId = int.Parse(context.Request.Form["userId"]);
                                userRepository.DeleteUser(userId);
                                context.Response.Redirect("/Index");
                            });
                        });
                        // CreateUser(app);
                    });
                })
                .Build();

            host.Run();
        }
        // private static void CreateUser(IApplicationBuilder app)
        // {
        //     var user = app.ApplicationServices.GetRequiredService<UserRepository>();

        //     User newUser = new User
        //     {
        //         UserName = "Shella",
        //         Password = "12345",
        //         Role = "Admin",
        //     };

        //     user.CreateUser(newUser);
        // }
    }
}
