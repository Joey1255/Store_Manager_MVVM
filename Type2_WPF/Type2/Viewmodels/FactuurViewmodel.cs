using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf.Viewmodels
{
    public class FactuurViewmodel : BaseViewmodel
    {
        public FactuurViewmodel()
        {
            CurrentViewmodel = new FactuurAanmakenViewmodel();
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
                case "Aanmaken": LoadFactuurAanmakenView(); break;
                case "Overzicht": LoadFactuurOverzichtView(); break;
            }
        }

        private void LoadFactuurOverzichtView()
        {
            CurrentViewmodel = new FactuurOverzichtViewmodel();
        }

        private void LoadFactuurAanmakenView()
        {
            CurrentViewmodel = new FactuurAanmakenViewmodel();
        }
    }
}
