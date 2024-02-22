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
    public class ProductOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Product> Producten { get; set; }
        public Product ProductRecord { get; set; }
        private string _foutmelding;        
        private string _zoekterm;

        public ProductOverzichtViewmodel()
        {
            Producten = new ObservableCollection<Product>(_unitOfWork.ProductRepo.Ophalen(x => x.Categorie));
            ProductRecordInstellen();
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                ProductRecordInstellen();
            }
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

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
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
                case "Verwijderen": return SelectedProduct != null ? true : false;
                case "Bewerken": return SelectedProduct != null ? true : false;
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

        private void Bewerken()
        {
            var vm = new ProductBewerkenViewmodel(SelectedProduct);
            var view = new ProductBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedProduct != null)
            {
                _unitOfWork.ProductRepo.Verwijderen(SelectedProduct.ProductId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Product is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Product selecteren";
            }
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
                SelectedProduct = null;
                ProductRecordInstellen();
                Foutmelding = "";
            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        private void ProductRecordInstellen()
        {
            if(SelectedProduct != null)
            {
                ProductRecord = SelectedProduct;
            }
            else
            {
                ProductRecord = new Product();
            }
        }
        private void Refresh()
        {
            List<Product> lijstProducten = _unitOfWork.ProductRepo.Ophalen(x => x.Categorie.Naam.Contains(Zoekterm) || x.Categorie.Beschrijving.Contains(Zoekterm) 
            || x.Productnummer.Contains(Zoekterm)).ToList();
            Producten = new ObservableCollection<Product>(lijstProducten);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
