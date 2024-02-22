using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf.Viewmodels
{
    public class MedewerkerViewmodel : BaseViewmodel
    {
        public MedewerkerViewmodel()
        {
            CurrentViewmodel = new MedewerkerAanmakenViewmodel();
        }

        private BaseViewmodel _currentViewmodel;

        public BaseViewmodel CurrentViewmodel
        {
            get { return _currentViewmodel; }
            set { _currentViewmodel = value; }
        }
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Aanmaken": return true;
                case "Overzicht": return true;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Aanmaken": LoadMedewerkerAanmakenView(); break;
                case "Overzicht": LoadMedewerkerOverzichtView(); break;
            }
        }

        private void LoadMedewerkerOverzichtView()
        {
            CurrentViewmodel = new MedewerkerOverzichtViewmodel();
        }

        private void LoadMedewerkerAanmakenView()
        {
            CurrentViewmodel = new MedewerkerAanmakenViewmodel();
        }
    }
}
