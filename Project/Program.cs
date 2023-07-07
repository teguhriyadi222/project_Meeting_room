using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserContext;
using UserData;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserConteks>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UserConteks") ?? throw new InvalidOperationException("Connection string 'UserConteks' not found.")));

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = "/Pages";
    options.Conventions.AddPageRoute("/Login", "");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
