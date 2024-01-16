using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace StarBound_Legacy
{
    /// <summary>
    /// Logique d'interaction pour Garage.xaml
    /// </summary>
    public partial class Garage : Window
    {

        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }
        ImageBrush imgCoeur = new ImageBrush();

        


        public Garage()
        {
            InitializeComponent();
            //Rectangle[] barreVie = new Rectangle[rectCoeur1, rectCoeur2, rectCoeur3, rectCoeur4, rectCoeur5, rectCoeur6, rectCoeur7, rectCoeur8, rectCoeur9, rectCoeur10];

            //txtNbPts.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./policesEcritures/ARCADECLASSIC.TTF");
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/coeur.png"));
            //for(int i = 0; i < this.Fenetre.VieJoueurDebutPartie; i++)
            //{
            //    rectCoeur6.Fill = imgCoeur;
            //}
        }

        

        private void AcheterVie(object sender, RoutedEventArgs e)
        {
            if (this.Fenetre.VieJoueurDebutPartie < this.Fenetre.MAX_VIE)
            {
                this.Fenetre.VieJoueurDebutPartie++;
            }
        }

        

        private void Rejouer(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }

        

        private void Jouer(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        

        




    }
}
