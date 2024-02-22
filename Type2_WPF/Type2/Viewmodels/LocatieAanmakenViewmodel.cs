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
    public class LocatieAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        // heb beschrijving eruit gehaald wegens onnodig en ook niet aanwezig in model.

        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        
        private string _foutmelding;
        public Locatie LocatieRecord { get; set; }
        public LocatieAanmakenViewmodel()
        {
            LocatieRecordInstellen();
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
            
            if (LocatieRecord.IsGeldig())
            {
                _unitOfWork.LocatieRepo.ToevoegenOfAanpassen(LocatieRecord);
                int ok = _unitOfWork.Save();
                if (ok < 0)
                {
                    Foutmelding = LocatieRecord.Error;

                }
            }
            else
            {
                Foutmelding = "Locatie is niet toegevoegd";
                Foutmelding += LocatieRecord.Error;
            }
            Annuleren();
        }
        private void Annuleren()
        {
            LocatieRecord.Naam = "";
        }
        private void LocatieRecordInstellen()
        {
            LocatieRecord = new Locatie();
        }
        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
