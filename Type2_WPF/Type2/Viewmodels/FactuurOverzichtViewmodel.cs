using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models;
using wpf.Views;
using System.Collections.ObjectModel;

namespace wpf.Viewmodels
{
    public class FactuurOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public Factuur FactuurRecord { get; set; }
        private string _foutmelding;
        private string _zoekterm;
        private Factuur _selectedFactuur;
        public ObservableCollection<Factuur> Facturen { get; set; }

        public FactuurOverzichtViewmodel()
        {
            Facturen = new ObservableCollection<Factuur>(_unitOfWork.FactuurRepo.Ophalen(x => x.Order));
            FactuurRecordInstellen();
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

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public Factuur SelectedFactuur
        {
            get { return _selectedFactuur; }
            set
            {
                _selectedFactuur = value;
                FactuurRecordInstellen();
            }
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
                case "Verwijderen": return SelectedFactuur != null ? true : false;
                case "Bewerken": return SelectedFactuur != null ? true : false;
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
            var vm = new FactuurBewerkenViewmodel(SelectedFactuur);
            var view = new FactuurBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if(SelectedFactuur != null)
            {
                _unitOfWork.FactuurRepo.Verwijderen(SelectedFactuur.FactuurId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Factuur is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Factuur selecteren";
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
                SelectedFactuur = null;
                FactuurRecordInstellen();
                Foutmelding = "";

            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        private void Refresh()
        {
            List<Factuur> lijstFacturen = _unitOfWork.FactuurRepo.Ophalen(x => x.Order.Ordernummer.Contains(Zoekterm)).ToList();
            Facturen = new ObservableCollection<Factuur>(lijstFacturen);
            
        }

        private void FactuurRecordInstellen()
        {
            if(SelectedFactuur != null)
            {
                FactuurRecord = SelectedFactuur;
            }
            else
            {
                FactuurRecord = new Factuur();
            }
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
