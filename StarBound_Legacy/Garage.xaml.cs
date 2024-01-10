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

       
        
        public int vieJoueur = 3;
        const int MAX_VIE = MainWindow.MAX_VIE;
        const int POINT_DEPART_BAR_VIE = 50, ECART_ENTRE_COEUR = 40, HAUTEUR_ELEMENT_BAS = 50;
        private DispatcherTimer minuterie;
        ImageBrush imgCoeur = new ImageBrush();

        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }
        private double hauteurFenetre;
        private double largeurFenetre;

        public Rectangle[] vie = new Rectangle[MAX_VIE];
        public Garage()
        {
            InitializeComponent();
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/coeur.png"));
            canvGarage.Height = SystemParameters.PrimaryScreenHeight;
            canvGarage.Width = SystemParameters.PrimaryScreenWidth;
            hauteurFenetre = canvGarage.Height;
            largeurFenetre = canvGarage.Width;
            Canvas.SetTop(btAcheterVie, hauteurFenetre - HAUTEUR_ELEMENT_BAS);
            Canvas.SetLeft(btAcheterVie, POINT_DEPART_BAR_VIE - 40);
            Canvas.SetTop(btRetour, hauteurFenetre - HAUTEUR_ELEMENT_BAS);
            Canvas.SetLeft(btRetour, largeurFenetre - 250);
            Canvas.SetTop(btRejouer, hauteurFenetre - HAUTEUR_ELEMENT_BAS);
            Canvas.SetLeft(btRejouer, largeurFenetre - 120);
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
            if (vieJoueur < MAX_VIE)
            {
                vieJoueur++;
            }
        }

        private void RetourMenu(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }

        private void Rejouer(object sender, RoutedEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }

        private void AfficheVie()
        {
            for (int i = 0; i < MAX_VIE; i++)
            {
                if (i < vieJoueur)
                {
                    vie[i].Opacity = 100;
                }
                else
                {
                    vie[i].Opacity = 0;
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
                canvGarage.Children.Add(coeur);
                Canvas.SetTop(coeur, canvGarage.Height - HAUTEUR_ELEMENT_BAS);
                Canvas.SetLeft(coeur, POINT_DEPART_BAR_VIE + ECART_ENTRE_COEUR * i);
                vie[i] = coeur;
            }
        }




    }
}
