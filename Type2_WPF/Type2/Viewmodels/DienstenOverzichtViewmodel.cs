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
    public class DienstenOverzichtViewmodel: BaseViewmodel
    {

        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Dienst> Diensten { get; set; }

        private string _foutmelding;
        private string _zoekterm;
        private Dienst _selectedDienst;
        public Dienst DienstRecord { get; set; }

        public Dienst SelectedDienst
        {
            get { return _selectedDienst; }
            set { 
                _selectedDienst = value;
                DienstRecordInstellen();
            }
        }

        public DienstenOverzichtViewmodel()
        {
            Diensten = new ObservableCollection<Dienst>(_unitOfWork.DienstRepo.Ophalen());
            DienstRecordInstellen();
        }
        private void DienstRecordInstellen()
        {
            if (SelectedDienst != null)
            {
                DienstRecord = SelectedDienst;
            }
            else
            {
                DienstRecord = new Dienst();
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
                case "Verwijderen": return SelectedDienst != null ? true : false;
                case "Bewerken": return SelectedDienst != null ? true : false;
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
            var vm = new DienstenBewerkenViewmodel(SelectedDienst);
            var view = new DienstenBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Verwijderen()
        {
            if (SelectedDienst != null)
            {
                _unitOfWork.DienstRepo.Verwijderen(SelectedDienst.DienstId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Dienst is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Dienst selecteren";
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
            List<Dienst> lijstDiensten = _unitOfWork.DienstRepo.Ophalen(x => x.Naam.Contains(Zoekterm) || x.Beschrijving.Contains(Zoekterm)).ToList();
            Diensten = new ObservableCollection<Dienst>(lijstDiensten);
        }
        private void Resetten()
        {
            if (this.IsGeldig())
            {
                SelectedDienst = null;
                DienstRecordInstellen();
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
