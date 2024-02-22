using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using wpf.Views;

namespace wpf.Viewmodels
{
    public class MainViewmodel : BaseViewmodel
    {
        public MainViewmodel()
        {
            CurrentViewmodel = new KlantViewmodel();
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
                case "AdminAanmelden": return true;
                case "MedewerkerAanmelden": return true;
                case "Klant": return true;
                case "Product": return true;
                case "Locatie": return true;
                case "Stock": return true;
                case "Categorie": return true;
                case "Order": return true;
                case "Factuur": return true;
                case "Medewerker": return Admin;
                case "Diensten": return Admin;
                case "Kortingkaart": return Admin;
                case "PowerOff": return true;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "AdminAanmelden": AdminAanmelden(); break;
                case "MedewerkerAanmelden": MedewerkerAanmelden(); break;
                case "Klant": LoadKlantView(); break;
                case "Product": LoadProductView(); break;
                case "Locatie": LoadLocatieView(); break;
                case "Stock": LoadStockView(); break;
                case "Categorie": LoadCategorieView(); break;
                case "Order": LoadOrderView(); break;
                case "Factuur": LoadFactuurView(); break;
                case "Medewerker": LoadMedewerkerView(); break;
                case "Diensten": LoadDienstenView(); break;
                case "Kortingkaart": LoadKortingkaartView(); break;
                case "PowerOff": PowerOff(); break;
            }
        }

        private void MedewerkerAanmelden()
        {
            Admin = false;
            MessageBox.Show("U bent ingelogd als Medewerker");
        }

        private bool _admin;

        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }



        private void AdminAanmelden()
        {
            Admin = true;
            MessageBox.Show("U bent ingelogd als Admin");

        }

        private void LoadKortingkaartView()
        {
            CurrentViewmodel = new KortingkaartViewmodel();
        }

        private void LoadDienstenView()
        {
            CurrentViewmodel = new DienstenViewmodel();
        }

        public void LoadKlantView()
        {
            CurrentViewmodel = new KlantViewmodel();
        }
        public void LoadProductView()
        {
            CurrentViewmodel = new ProductViewmodel();
        }
        public void LoadLocatieView()
        {
            CurrentViewmodel = new LocatieViewmodel();
        }
        public void LoadStockView()
        {
            CurrentViewmodel = new StockViewmodel();
        }
        public void LoadCategorieView()
        {
            CurrentViewmodel = new CategorieViewmodel();
        }
        public void LoadOrderView()
        {
            CurrentViewmodel = new OrderViewmodel();
        }
        public void LoadFactuurView()
        {
            CurrentViewmodel = new FactuurViewmodel();
        }
        public void LoadMedewerkerView()
        {
            CurrentViewmodel = new MedewerkerViewmodel();
        }
        public void PowerOff()
        {
            Application.Current.Shutdown();
        }
    }
}
