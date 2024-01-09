using System;
using System.Collections.Generic;
using System.Linq;
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
        const int MAX_VIE = 10;
        const int POINT_DEPART_BAR_VIE = 50, ECART_ENTRE_COEUR = 40, HAUTEUR_BAR_VIE = 30;

        public int vieJoueur = 3;
        private DispatcherTimer minuterie;
        ImageBrush imgCoeur = new ImageBrush();


        public Rectangle[] vie = new Rectangle[MAX_VIE];
        public Garage()
        {
            InitializeComponent(); 
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/coeur.png"));
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
                Canvas.SetTop(coeur, canvGarage.Height - HAUTEUR_BAR_VIE);
                Canvas.SetLeft(coeur, POINT_DEPART_BAR_VIE + ECART_ENTRE_COEUR * i);
                vie[i] = coeur;
            }
        }
    }
}
