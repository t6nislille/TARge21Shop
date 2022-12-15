﻿using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain.Spaceship;

namespace TARge21Shop.Data
{
    public class TARge21ShopContext : DbContext
    {
        public TARge21ShopContext(DbContextOptions<TARge21ShopContext> options) 
        : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=laptop-chli9elt\\t6nis;Initial Catalog=TARge21Shop;Integrated Security=True");
        }
    }


}   
