using dal.Data.UnitOfWork;
using dal;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace wpf.Viewmodels
{
    public class OrderAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _ordernummer;
        private string _klantId;
        private Klant _geselecteerdeKlant;
        private ObservableCollection<Klant> _klanten;
        private string _foutmelding;
        public Order OrderRecord { get; set; }

        public OrderAanmakenViewmodel()
        {
            Klanten = new ObservableCollection<Klant>(_unitOfWork.KlantRepo.Ophalen());
            OrderRecordInstellen();
        }

        public string Ordernummer
        {
            get { return _ordernummer; }
            set { _ordernummer = value; }
        }

        public string KlantId
        {
            get { return _klantId; }
            set { _klantId = value; }
        }

        public Klant GeselecteerdeKlant
        {
            get { return _geselecteerdeKlant; }
            set { _geselecteerdeKlant = value; }
        }

        public ObservableCollection<Klant> Klanten
        {
            get { return _klanten; }
            set { _klanten = value; }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public override string this[string columnName]
        {
            get { return ""; }
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
            if (this.IsGeldig())
            {
                OrderRecord.KlantId = GeselecteerdeKlant.Klantid;
                if (OrderRecord.IsGeldig())
                {
                    _unitOfWork.OrderRepo.ToevoegenOfAanpassen(OrderRecord);
                    int ok = _unitOfWork.Save();
                    if (ok < 0)
                    {
                        Foutmelding = OrderRecord.Error;
                        MessageBox.Show(Foutmelding);

                    }
                    Annuleren();
                }
                else
                {
                    Foutmelding = "Order is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += OrderRecord.Error;
                    MessageBox.Show(Foutmelding);
                }
            }
        }

        private void Annuleren()
        {
            OrderRecord.Ordernummer = "";
            GeselecteerdeKlant = null;
        }

        private void OrderRecordInstellen()
        {
            OrderRecord = new Order();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
