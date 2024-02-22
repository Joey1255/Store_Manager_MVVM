using dal;
using dal.Data.UnitOfWork;
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
    public class CategorieOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Categorie> Categorieën { get; set; }
        public Categorie CategorieRecord { get; set; }
        private Categorie _selectedCategorie;
        private string _foutmelding;
        private string _zoekterm;

        public CategorieOverzichtViewmodel()
        {
            Categorieën = new ObservableCollection<Categorie>(_unitOfWork.CategorieRepo.Ophalen());
            CategorieRecordInstellen();
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

        public Categorie SelectedCategorie
        {
            get { return _selectedCategorie; }
            set
            {
                _selectedCategorie = value;
                CategorieRecordInstellen();
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
                case "Verwijderen": return SelectedCategorie != null ? true : false;
                case "Bewerken": return SelectedCategorie != null ? true : false;
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
            var vm = new CategorieBewerkenViewmodel(SelectedCategorie);
            var view = new CategorieBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedCategorie != null)
            {
                _unitOfWork.CategorieRepo.Verwijderen(SelectedCategorie.CategorieId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Orderlijn is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Orderlijn selecteren";
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

        private void Refresh()
        {
            List<Categorie> lijstCategorieën = _unitOfWork.CategorieRepo.Ophalen(x => x.Naam.Contains(Zoekterm) || x.Beschrijving.Contains(Zoekterm) || x.CategorieId.ToString().Contains(Zoekterm)).ToList();
            Categorieën = new ObservableCollection<Categorie>(lijstCategorieën);
        }

        private void CategorieRecordInstellen()
        {
            if(SelectedCategorie != null)
            {
                CategorieRecord = SelectedCategorie;
            }
            else
            {
                CategorieRecord = new Categorie();
            }
        }

        public void Resetten()
        {
            if (this.IsGeldig())
            {
                SelectedCategorie = null;
                CategorieRecordInstellen();
                Foutmelding = "";
            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
