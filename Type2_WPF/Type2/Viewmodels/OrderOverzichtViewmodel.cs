using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using models;
using wpf.Views;

namespace wpf.Viewmodels
{
    public class OrderOverzichtViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        public ObservableCollection<Order> Orders { get; set; }
        public Order OrderRecord { get; set; }
        private Order _geselecteerdOrder;
        private string _foutmelding;
        private string _zoekterm;


        public OrderOverzichtViewmodel() 
        {
            Orders = new ObservableCollection<Order>(_unitOfWork.OrderRepo.Ophalen(x => x.Klant));
            OrderRecordInstellen();
        }
        
        public Order GeselecteerdOrder
        {
            get { return _geselecteerdOrder; }
            set 
            { 
                _geselecteerdOrder = value;
                OrderRecordInstellen();   
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
                case "Verwijderen": return GeselecteerdOrder != null ? true : false;
                case "Bewerken": return GeselecteerdOrder != null ? true : false;
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

        private void OrderRecordInstellen()
        {
            if (GeselecteerdOrder != null)
            {
                OrderRecord = GeselecteerdOrder;
            }
            else
            {
                OrderRecord = new Order();
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
        public void Verwijderen()
        {
            if (GeselecteerdOrder != null)
            {
                _unitOfWork.OrderRepo.Verwijderen(GeselecteerdOrder.OrderId);
                int ok = _unitOfWork.Save();
                FoutmeldingInstellenNaSave(ok, "Order is niet verwijderd");
            }
            else
            {
                Foutmelding = "Eerst Order selecteren";
            }
        }
        public void Bewerken()
        {
            var vm = new OrderBewerkenViewmodel(GeselecteerdOrder);
            var view = new OrderBewerkenWindow();
            view.DataContext = vm;
            view.Show();
        }

        private void Resetten()
        {
            if (this.IsGeldig())
            {
                GeselecteerdOrder = null;
                OrderRecordInstellen();
                Foutmelding = "";

            }
            else
            {
                Foutmelding = this.Error;
            }
        }

        private void Refresh()
        {
            List<Order> lijstOrders = _unitOfWork.OrderRepo.Ophalen(x => x.Klant.Voornaam.Contains(Zoekterm) || x.Ordernummer.Contains(Zoekterm) 
            || x.Klant.Achternaam.Contains(Zoekterm) || x.Klant.Bedrijfsnaam.Contains(Zoekterm) || x.Klant.Klantid.ToString().Contains(Zoekterm)).ToList();
            Orders = new ObservableCollection<Order>(lijstOrders);

        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
