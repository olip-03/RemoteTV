using System.Runtime.InteropServices;
using RemoteTV;

var builder = WebApplication.CreateBuilder(args);

// Load all settings into memory
ProgramSettings.UpdateFolders();
if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    ProgramSettings.runtime = "Windows";
}

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("AuthCookie").AddCookie("AuthCookie", options => {
    options.Cookie.Name = "AuthCookie";
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});
builder.Services.AddAuthorization(options => {
    options.AddPolicy("IsAuthenticated", 
        policy => policy.RequireClaim("IsAuthenticated", "true"));
});

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