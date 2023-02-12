using Microsoft.EntityFrameworkCore;
using System.Xml;
using TARge21Shop.Core.Domain;

namespace TARge21Shop.Data
{
    public class TARge21ShopContext : DbContext
    {
        public TARge21ShopContext(DbContextOptions<TARge21ShopContext> options) 
        : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=laptop-chli9elt\\t6nis;Initial Catalog=Targe21_Shop;Integrated Security=True");
        }
    }


}   
