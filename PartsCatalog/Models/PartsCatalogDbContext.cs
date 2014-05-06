﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
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
        }
    }
}