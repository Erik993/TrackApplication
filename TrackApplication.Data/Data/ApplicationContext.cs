using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplicationData.DbContextData
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if statement to make test possible with EF InMemory
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer
                ("Server=(localdb)\\mssqllocaldb;Database=TrackApplication;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
            

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<ITSupport> ITSupports { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Assignment> Assignments { get; set; } = null!;
    }
}
