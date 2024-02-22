using dal.Data.UnitOfWork;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models;
using System.Collections.ObjectModel;
using System.Windows;

namespace wpf.Viewmodels
{
    public class DienstenAanmakenViewmodel: BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Dienst DienstRecord { get; set; }

        public DienstenAanmakenViewmodel()
        {
            DienstRecordInstellen();
        }

        private void DienstRecordInstellen()
        {
            DienstRecord = new Dienst();
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
            if (DienstRecord.IsGeldig())
            {
                _unitOfWork.DienstRepo.ToevoegenOfAanpassen(DienstRecord);
                int ok = _unitOfWork.Save();
                if (ok < 0)
                {
                    Foutmelding = DienstRecord.Error;
                    MessageBox.Show(Foutmelding);

                }
            }
            else
            {
                Foutmelding = "Dienst is niet toegevoegd";
                Foutmelding += DienstRecord.Error;
                MessageBox.Show(Foutmelding);
            }
            Annuleren();
        }

        private void Annuleren()
        {
            DienstRecord.Naam = "";
            DienstRecord.Beschrijving = "";
            DienstRecord.Prijs = 0;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
