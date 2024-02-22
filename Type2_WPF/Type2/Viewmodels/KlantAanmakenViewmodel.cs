using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models;
using System.Windows;

namespace wpf.Viewmodels
{
    public class KlantAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());

        private string _foutmelding;

        public Klant KlantRecord { get; set; }


        public KlantAanmakenViewmodel()
        {
            KlantRecordInstellen();
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        private void KlantRecordInstellen()
        {
            KlantRecord = new Klant();
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
                if (KlantRecord.IsGeldig())
                {
                    _unitOfWork.KlantRepo.ToevoegenOfAanpassen(KlantRecord);
                    int ok = _unitOfWork.Save();
                    if (ok < 0)
                    {
                        Foutmelding = KlantRecord.Error;
                    }
                    Wissen();
                }
                else
                {
                    Foutmelding = "Klant is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += KlantRecord.Error;
                    MessageBox.Show(Foutmelding);
                }
            }
        }

        private void Annuleren()
        {
            Wissen();
        }

        private void Wissen()
        {
            KlantRecord.Telefoonnummer = "";
            KlantRecord.Straat = "";
            KlantRecord.Huisnummer = "";
            KlantRecord.Gemeente = "";
            KlantRecord.Email = "";
            KlantRecord.Voornaam = "";
            KlantRecord.Achternaam = "";
            KlantRecord.Bedrijfsnaam = "";
            KlantRecord.Btwnummer = "";
            Foutmelding = "";

        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
