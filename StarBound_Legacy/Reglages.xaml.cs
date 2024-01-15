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
    /// Logique d'interaction pour Reglages.xaml
    /// </summary>
    public partial class Reglages : Window
    {
        public Reglages()
        {
            InitializeComponent();
        }
        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }
        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }
    }
}
