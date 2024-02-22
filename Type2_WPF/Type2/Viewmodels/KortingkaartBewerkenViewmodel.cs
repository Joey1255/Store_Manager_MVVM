using dal.Data.UnitOfWork;
using dal;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Views;
using wpf.Interfaces;
using models;

namespace wpf.Viewmodels
{
    public class KortingkaartBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public KortingkaartBewerkenViewmodel() { }
        public KortingkaartBewerkenViewmodel(Kortingskaart selectedKortingkaart) {
            SelectedKortingkaart = selectedKortingkaart;
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Kortingskaart KortingkaartRecord { get; set; }
        public Action Close { get; set; }

        private Kortingskaart _selectedKortingkaart;

        public Kortingskaart SelectedKortingkaart
        {
            get { return _selectedKortingkaart; }
            set
            {
                _selectedKortingkaart = value;
                KortingkaartRecordInstellen();
            }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
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

        private void KortingkaartRecordInstellen()
        {
            if (SelectedKortingkaart != null)
            {
                KortingkaartRecord = SelectedKortingkaart;
            }
            else
            {
                KortingkaartRecord = new Kortingskaart();
            }
        }

        public void Opslaan()
        {
            if (SelectedKortingkaart != null)
            {
                if (SelectedKortingkaart.IsGeldig())
                {
                    _unitOfWork.KortingskaartRepo.Aanpassen(SelectedKortingkaart);
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
