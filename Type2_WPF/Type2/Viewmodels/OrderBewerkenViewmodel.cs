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
using System.Collections.ObjectModel;

namespace wpf.Viewmodels
{
    public class OrderBewerkenViewmodel: BaseViewmodel, IDisposable, Iclosable
    {
        public OrderBewerkenViewmodel() { }
        public OrderBewerkenViewmodel(Order geselecteerdOrder)
        {
            GeselecteerdOrder = geselecteerdOrder;
            Klanten = new ObservableCollection<Klant>(_unitOfWork.KlantRepo.Ophalen()); 
        }

        private DelegateCommand _closeCommand;
        
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private ObservableCollection<Klant> _klanten;
        private string _foutmelding;
        private Order _geselecteerdOrder;
        private Klant _geselecteerdeKlant;
        
        public Action Close { get; set; }

        public ObservableCollection<Klant> Klanten
        {
            get { return _klanten; }
            set { _klanten = value; }
        }
        public Order GeselecteerdOrder
        {
            get { return _geselecteerdOrder; }
            set { _geselecteerdOrder = value; }
        }

        public Klant GeselecteerdeKlant
        {
            get { return _geselecteerdeKlant; }
            set { _geselecteerdeKlant = value; }
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
            if (GeselecteerdOrder != null)
            {
                

                if (GeselecteerdOrder.IsGeldig())
                {
                    _unitOfWork.OrderRepo.Aanpassen(GeselecteerdOrder);
                    int ok = _unitOfWork.Save();
                    FoutmeldingInstellenNaSave(ok, "order is niet aangepast");
                }
            }
            else
            {
                Foutmelding = "Eerst een order selecteren";
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
