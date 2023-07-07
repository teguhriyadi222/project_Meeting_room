using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }
}
