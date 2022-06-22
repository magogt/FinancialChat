using Microsoft.EntityFrameworkCore;
using FinancialChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FinancialChat.Infrastructure.DB
{
  public class ChatDbContext : IdentityDbContext<IdentityUser>
  {
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }

    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage").Property(x => x.Id).ValueGeneratedOnAdd();
      modelBuilder.Entity<ChatRoom>().ToTable("ChatRoom").Property(x => x.Id).ValueGeneratedOnAdd();
    }
  }
}
