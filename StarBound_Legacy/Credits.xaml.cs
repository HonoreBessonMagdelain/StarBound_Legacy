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
    /// Logique d'interaction pour Credits.xaml
    /// </summary>
    public partial class Credits : Window
    {
        private ImageBrush boutonRetourAppuye = new ImageBrush();
        private ImageBrush boutonRetourRelache = new ImageBrush();
        public Credits()
        {
            InitializeComponent();
            boutonRetourAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourAppuye.png"));
            boutonRetourRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourRelache.png"));
        }
        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }

        

        private void RetourEntreeSouris(object sender, MouseEventArgs e)
        {
            rectRetour.Fill = boutonRetourAppuye;
        }

        private void RetourSortieSouris(object sender, MouseEventArgs e)
        {
            rectRetour.Fill = boutonRetourRelache;
        }

        private void Retour(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }
    }
}
