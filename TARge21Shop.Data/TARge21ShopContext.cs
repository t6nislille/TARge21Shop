﻿using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Domain.Car;

namespace TARge21Shop.Data
{
    public class TARge21ShopContext : DbContext
    {
        public TARge21ShopContext(DbContextOptions<TARge21ShopContext> options) 
        : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=laptop-chli9elt\\t6nis;Initial Catalog=TARge21_Test;Integrated Security=True");
        }
    }


}   
