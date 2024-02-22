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
    public class DienstenBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public DienstenBewerkenViewmodel() { }
        public DienstenBewerkenViewmodel(Dienst selectedDienst)
        {
            SelectedDienst = selectedDienst;
        }
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Action Close { get; set; }

        private Dienst dienst;
        public Dienst SelectedDienst
        {
            get { return dienst; }
            set { dienst = value; }
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

        private void Opslaan()
        {
            if (SelectedDienst != null)
            {
                if (SelectedDienst.IsGeldig())
                {
                    _unitOfWork.DienstRepo.Aanpassen(SelectedDienst);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Dienst is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een dienst selecteren";
            }
        }

        private void Annuleren()
        {
            Close?.Invoke();
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
