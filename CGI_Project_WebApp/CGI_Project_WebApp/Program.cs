using Auth0.AspNetCore.Authentication;
using CGI_Project_WebApp_Core;
using CGI_Project_WebApp_DAL;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.Scope = "openid profile email";
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Account/Logout");

    options.Conventions.AuthorizePage("/Account/Profile");

    options.Conventions.AuthorizePage("/Catalog");
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<LeaderboardService>();
builder.Services.AddScoped<IPollRepository, MockPollRepo>();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
