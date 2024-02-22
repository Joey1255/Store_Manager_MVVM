using dal;
using dal.Data.UnitOfWork;
using models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf.Interfaces;

namespace wpf.Viewmodels
{
    public class CategorieBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        //Er moet een constructor zijn zonder parameters om het window te laten sluiten.
        public CategorieBewerkenViewmodel() { }
        public CategorieBewerkenViewmodel(Categorie selectedCategorie)
        {
            SelectedCategorie = selectedCategorie;
            
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public Categorie CategorieRecord { get; set; }
        private Categorie _selectedCategorie;
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Action Close { get; set; } 

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

       

        private void CategorieRecordInstellen()
        {
            if (SelectedCategorie != null)
            {
                CategorieRecord = SelectedCategorie;
            }
            else
            {
                CategorieRecord = new Categorie();
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
            if (SelectedCategorie != null)
            {
                if (SelectedCategorie.IsGeldig())
                {
                    _unitOfWork.CategorieRepo.Aanpassen(SelectedCategorie);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Categorie is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een categorie selecteren!";
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
