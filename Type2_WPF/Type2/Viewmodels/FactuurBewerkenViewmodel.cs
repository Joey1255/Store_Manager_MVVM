using dal.Data.UnitOfWork;
using dal;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Interfaces;
using models;
using System.Collections.ObjectModel;

namespace wpf.Viewmodels
{
    public class FactuurBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private DelegateCommand _closeCommand;
        public Action Close { get; set; }
       
        public FactuurBewerkenViewmodel() { }
        public FactuurBewerkenViewmodel(Factuur selectedFactuur) 
        {
            SelectedFactuur = selectedFactuur;
           
        }

        private Factuur _selectedFactuur;
        private string _foutmelding;
        

        public Factuur SelectedFactuur
        {
            get { return _selectedFactuur; }
            set
            {
                _selectedFactuur = value;
                
            }
        }

        

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }



        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
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
            if(SelectedFactuur != null)
            {
                
                if (SelectedFactuur.BtwNummer == true)
                {
                    SelectedFactuur.BtwPercentage = 6;
                }
                else
                {
                    SelectedFactuur.BtwPercentage = 21;
                }

                if (SelectedFactuur.IsGeldig())
                {
                    _unitOfWork.FactuurRepo.Aanpassen(SelectedFactuur);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Factuur is niet verwijderd");
                }
                else
                {
                    Foutmelding = "Eerst een Factuur Selecteren";
                }
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

        public override string this[string columnName]
        {
            get
            {

                return "";
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        
    }
}
