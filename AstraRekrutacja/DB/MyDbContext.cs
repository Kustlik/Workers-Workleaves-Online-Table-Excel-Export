using AstraRekrutacja.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.DB
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContext") { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Workleave> Workleaves { get; set; }
        public DbSet<WorkleaveType> WorkleaveTypes { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}