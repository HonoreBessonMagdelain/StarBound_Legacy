using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }


        private void Garage(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "garage";
            this.DialogResult = true;
        }
        


        private void Quitter(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }

        private void Credits(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).FenetreAOuvrir = "";
            this.DialogResult = true;
            
            
        }
        private void Reglages(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).FenetreAOuvrir = "reglages";
            this.DialogResult = true;
        }
        private void Jouer(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }
    }
}
