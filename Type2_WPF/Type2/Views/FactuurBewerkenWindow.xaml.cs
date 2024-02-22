using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpf.Interfaces;

namespace wpf.Views
{
    /// <summary>
    /// Interaction logic for FactuurBewerkenWindow.xaml
    /// </summary>
    public partial class FactuurBewerkenWindow : Window
    {
        public FactuurBewerkenWindow()
        {
            InitializeComponent();
            Loaded += FactuurBewerkenWindow_Loaded;
        }

        private void FactuurBewerkenWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is Iclosable vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
