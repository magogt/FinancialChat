
using FinancialChat.Bussiness.ChatBot;
using FinancialChat.Bussiness.Services;
using FinancialChat.Hubs;
using FinancialChat.Infrastructure.Repositories;
using FinancialChat.State;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FinancialChat.Infrastructure.DB;
using FinancialChat.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
  opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ChatDbContext>();
builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ChatApp.SQLite"), b => b.MigrationsAssembly("FinancialChat.Infrastructure")));

builder.Services.AddRazorPages();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddRepositories();
builder.Services.AddScoped<StockDataMessageProducer>();
builder.Services.AddScoped<FinancialChatHubService>();
builder.Services.AddScoped<AppState>();

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
app.MapBlazorHub();
app.MapHub<FinancialChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
