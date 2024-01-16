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
        private ImageBrush boutonJouerAppuye = new ImageBrush();
        private ImageBrush boutonJouerRelache = new ImageBrush();
        public MenuPrincipal()
        {            
            InitializeComponent();
            boutonJouerAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/JouerAppuye.png"));
            boutonJouerAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/JouerRelache.png"));

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
            this.Fenetre.FenetreAOuvrir = "quitter";
            this.DialogResult = true;
        }

        private void Credits(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "credits";
            this.DialogResult = true;
            
            
        }
        private void Reglages(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "reglages";
            this.DialogResult = true;
        }
        

        private void JouerEntreeSouris(object sender, MouseEventArgs e)
        {
            rectJouer.Fill = boutonJouerAppuye;
        }

        private void JouerSortieSouris(object sender, MouseEventArgs e)
        {
            rectJouer.Fill = boutonJouerRelache;
        }

        

        private void Jouer(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }
    }
}
