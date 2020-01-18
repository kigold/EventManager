using EventManagement.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.Context
{
    public class EventMgtDbContext : IdentityDbContext<IdentityUser>
    {
        public EventMgtDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // add your own configuration here
            modelBuilder.ApplyConfiguration(new EventMap());
        }

        public DbSet<Event> Events { get; set; }
    }
}
