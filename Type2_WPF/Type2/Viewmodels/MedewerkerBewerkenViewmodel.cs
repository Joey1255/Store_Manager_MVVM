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
using System.Windows;

namespace wpf.Viewmodels
{
    public class MedewerkerBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public MedewerkerBewerkenViewmodel() { }

        public MedewerkerBewerkenViewmodel(Medewerker selectedMedewerker)
        {
            SelectedMedewerker = selectedMedewerker;
        }
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }
        public Medewerker MedewerkerRecord { get; set; }
        private Medewerker _selectedMedewerker;
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Action Close { get; set; }
            

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public Medewerker SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set
            {
                _selectedMedewerker = value;
                MedewerkerRecordInstellen();
            }
        }

        private void MedewerkerRecordInstellen()
        {
            if (SelectedMedewerker != null)
            {
                MedewerkerRecord = SelectedMedewerker;
            }
            else
            {
                MedewerkerRecord = new Medewerker();
            }
        }


        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Annuleren": return true;
                case "Opslaan": return true;
                case "ResetPaswoord": return true;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Annuleren": Annuleren(); break;
                case "Opslaan": Opslaan(); break;
                case "ResetPaswoord": ResetPaswoord(); break;
            };
        }

        private void Opslaan()
        {
            if (SelectedMedewerker != null)
            {
                if (SelectedMedewerker.IsGeldig())
                {
                    _unitOfWork.MedewerkerRepo.Aanpassen(SelectedMedewerker);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "Medewerker is niet verwijderd");
                }
            }
            else
            {
                Foutmelding = "Eerst een medewerker selecteren!";
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


        public void ResetPaswoord()
        {
            MessageBox.Show("Email voor het opnieuw instellen van het paswoord is verstuurd naar de geselecteerde medewerker");
        }
    }
}
