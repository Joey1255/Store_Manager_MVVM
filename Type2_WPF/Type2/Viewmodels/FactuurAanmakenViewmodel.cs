using dal.Data.UnitOfWork;
using dal;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace wpf.Viewmodels
{
    public class FactuurAanmakenViewmodel : BaseViewmodel
    {

        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Factuur FactuurRecord { get; set; }
        private ObservableCollection<Order> _orders;
        private Order _geselecteerdOrder;
        

        public FactuurAanmakenViewmodel()
        {
            Orders = new ObservableCollection<Order>(_unitOfWork.OrderRepo.Ophalen());
            FactuurRecordInstellen();
        }

        public Order GeselecteerdOrder
        {
            get { return _geselecteerdOrder; }
            set { _geselecteerdOrder = value; }
        }

       
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
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
                FactuurRecord.OrderId = GeselecteerdOrder.OrderId;
                if(FactuurRecord.BtwNummer == true)
                {
                    FactuurRecord.BtwPercentage = 6;
                }
                else
                {
                    FactuurRecord.BtwPercentage = 21;
                }


                if (FactuurRecord.IsGeldig())
                {
                    _unitOfWork.FactuurRepo.ToevoegenOfAanpassen(FactuurRecord);
                    int ok = _unitOfWork.Save();
                    if (ok < 0)
                    {
                        Foutmelding = FactuurRecord.Error;
                        Wissen();
                    }
                }
                else
                {
                    Foutmelding = "Factuur is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += FactuurRecord.Error;

                }
            }
            Wissen();
        }

        private void Annuleren()
        {
            Wissen();
        }

        private void Wissen()
        {
            GeselecteerdOrder = null;
            FactuurRecord.BtwNummer = false;
            
        }

        private void FactuurRecordInstellen()
        {
            FactuurRecord = new Factuur();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
