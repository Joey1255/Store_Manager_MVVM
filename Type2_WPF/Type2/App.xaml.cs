using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpf.Viewmodels;
using wpf.Views;

namespace Type2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var vm = new MainViewmodel();
            var view = new MainView();
            view.DataContext = vm;
            view.Show();

        }
    }
}
