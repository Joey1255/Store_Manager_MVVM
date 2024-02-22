using dal.Data.Repositories;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private IRepository<Klant> _klantRepo;
        private IRepository<Product> _productRepo;
        private IRepository<Locatie> _locatieRepo;
        private IRepository<Stock> _stockRepo;
        private IRepository<Categorie> _categorieRepo;
        private IRepository<Order> _orderRepo;
        private IRepository<Factuur> _factuurRepo;
        private IRepository<Medewerker> _medewerkerRepo;
        private IRepository<OrderProduct> _orderProductRepo;
        private IRepository<Dienst> _dienstRepo;
        private IRepository<OrderDienst> _orderDienstRepo;
        private IRepository<Kortingskaart> _kortingskaartRepo;

        public UnitOfWork(Type2Context ctx)
        {
            Context = ctx;
        }

        private Type2Context Context { get; }

        public IRepository<Klant> KlantRepo
        {
            get
            {
                if (_klantRepo == null)
                {
                    _klantRepo = new Repository<Klant>(Context);
                }
                return _klantRepo;
            }
        }
        public IRepository<Product> ProductRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new Repository<Product>(Context);
                }
                return _productRepo;
            }
        }
        public IRepository<Locatie> LocatieRepo
        {
            get
            {
                if (_locatieRepo == null)
                {
                    _locatieRepo = new Repository<Locatie>(Context);
                }
                return _locatieRepo;
            }
        }

        public IRepository<Stock> StockRepo
        {
            get
            {
                if (_stockRepo == null)
                {
                    _stockRepo = new Repository<Stock>(Context);
                }
                return _stockRepo;
            }
        }

        public IRepository<Categorie> CategorieRepo
        {
            get
            {
                if (_categorieRepo == null)
                {
                    _categorieRepo = new Repository<Categorie>(Context);
                }
                return _categorieRepo;
            }
        }

        public IRepository<Order> OrderRepo
        {
            get
            {
                if (_orderRepo == null)
                {
                    _orderRepo = new Repository<Order>(Context);
                }
                return _orderRepo;
            }
        }
        public IRepository<Factuur> FactuurRepo
        {
            get
            {
                if (_factuurRepo == null)
                {
                    _factuurRepo = new Repository<Factuur>(Context);
                }
                return _factuurRepo;
            }
        }

        public IRepository<Medewerker> MedewerkerRepo
        {
            get
            {
                if (_medewerkerRepo == null)
                {
                    _medewerkerRepo = new Repository<Medewerker>(Context);
                }
                return _medewerkerRepo;
            }
        }
        public IRepository<OrderProduct> OrderProductRepo
        {
            get
            {
                if (_orderProductRepo == null)
                {
                    _orderProductRepo = new Repository<OrderProduct>(Context);
                }
                return _orderProductRepo;
            }
        }

        public IRepository<Dienst> DienstRepo
        {
            get
            {
                if (_dienstRepo == null)
                {
                    _dienstRepo = new Repository<Dienst>(Context);
                }
                return _dienstRepo;
            }
        }

        public IRepository<OrderDienst> OrderDienstRepo
        {
            get
            {
                if (_orderDienstRepo == null)
                {
                    _orderDienstRepo = new Repository<OrderDienst>(Context);
                }
                return _orderDienstRepo;
            }
        }

        public IRepository<Kortingskaart> KortingskaartRepo
        {
            get
            {
                if (_kortingskaartRepo == null)
                {
                    _kortingskaartRepo = new Repository<Kortingskaart>(Context);
                }
                return _kortingskaartRepo;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}
