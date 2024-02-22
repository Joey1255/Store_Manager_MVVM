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
    public class LocatieBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public LocatieBewerkenViewmodel() { }
        public LocatieBewerkenViewmodel(Locatie selectedLocatie)
        {
            SelectedLocatie = selectedLocatie;
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public Locatie LocatieRecord { get; set; }
        private Locatie _selectedLocatie;
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Action Close { get; set; }
      
        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public Locatie SelectedLocatie
        {
            get { return _selectedLocatie; }
            set
            {
                _selectedLocatie = value;
                LocatieRecordInstellen();
            }
        }

        private void LocatieRecordInstellen()
        {
            if (SelectedLocatie != null)
            {
                LocatieRecord = SelectedLocatie;
            }
            else
            {
                LocatieRecord = new Locatie();
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
            if (SelectedLocatie != null)
            {
                if (SelectedLocatie.IsGeldig())
                {
                    _unitOfWork.LocatieRepo.Aanpassen(SelectedLocatie);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Locatie is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een locatie selecteren!";
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
