using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models;
using wpf.Views;

namespace wpf.Viewmodels
{
    public class KortingkaartAanmakenViewmodel: BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Kortingskaart KortingkaartRecord { get; set; }

        public KortingkaartAanmakenViewmodel()
        {
            KortingkaartRecordInstellen();
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

        public void Opslaan()
        {
            if (this.IsGeldig())
            {
                if(KortingkaartRecord.Professioneel == true)
                {
                    if (KortingkaartRecord.Teller % 10 == 0)
                    {
                        KortingkaartRecord.Percentage = 30;
                        KortingkaartRecord.Teller++;
                    }
                    else
                    {
                       KortingkaartRecord.Percentage = 15;
                       KortingkaartRecord.Teller++;
                    }
                }
                else
                {
                    if (KortingkaartRecord.Teller % 10 == 0)
                    {
                        KortingkaartRecord.Percentage = 15;
                        KortingkaartRecord.Teller++;
                    }
                    else
                    {
                        KortingkaartRecord.Percentage = 5;
                        KortingkaartRecord.Teller++;
                    }
                }

                if (KortingkaartRecord.IsGeldig())
                {
                    _unitOfWork.KortingskaartRepo.ToevoegenOfAanpassen(KortingkaartRecord);
                    int ok = _unitOfWork.Save();
                    if (ok < 0)
                    {
                        Foutmelding = KortingkaartRecord.Error;
                        Wissen();
                    }
                }
                else
                {
                    Foutmelding = "Kortingkaart is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += KortingkaartRecord.Error;

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
            KortingkaartRecord.Professioneel = false;
            KortingkaartRecord.Code = "";
            KortingkaartRecord.Teller = 0;
        }

        private void KortingkaartRecordInstellen()
        {
            KortingkaartRecord = new Kortingskaart();
        }
        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }

}
