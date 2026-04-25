using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HW_Academy_Razor_Pages.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContextFactory<HW_Academy_Razor_PagesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HW_Academy_Razor_PagesContext") ?? throw new InvalidOperationException("Connection string 'HW_Academy_Razor_PagesContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapRazorComponents<App>();

app.Run();
