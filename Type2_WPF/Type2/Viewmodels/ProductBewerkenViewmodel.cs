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
    public class ProductBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        //Er moet een constructor zijn zonder parameters om het window te laten sluiten.
        public ProductBewerkenViewmodel() { }
        public ProductBewerkenViewmodel(Product selectedProduct) 
        { 
            SelectedProduct = selectedProduct;
            Categorieen = new ObservableCollection<Categorie>(_unitOfWork.CategorieRepo.Ophalen());
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

 
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        private Categorie _geselecteerdeCategorie;
        private ObservableCollection<Categorie> _categorieen;
        public Action Close { get; set; }

        private Product product;

        public Product SelectedProduct
        {
            get { return product; }
            set { product = value; }
        }


        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public Categorie GeselecteerdeCategorie
        {
            get { return _geselecteerdeCategorie; }
            set { _geselecteerdeCategorie = value; }
        }

        public ObservableCollection<Categorie> Categorieen
        {
            get { return _categorieen; }
            set { _categorieen = value; }
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
            if(SelectedProduct != null)
            {
                SelectedProduct.CategorieId = GeselecteerdeCategorie.CategorieId;

                if (SelectedProduct.IsGeldig())
                {
                    _unitOfWork.ProductRepo.Aanpassen(SelectedProduct);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Product is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een product selecteren";
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
