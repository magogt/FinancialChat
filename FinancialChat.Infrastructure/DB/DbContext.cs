﻿using Microsoft.EntityFrameworkCore;
using FinancialChat.Core.Entities;

namespace FinancialChat.Infrastructure.DB
{
  public class DbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {

    }

    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage").Property(x => x.Id).ValueGeneratedOnAdd();
      modelBuilder.Entity<ChatRoom>().ToTable("ChatRoom").Property(x => x.Id).ValueGeneratedOnAdd();
    }
  }
}