using Microsoft.EntityFrameworkCore;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal
{
    public class Type2Context: DbContext
    {
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Product> Producten { get; set;}
        public DbSet<Locatie> Locaties { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Categorie> Categorieën { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Factuur> Facturen { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<OrderProduct> OrderProducten { get; set; }
        public DbSet<Kortingskaart> Kortingskaarten { get; set; }
        public DbSet<Dienst> Diensten { get; set; }
        public DbSet<OrderDienst> OrderDiensten { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Type2;Trusted_Connection=True;");
        }
    }
}
