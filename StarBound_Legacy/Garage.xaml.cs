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

        Rectangle[] barreVie;
        private ImageBrush boutonRetourMenuAppuye = new ImageBrush();
        private ImageBrush boutonRetourMenuRelache = new ImageBrush();
        private ImageBrush boutonRejouerAppuye = new ImageBrush();
        private ImageBrush boutonRejouerRelache = new ImageBrush();
        private ImageBrush fondObjetGarage = new ImageBrush();

        public Garage(MainWindow fenetre)
        {
            InitializeComponent();
            boutonRetourMenuAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourMenuAppuye.png"));
            boutonRetourMenuRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourMenuRelache.png"));
            boutonRejouerAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/boutonRejouerAppuye.png"));
            boutonRejouerRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/boutonRejouerRelache.png"));
            fondObjetGarage.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/ObjetGarage.png"));
            barreVie = new Rectangle[10] { rectCoeur1, rectCoeur2, rectCoeur3, rectCoeur4, rectCoeur5, rectCoeur6, rectCoeur7, rectCoeur8, rectCoeur9, rectCoeur10 };
            //int vie = this.Fenetre.VieJoueurDebutPartie;
            Rectangle[] fondObjets = new Rectangle[6] { rectObjet1, rectObjet2, rectObjet3, rectObjet4, rectObjet5, rectObjet6 };
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/coeur.png"));
            this.Fenetre = fenetre;
            for (int i = 0; i < 6; i++)
            {
                fondObjets[i].Fill = fondObjetGarage;
            }
            for (int i = 0; i < this.Fenetre.VieJoueurDebutPartie; i++)
            {
                barreVie[i].Fill = imgCoeur;
            }
            this.TxtCreditPoint.Text = this.Fenetre.PointCredit.ToString();
        }

        

        private void AcheterVie(object sender, RoutedEventArgs e)
        {
            if (this.Fenetre.VieJoueurDebutPartie < this.Fenetre.MAX_VIE)
            {
                this.Fenetre.VieJoueurDebutPartie++;
                for (int i = 0; i < this.Fenetre.VieJoueurDebutPartie; i++)
                {
                    barreVie[i].Fill = imgCoeur;
                }
            }
        }

        

        

        private void RetourEntreeSouris(object sender, MouseEventArgs e)
        {
            rectRetourMenu.Fill = boutonRetourMenuAppuye;
        }

        private void RetourSortieSouris(object sender, MouseEventArgs e)
        {
            rectRetourMenu.Fill = boutonRetourMenuRelache;
        }

        private void menuPrincipal(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }

        private void RejouerSortieSouris(object sender, MouseEventArgs e)
        {
            rectRejouer.Fill = boutonRejouerRelache;
        }

        private void Rejouer(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }

        private void RejouerEntreeSouris(object sender, MouseEventArgs e)
        {
            rectRejouer.Fill = boutonRejouerAppuye;
        }
    }
}
