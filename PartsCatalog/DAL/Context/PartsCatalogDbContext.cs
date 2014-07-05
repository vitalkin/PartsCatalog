using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PartsCatalog.DAL.Context
{
    public class PartsCatalogDbContext : DbContext
    {
        static PartsCatalogDbContext()
        {
            Database.SetInitializer<PartsCatalogDbContext>(null);
        }

        public PartsCatalogDbContext()
            : base("DatabaseConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Make>();
            modelBuilder.Entity<Model>();
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Part>();
        }
    }
}