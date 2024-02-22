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
    public class KlantBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public KlantBewerkenViewmodel() { }
        public KlantBewerkenViewmodel(Klant selectedKlant)
        {
            SelectedKlant = selectedKlant;
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public Klant KlantRecord { get; set; }
        private Klant _selectedKlant { get; set; }
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Action Close { get; set; }

        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set
            {
                _selectedKlant = value;
                KlantRecordInstellen();
            }
        }
        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        private void KlantRecordInstellen()
        {
            if (SelectedKlant != null)
            {
                KlantRecord = SelectedKlant;
            }
            else
            {
                KlantRecord = new Klant();
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
            if (SelectedKlant != null)
            {
                if (SelectedKlant.IsGeldig())
                {
                    _unitOfWork.KlantRepo.Aanpassen(SelectedKlant);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Klant is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een klant selecteren!";
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
