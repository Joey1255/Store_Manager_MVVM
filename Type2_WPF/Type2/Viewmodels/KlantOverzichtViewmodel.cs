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
    public class KlantOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Klant> Klanten { get; set; }
        public Klant KlantRecord { get; set; }
        private Klant _selectedKlant;
        private string _foutmelding;
        private string _zoekterm = "";
        private bool _partKlanten;
        private bool _profKlanten;

        public KlantOverzichtViewmodel()
        {
            Klanten = new ObservableCollection<Klant>(_unitOfWork.KlantRepo.Ophalen());
            KlantRecordInstellen();
        }
        public bool PartKlanten
        {
            get { return _partKlanten; }
            set
            {
                _partKlanten = value;
                Refresh();
            }
        }
        public bool ProfKlanten
        {
            get { return _profKlanten; }
            set 
            { 
                _profKlanten = value;
                Refresh();
            }
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

        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set { _selectedKlant = value; }
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
                case "Verwijderen": return SelectedKlant != null ? true : false;
                case "Bewerken": return SelectedKlant != null ? true : false;
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
            var vm = new KlantBewerkenViewmodel(SelectedKlant);
            var view = new KlantBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedKlant != null)
            {
                _unitOfWork.KlantRepo.Verwijderen(SelectedKlant.Klantid);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Klant is niet verwijderd");
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

        private void Resetten()
        {
            if (this.IsGeldig())
            {
                SelectedKlant = null;
                KlantRecordInstellen();
                Foutmelding = "";
            }
            else
            {
                Foutmelding = this.Error;
            }
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

        private void Refresh()
        {
            List<Klant> lijstKlanten = null;
            if (ProfKlanten && !PartKlanten)
            {
               lijstKlanten = _unitOfWork.KlantRepo.Ophalen(x => x.Professioneel && (x.Bedrijfsnaam.Contains(Zoekterm) || x.Btwnummer.Contains(Zoekterm) || x.Straat.Contains(Zoekterm)
               || x.Gemeente.Contains(Zoekterm) || x.Email.Contains(Zoekterm))).ToList();
            }
            else if (!ProfKlanten && PartKlanten)
            {
                lijstKlanten = _unitOfWork.KlantRepo.Ophalen(x => !x.Professioneel && (x.Voornaam.Contains(Zoekterm) || x.Achternaam.Contains(Zoekterm) || x.Straat.Contains(Zoekterm)
               || x.Gemeente.Contains(Zoekterm) || x.Email.Contains(Zoekterm))).ToList();
            }
            else
            {
                lijstKlanten = _unitOfWork.KlantRepo.Ophalen(x => x.Voornaam.Contains(Zoekterm) || x.Achternaam.Contains(Zoekterm) || x.Klantid.ToString().Contains(Zoekterm)
                || x.Btwnummer.Contains(Zoekterm) || x.Straat.Contains(Zoekterm) || x.Gemeente.Contains(Zoekterm) || x.Email.Contains(Zoekterm)).ToList();
            }

            Klanten = new ObservableCollection<Klant>(lijstKlanten);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
