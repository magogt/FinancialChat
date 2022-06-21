
using FinancialChat.Hubs;
using FinancialChat.Infrastructure.Repositories;
using FinancialChat.State;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
  opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      new[] { "application/octet-stream" });
});

builder.Services.AddDbContext<FinancialChat.Infrastructure.DB.ChatDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ChatApp.SQLite"), b => b.MigrationsAssembly("FinancialChat.Infrastructure")));

builder.Services.AddRepositories();
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

app.MapBlazorHub();
app.MapHub<FinancialChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
