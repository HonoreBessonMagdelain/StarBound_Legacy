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

namespace StarBound_Legacy
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Garage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            MainWindow.OuvertureGarage();
            
        }

        private void Quitter(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
