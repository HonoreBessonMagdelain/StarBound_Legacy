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
        private ImageBrush boutonGarageAppuye = new ImageBrush();
        private ImageBrush boutonGarageRelache = new ImageBrush();
        private ImageBrush boutonParametresAppuye = new ImageBrush();
        private ImageBrush boutonParametresRelache = new ImageBrush();
        private ImageBrush boutonCreditsAppuye = new ImageBrush();
        private ImageBrush boutonCreditsRelache = new ImageBrush();
        private ImageBrush boutonQuitterAppuye = new ImageBrush();
        private ImageBrush boutonQuitterRelache = new ImageBrush();
        public MenuPrincipal()
        {            
            InitializeComponent();
            boutonJouerAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/JouerAppuye1.png"));
            boutonJouerRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/JouerRelache1.png"));
            boutonGarageAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/GarageAppuye1.png"));
            boutonGarageRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/GarageRelache1.png"));
            boutonParametresAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/ParametresAppuye.png"));
            boutonParametresRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/ParametresRelache.png"));
            boutonCreditsAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/CreditsAppuye.png"));
            boutonCreditsRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/CreditsRelache.png"));
            boutonQuitterAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/QuitterAppuye.png"));
            boutonQuitterRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/QuitterRelache.png"));

        }
        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
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

        private void GarageEntreeSouris(object sender, MouseEventArgs e)
        {
            rectGarage.Fill = boutonGarageAppuye;
        }

        private void GarageSortieSouris(object sender, MouseEventArgs e)
        {
            rectGarage.Fill = boutonGarageRelache;   
        }

        private void Garage(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "garage";
            this.DialogResult = true;
        }

        private void ParametresEntreeSouris(object sender, MouseEventArgs e)
        {
            rectParametres.Fill = boutonParametresAppuye;
        }

        private void ParametresSortieSouris(object sender, MouseEventArgs e)
        {
            rectParametres.Fill = boutonParametresRelache;
        }

        private void Parametres(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "reglages";
            this.DialogResult = true;
        }

        private void CreditsEntreeSouris(object sender, MouseEventArgs e)
        {
            rectCredits.Fill = boutonCreditsAppuye;
        }

        private void CreditsSortieSouris(object sender, MouseEventArgs e)
        {
            rectCredits.Fill = boutonCreditsRelache;
        }

        private void Credits(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "credits";
            this.DialogResult = true;
        }

        private void QuitterEntreeSouris(object sender, MouseEventArgs e)
        {
            rectQuitter.Fill = boutonQuitterAppuye;
        }

        private void QuitterSortieSouris(object sender, MouseEventArgs e)
        {
            rectQuitter.Fill = boutonQuitterRelache;
        }

        private void Quitter(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "quitter";
            this.DialogResult = true;
        }
    }
}
