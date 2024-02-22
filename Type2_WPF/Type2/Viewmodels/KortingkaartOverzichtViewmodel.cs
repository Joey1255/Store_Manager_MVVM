using dal.Data.UnitOfWork;
using dal;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using wpf.Views;

namespace wpf.Viewmodels
{
    public class KortingkaartOverzichtViewmodel: BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());

        private string _foutmelding;
        private string _zoekterm;
        private Kortingskaart _selectedKortingkaart;
        public Kortingskaart KortingkaartRecord { get; set; }
        public ObservableCollection<Kortingskaart> Kortingkaarten { get; set; }

        public Kortingskaart SelectedKortingkaart
        {
            get { return _selectedKortingkaart; }
            set
            {
                _selectedKortingkaart = value;
                KortingkaartRecordInstellen();
            }
        }

        public KortingkaartOverzichtViewmodel()
        {
            Kortingkaarten = new ObservableCollection<Kortingskaart>(_unitOfWork.KortingskaartRepo.Ophalen());
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
                case "Verwijderen": return SelectedKortingkaart != null ? true : false;
                case "Bewerken": return SelectedKortingkaart != null ? true : false;
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
            var vm = new KortingkaartBewerkenViewmodel(SelectedKortingkaart);
            var view = new KortingkaartBewerken();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedKortingkaart != null)
            {
                _unitOfWork.FactuurRepo.Verwijderen(SelectedKortingkaart.KortingskaartId);
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
                SelectedKortingkaart = null;
                KortingkaartRecordInstellen();
                Foutmelding = "";

            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        private void Refresh()
        {
            List<Kortingskaart> lijstKortingkaarten = _unitOfWork.KortingskaartRepo.Ophalen(x => x.Code.Contains(Zoekterm)).ToList();
            Kortingkaarten = new ObservableCollection<Kortingskaart>(lijstKortingkaarten);

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
        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
