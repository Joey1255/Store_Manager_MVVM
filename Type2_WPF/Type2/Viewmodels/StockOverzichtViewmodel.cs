using dal.Data.UnitOfWork;
using dal;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Views;

namespace wpf.Viewmodels
{
    public class StockOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Stock> Stocks { get; set; }
        public Stock StockRecord { get; set; }
        private string _foutmelding;
        private string _zoekterm;
        private Stock _geselecteerdeStock;

        public StockOverzichtViewmodel()
        {
            Stocks = new ObservableCollection<Stock>(_unitOfWork.StockRepo.Ophalen(x => x.Locatie, y => y.Product));
            StockRecordInstellen();
        }
        public Stock GeselecteerdeStock
        {
            get { return _geselecteerdeStock; }
            set 
            { 
                _geselecteerdeStock = value;
                StockRecordInstellen();
            }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public string Zoekterm
        {
            get { return _zoekterm; }
            set 
            { 
                _zoekterm = value;
                Refresh();
            }
        }

        
        public override string this[string columnName]
        {
            get
            {

                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": return GeselecteerdeStock != null ? true : false;
                case "Bewerken": return GeselecteerdeStock != null ? true : false;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "Bewerken": Bewerken(); break;
            };
        }

        private void Verwijderen()
        {
            if (GeselecteerdeStock != null)
            {
                _unitOfWork.StockRepo.Verwijderen(GeselecteerdeStock.StockId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Stock is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Stock selecteren";
            }
        }

        private void Bewerken()
        {
            var vm = new StockBewerkenViewmodel(GeselecteerdeStock);
            var view = new StockBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void FoutmeldingInstellenNaSave(int ok, string melding)
        {
            if (ok > 0)
            {
                Refresh();
                Resetten();
            }
            else
            {
                Foutmelding = melding;
            }
        }

        private void Resetten()
        {
            if (this.IsGeldig())
            {
                GeselecteerdeStock = null;
                StockRecordInstellen();
                Foutmelding = "";
            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        private void Refresh()
        {
            List<Stock> stockLijst = _unitOfWork.StockRepo.Ophalen(x => x.Locatie.Naam.Contains(Zoekterm) || x.Product.Naam.Contains(Zoekterm), y => y.Product).ToList();
            Stocks = new ObservableCollection<Stock>(stockLijst);
        }

        private void StockRecordInstellen()
        {
            if (GeselecteerdeStock != null)
            {
                StockRecord = GeselecteerdeStock;
            }
            else
            {
                StockRecord = new Stock();
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
