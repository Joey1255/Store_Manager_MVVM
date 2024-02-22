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

namespace wpf.Viewmodels
{
    public class LocatieOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Locatie> Locaties { get; set; }
        public Locatie LocatieRecord { get; set; }
        private Locatie _selectedLocatie;
        private string _foutmelding;
        private string _zoekterm;

        public LocatieOverzichtViewmodel()
        {
            Locaties = new ObservableCollection<Locatie>(_unitOfWork.LocatieRepo.Ophalen());
            LocatieRecordInstellen();
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


        public Locatie SelectedLocatie
        {
            get { return _selectedLocatie; }
            set
            {
                _selectedLocatie = value;
                LocatieRecordInstellen();
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
                case "Verwijderen": return SelectedLocatie != null ? true : false;
                case "Bewerken": return SelectedLocatie != null ? true : false;
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
            var vm = new LocatieBewerkenViewmodel(SelectedLocatie);
            var view = new LocatieBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedLocatie != null)
            {
                _unitOfWork.LocatieRepo.Verwijderen(SelectedLocatie.LocatieId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Locatie is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst een locatie selecteren";
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
            List<Locatie> lijstLocaties = _unitOfWork.LocatieRepo.Ophalen(x => x.Naam.Contains(Zoekterm)).ToList();
            Locaties = new ObservableCollection<Locatie>(lijstLocaties);
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

        public void Resetten()
        {
            if (this.IsGeldig())
            {
                SelectedLocatie = null;
                LocatieRecordInstellen();
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
