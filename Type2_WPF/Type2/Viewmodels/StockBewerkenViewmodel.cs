using dal.Data.UnitOfWork;
using dal;
using models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Interfaces;
using System.Collections.ObjectModel;

namespace wpf.Viewmodels
{
    public class StockBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public StockBewerkenViewmodel() { }
        public StockBewerkenViewmodel(Stock geselecteerdeStock) 
        {
            GeselecteerdeStock= geselecteerdeStock;
            Locaties = new ObservableCollection<Locatie>(_unitOfWork.LocatieRepo.Ophalen());
            Producten = new ObservableCollection<Product>(_unitOfWork.ProductRepo.Ophalen());

        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

       
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        private Stock _geselecteerdeStock;
        private Product _geselecteerdProduct;
        private Locatie _geselecteerdeLocatie;
        private ObservableCollection<Product> _producten;
        private ObservableCollection<Locatie> _Locaties;
        public Action Close { get; set; }

        public Stock GeselecteerdeStock
        {
            get { return _geselecteerdeStock; }
            set { _geselecteerdeStock = value;}
        }

        public Product GeselecteerdProduct
        {
            get { return _geselecteerdProduct; }
            set { _geselecteerdProduct = value;}
        }

        public Locatie GeselecteerdeLocatie
        {
            get { return _geselecteerdeLocatie;}
            set
            {
                _geselecteerdeLocatie = value;
            }
        }

        public ObservableCollection<Locatie> Locaties
        {
            get { return _Locaties; }
            set { _Locaties = value; }
        }

        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; }
        }
        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }



        
        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Annuleren": return true;
                case "Opslaan": return true;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Annuleren": Annuleren(); break;
                case "Opslaan": Opslaan(); break;
            };
        }

        private void Opslaan()
        {
            if (GeselecteerdeStock != null)
            {

                if (GeselecteerdeStock.IsGeldig())
                {
                    _unitOfWork.StockRepo.Aanpassen(GeselecteerdeStock);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Stock is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een Stock selecteren";
            }
        }

        private void FoutmeldingInstellenNaSave(int ok, string melding)
        {
            if (ok > 0)
            {
                Close?.Invoke();
            }
            else
            {
                Foutmelding = melding;
            }
        }

        private void Annuleren()
        {

            Close?.Invoke();

        }



        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
        public override string this[string columnName]
        {
            get
            {

                return "";
            }
        }
    }
}
