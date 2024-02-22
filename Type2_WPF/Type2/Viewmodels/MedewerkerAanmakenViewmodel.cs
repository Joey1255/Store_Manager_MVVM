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
    public class MedewerkerAanmakenViewmodel : BaseViewmodel
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _paswoord;
        private string _foutmelding;
        public Medewerker MedewerkerRecord { get; set; }
        public MedewerkerAanmakenViewmodel()
        {
            MedewerkerRecordInstellen();
        }
        public string Paswoord
        {
            get { return _paswoord; }
            set { _paswoord = value; }
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
            MedewerkerRecord.Paswoord = "paswoord";

            if (MedewerkerRecord.IsGeldig())
            {
                _unitOfWork.MedewerkerRepo.ToevoegenOfAanpassen(MedewerkerRecord);
                int ok = _unitOfWork.Save();
                if (ok < 0)
                {
                    Foutmelding = MedewerkerRecord.Error;
                }
                else
                {
                    MessageBox.Show("Er is een email verstuurd naar de medewerker om het paswoord in te stellen");
                }
            }
            else
            {
                Foutmelding = "Medewerker is niet toegevoegd" + Environment.NewLine;
                Foutmelding += MedewerkerRecord.Error;
                MessageBox.Show(Foutmelding);
            }
            Annuleren();
        }
        private void Annuleren()
        {
            MedewerkerRecord.Voornaam = "";
            MedewerkerRecord.Achternaam = "";
            MedewerkerRecord.Email = "";
            MedewerkerRecord.Paswoord = "";
        }
        private void MedewerkerRecordInstellen()
        {
            MedewerkerRecord = new Medewerker();
        }
        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
