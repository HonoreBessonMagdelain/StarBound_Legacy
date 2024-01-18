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

        private ImageBrush soin = new ImageBrush();
        private ImageBrush soinSelectionne = new ImageBrush();
        private ImageBrush bombe = new ImageBrush();
        private ImageBrush bombeSelectionne = new ImageBrush();
        private ImageBrush bouclier = new ImageBrush();
        private ImageBrush bouclierSelectionne = new ImageBrush();

        private ImageBrush pistoletLaser = new ImageBrush();
        private ImageBrush pistoletLaserSelectionne = new ImageBrush();
        private ImageBrush lanceBombe = new ImageBrush();
        private ImageBrush lanceBombeSelectionne = new ImageBrush();
        private ImageBrush miniGun = new ImageBrush();
        private ImageBrush miniGunSelectionne = new ImageBrush();

        private ImageBrush soinDescription = new ImageBrush();
        private ImageBrush bouclierDescription = new ImageBrush();
        private ImageBrush bombeDescription = new ImageBrush();

        private ImageBrush pistoletLaserDescription = new ImageBrush();
        private ImageBrush lanceBombeDescription = new ImageBrush();
        private ImageBrush miniGunDescription = new ImageBrush();

        private ImageBrush plusDescription = new ImageBrush();
        private ImageBrush plusRelache = new ImageBrush();
        private ImageBrush plusAppuye = new ImageBrush();

        private ImageBrush vaisseau2 = new ImageBrush();
        private ImageBrush vaisseau3 = new ImageBrush();
        private ImageBrush vaisseau4 = new ImageBrush();

        private ImageBrush prixRelache = new ImageBrush();
        private ImageBrush prixAppuye = new ImageBrush();

        private readonly int PRIX_SOIN = 200;
        private readonly int PRIX_BOMBE = 300;
        private readonly int PRIX_BOUCLIER = 400;
        private readonly int PRIX_PISTOLET_LASER = 500;
        private readonly int PRIX_LANCE_BOMBE = 500;
        private readonly int PRIX_MINIGUN = 500;
        private readonly int PRIX_COEUR = 500;

        private String itemSelectionne;

        private readonly String UNITE_PRIX = " pts";

        public Garage(MainWindow fenetre)
        {
            InitializeComponent();
            boutonRetourMenuAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourMenuAppuye.png"));
            boutonRetourMenuRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/RetourMenuRelache.png"));
            boutonRejouerAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/boutonRejouerAppuye.png"));
            boutonRejouerRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/boutonRejouerRelache.png"));
            fondObjetGarage.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/ObjetGarage.png"));

            soin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/Soin.png"));
            soinSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/SoinSelectionne.png"));
            bombe.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/BombeNucleaire.png"));
            bombeSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/BombeNucleaireSelectionne.png"));
            bouclier.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/Bouclier.png"));
            bouclierSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/BouclierSelectionne.png"));

            pistoletLaser.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/PistoletLaser.png"));
            pistoletLaserSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/PistoletLaserSelectionne.png"));
            lanceBombe.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/LanceBombe.png"));
            lanceBombeSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/LanceBombeSelectionne.png"));
            miniGun.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/MiniGun.png"));
            miniGunSelectionne.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/MiniGunSelectionne.png"));

            soinDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/SoinDescription.png"));
            bouclierDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/BouclierDescription.png"));
            bombeDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/BombeDescription.png"));

            pistoletLaserDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/PistoletLaserDescription.png"));
            lanceBombeDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/LanceBombeDescription.png"));
            miniGunDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/MiniGunDescription.png"));

            plusDescription.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/textes/PlusDescription.png"));
            plusRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/PlusRelache.png"));
            plusAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/PlusAppuye.png"));

            vaisseau2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon2.png"));
            vaisseau3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon3.png"));
            vaisseau4.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vaisseaux/Vaisseau1canon4.png"));

            prixRelache.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/PrixRelache.png"));
            prixAppuye.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Boutons/PrixAppuye.png"));

            barreVie = new Rectangle[10] { rectCoeur1, rectCoeur2, rectCoeur3, rectCoeur4, rectCoeur5, rectCoeur6, rectCoeur7, rectCoeur8, rectCoeur9, rectCoeur10 };
            //int vie = this.Fenetre.VieJoueurDebutPartie;
            Rectangle[] fondObjets = new Rectangle[6] { rectObjet1, rectObjet2, rectObjet3, rectObjet4, rectObjet5, rectObjet6 };
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/Coeur.png"));
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

        private void SoinEntreeSouris(object sender, MouseEventArgs e)
        {
            rectSoin.Fill = soinSelectionne;
        }

        private void SoinSortieSouris(object sender, MouseEventArgs e)
        {
            rectSoin.Fill = soin;
        }

        private void Soin(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = soinDescription;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_SOIN.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "soin";

        }

        private void BombeEntreeSouris(object sender, MouseEventArgs e)
        {
            rectBombe.Fill = bombeSelectionne;
        }

        private void BombeSortieSouris(object sender, MouseEventArgs e)
        {
            rectBombe.Fill= bombe;
        }

        private void Bombe(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = bombeDescription;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_BOMBE.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "bombe";

        }

        private void BouclierEntreeSouris(object sender, MouseEventArgs e)
        {
            rectBouclier.Fill = bouclierSelectionne;
        }

        private void BouclierSortieSouris(object sender, MouseEventArgs e)
        {
            rectBouclier.Fill = bouclier;
        }

        private void Bouclier(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = bouclierDescription;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_BOUCLIER.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "bouclier";
        }

        private void PistoletLaserEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPistoletLaser.Fill = pistoletLaserSelectionne;
        }

        private void PistoletLaserSortieSouris(object sender, MouseEventArgs e)
        {
            rectPistoletLaser.Fill = pistoletLaser;
        }

        private void PistoletLaser(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = pistoletLaserDescription;
            rectVaisseau.Fill = vaisseau2;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_PISTOLET_LASER.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "pistoletLaser";
        }

        private void LanceBombeEntreeSouris(object sender, MouseEventArgs e)
        {
            rectLanceBombe.Fill = lanceBombeSelectionne;
        }

        private void LanceBombeSortieSouris(object sender, MouseEventArgs e)
        {
            rectLanceBombe.Fill = lanceBombe;
        }

        private void LanceBombe(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = lanceBombeDescription;
            rectVaisseau.Fill = vaisseau3;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_LANCE_BOMBE.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "lanceBombe";
        }

        private void MiniGunEntreeSouris(object sender, MouseEventArgs e)
        {
            rectMiniGun.Fill = miniGunSelectionne;
        }

        private void MiniGunSortieSouris(object sender, MouseEventArgs e)
        {
            rectMiniGun.Fill = miniGun;
        }

        private void MiniGun(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = miniGunDescription;
            rectVaisseau.Fill = vaisseau4;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_MINIGUN.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "miniGun";
        }

        private void PlusEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPlus.Fill = plusAppuye;
        }

        private void PlusSortieSouris(object sender, MouseEventArgs e)
        {
            rectPlus.Fill = plusRelache;
        }

        private void Plus(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = plusDescription;
            rectPrix.Fill = prixRelache;
            txtPrix.Text = PRIX_COEUR.ToString() + UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "vie";
        }

        private void PrixEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPrix.Fill = prixAppuye;
        }

        private void PrixSortieSouris(object sender, MouseEventArgs e)
        {
            rectPrix.Fill= prixRelache;
        }

        private void Acheter(String itemSelectionne)
        {

        }

        private void TxtPrixEntreeSouris(object sender, MouseEventArgs e)
        {
            if (txtPrix.Foreground != null)
                rectPrix.Fill = prixAppuye;
        }

        private void TxtPrixSortieSouris(object sender, MouseEventArgs e)
        {
            if (txtPrix.Foreground != null)
                rectPrix.Fill = prixAppuye;
        }
    }
}
