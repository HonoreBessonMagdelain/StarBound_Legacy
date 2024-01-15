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

        const int MAX_VIE = MainWindow.MAX_VIE;
        const int POINT_DEPART_BAR_VIE = 50, ECART_ENTRE_COEUR = 40, HAUTEUR_ELEMENT_BAS = 50;
        private DispatcherTimer minuterie;
        ImageBrush imgCoeur = new ImageBrush();

        private double hauteurFenetre;
        private double largeurFenetre;

        public Rectangle[] barVie = new Rectangle[MAX_VIE];
        public Garage()
        {
            InitializeComponent();
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/coeur.png"));
            ///canvGarage.Height = SystemParameters.PrimaryScreenHeight;
            
            CreerBarVie();
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16);
            minuterie.Tick += GameEngine;
            minuterie.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            AfficheVie();
        }

        private void AcheterVie(object sender, RoutedEventArgs e)
        {
            if (this.Fenetre.vieJoueurDebutPartie < MAX_VIE)
            {
                this.Fenetre.vieJoueurDebutPartie++;
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

        private void AfficheVie()
        {
            for (int i = 0; i < MAX_VIE; i++)
            {
                if (i < this.Fenetre.vieJoueurDebutPartie)
                {
                    barVie[i].Opacity = 100;
                }
                else
                {
                    barVie[i].Opacity = 0.5;
                }
            }
        }

        private void CreerBarVie()
        {
            for (int i = 0;i < MAX_VIE; i++)
            {
                Rectangle coeur = new Rectangle()
                {
                    Width = 30,
                    Height = 30,
                };
                coeur.Fill = imgCoeur;
                //canvGarage.Children.Add(coeur);
                //Canvas.SetTop(coeur, canvGarage.Height - HAUTEUR_ELEMENT_BAS);
                Canvas.SetLeft(coeur, POINT_DEPART_BAR_VIE + ECART_ENTRE_COEUR * i);
                barVie[i] = coeur;
            }
        }




    }
}
