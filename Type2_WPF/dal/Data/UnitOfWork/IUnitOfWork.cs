using dal.Data.Repositories;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Klant> KlantRepo { get; }
        IRepository<Product> ProductRepo { get; }
        IRepository<Locatie> LocatieRepo { get; }
        IRepository<Stock> StockRepo { get; }
        IRepository<Categorie> CategorieRepo { get; }
        IRepository<Order> OrderRepo { get; }
        IRepository<Factuur> FactuurRepo { get; }
        IRepository<Medewerker> MedewerkerRepo { get; }
        IRepository<OrderProduct> OrderProductRepo { get; }
        IRepository<Dienst> DienstRepo { get; }
        IRepository<OrderDienst> OrderDienstRepo { get; }
        IRepository<Kortingskaart> KortingskaartRepo { get; }

        int Save();
    }
}
