using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Reflection;
using models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows;

namespace wpf.Viewmodels
{
    public class ProductAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private ObservableCollection<Categorie> _categorieen;
        private string _foutmelding;

        public Product ProductRecord { get; set; }

        public ProductAanmakenViewmodel() {
            Categorieen = new ObservableCollection<Categorie>(_unitOfWork.CategorieRepo.Ophalen());
            ProductRecordInstellen();
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public ObservableCollection<Categorie> Categorieen
        {
            get { return _categorieen; }
            set { _categorieen = value;}
        }       


        public override string this[string columnName]
        {
            get
            {
                if (columnName == "ProductRecord.Prijs" && !decimal.TryParse(ProductRecord.Prijs.ToString(), out decimal prijs))
                {
                    return "Prijs moet een numerieke waarde zijn!" + Environment.NewLine;
                }

                return "";
            }
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

            if (ProductRecord.IsGeldig())
            {
                _unitOfWork.ProductRepo.ToevoegenOfAanpassen(ProductRecord);
                int ok = _unitOfWork.Save();
                if (ok < 0)
                {
                    Foutmelding = ProductRecord.Error;
                    MessageBox.Show(Foutmelding);

                }
            }
            else
            {
                Foutmelding = "Categorie is niet toegevoegd";
                Foutmelding += ProductRecord.Error;
                MessageBox.Show(Foutmelding);
            }
            Annuleren();
        }
        private void Annuleren()
        {
            ProductRecord.Productnummer = "";
            ProductRecord.Naam = "";
            ProductRecord.Beschrijving = "";
            ProductRecord.Prijs = 0;
            ProductRecord.CategorieId = -1 ;
        }

        private void ProductRecordInstellen()
        {
            ProductRecord = new Product();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
