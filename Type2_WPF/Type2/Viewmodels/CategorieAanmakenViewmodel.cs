using dal.Data.UnitOfWork;
using models;
using dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf.Viewmodels
{
    public class CategorieAanmakenViewmodel : BaseViewmodel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new Type2Context());
        private string _foutmelding;
        public Categorie CategorieRecord { get; set; }

        public CategorieAanmakenViewmodel()
        {
            CategorieRecordInstellen();
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
               
                if (CategorieRecord.IsGeldig())
                {
                    _unitOfWork.CategorieRepo.ToevoegenOfAanpassen(CategorieRecord);
                        int ok = _unitOfWork.Save();
                        if (ok < 0)
                        {
                            Foutmelding = CategorieRecord.Error;
                            

                        }
                    Wissen();
                }
                else
                {
                    Foutmelding = "Categorie is niet toegevoegd" + Environment.NewLine;
                    Foutmelding += CategorieRecord.Error;
                    
                } 
            }         
            
        }

        private void Annuleren()
        {
            Wissen();
        }

        private void Wissen()
        {
            CategorieRecord.Naam = "";
            CategorieRecord.Beschrijving = "";
            Foutmelding = "";
        }

        private void CategorieRecordInstellen()
        {
            CategorieRecord = new Categorie();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
