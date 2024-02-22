using dal.Data.UnitOfWork;
using dal;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.Views;
using System.Windows;

namespace wpf.Viewmodels
{
    public class MedewerkerOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Medewerker> Medewerkers { get; set; }
        public Medewerker MedewerkerRecord { get; set; }
        private Medewerker _selectedMedewerker;
        private string _foutmelding;
        private string _zoekterm;

        public MedewerkerOverzichtViewmodel()
        {
            Medewerkers = new ObservableCollection<Medewerker>(_unitOfWork.MedewerkerRepo.Ophalen());
            MedewerkerRecordInstellen();
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


        public Medewerker SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set
            {
                _selectedMedewerker = value;
                MedewerkerRecordInstellen();
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
                case "Verwijderen": return SelectedMedewerker != null ? true : false;
                case "Bewerken": return SelectedMedewerker != null ? true : false;
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
            var vm = new MedewerkerBewerkenViewmodel(SelectedMedewerker);
            var view = new MedewerkerBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedMedewerker != null)
            {
                _unitOfWork.MedewerkerRepo.Verwijderen(SelectedMedewerker.MedewerkerId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Medewerker is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst een medewerker selecteren";
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
            List<Medewerker> lijsMedewerkers = _unitOfWork.MedewerkerRepo.Ophalen(x => x.Achternaam.Contains(Zoekterm) || x.Voornaam.Contains(Zoekterm) || x.Email.Contains(Zoekterm) 
            || x.MedewerkerId.ToString().Contains(Zoekterm)).ToList();
            Medewerkers = new ObservableCollection<Medewerker>(lijsMedewerkers);
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

        public void Resetten()
        {
            if (this.IsGeldig())
            {
                SelectedMedewerker = null;
                MedewerkerRecordInstellen();
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
