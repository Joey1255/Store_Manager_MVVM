using dal;
using dal.Data.UnitOfWork;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf.Viewmodels
{
    public class StockAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _aantal;
        private string _productId;
        private string _locatieId;
        private string _foutmelding;
        private Product _geselecteerdeProduct;
        private Locatie _geselecteerdeLocatie;
        private ObservableCollection<Product> _producten;
        private ObservableCollection<Locatie> _locaties;
        public Stock StockRecord { get; set; }

        public StockAanmakenViewmodel()
        {
            Producten = new ObservableCollection<Product>(_unitOfWork.ProductRepo.Ophalen());
            Locaties = new ObservableCollection<Locatie>(_unitOfWork.LocatieRepo.Ophalen());
            StockRecordInstellen();
        }

        public string Aantal
        {
            get { return _aantal; }
            set { _aantal = value; }
        }

        public string ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public string LocatieId
        {
            get { return _locatieId; }
            set { _locatieId = value; }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public Product GeselecteerdProduct
        {
            get { return _geselecteerdeProduct; }
            set { _geselecteerdeProduct = value; }
        }

        public Locatie GeselecteerdeLocatie
        {
            get { return _geselecteerdeLocatie; }
            set
            {
                _geselecteerdeLocatie = value;
            }
        }

        public ObservableCollection<Locatie> Locaties
        {
            get { return _locaties; }
            set { _locaties = value; }
        }

        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; }
        }
        
        public override string this[string columnName]
        {
            get { return ""; }
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
            if (this.IsGeldig())
            {
                StockRecord.LocatieId = GeselecteerdeLocatie.LocatieId;
                StockRecord.ProductId = GeselecteerdProduct.ProductId;

                if (StockRecord.IsGeldig())
                {
                    _unitOfWork.StockRepo.ToevoegenOfAanpassen(StockRecord);
                    int ok = _unitOfWork.Save();
                    if (ok < 0)
                    {
                        Foutmelding = StockRecord.Error;
                        MessageBox.Show(Foutmelding);

                    }
                }
                else
                {
                    Foutmelding = "Stock is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += StockRecord.Error;
                    MessageBox.Show(Foutmelding);
                }
                Annuleren();

            }
            
        }

        private void Annuleren()
        {
            StockRecord.Aantal = 0;
            GeselecteerdeLocatie = null;
            GeselecteerdProduct = null;
            Foutmelding = "";
        }

        private void StockRecordInstellen()
        {
            StockRecord = new Stock();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
