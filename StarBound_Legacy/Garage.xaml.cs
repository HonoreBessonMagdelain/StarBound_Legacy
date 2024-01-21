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

        private readonly int PRIX_SOIN = 100;
        private readonly int PRIX_BOMBE = 500;
        private readonly int PRIX_BOUCLIER = 50;
        private readonly int PRIX_PISTOLET_LASER = 50;
        private readonly int PRIX_LANCE_BOMBE = 50;
        private readonly int PRIX_MINIGUN = 50;
        private readonly int PRIX_COEUR = 150;

        private const int MAX_SOIN = 5;
        private const int MAX_BOMBE = 1;
        private const int MAX_BOUCLIER = 5;

        private String itemSelectionne;

        

        public Garage(MainWindow fenetre)
        {
            InitializeComponent();
            Apparences.InitialisationImagesGarage();
            
            barreVie = new Rectangle[10] { rectCoeur1, rectCoeur2, rectCoeur3, rectCoeur4, rectCoeur5, rectCoeur6, rectCoeur7, rectCoeur8, rectCoeur9, rectCoeur10 };
            //int vie = this.Fenetre.VieJoueurDebutPartie;
            Rectangle[] fondObjets = new Rectangle[6] { rectObjet1, rectObjet2, rectObjet3, rectObjet4, rectObjet5, rectObjet6 };
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/Coeur.png"));
            this.Fenetre = fenetre;
            for (int i = 0; i < 6; i++)
            {
                fondObjets[i].Fill = Apparences.fondObjetGarage;
            }
            ActualisationDonnees();
            
            
        }
        private void ActualisationDonnees()
        {
            for (int i = 0; i < this.Fenetre.VieJoueurDebutPartie; i++)
            {
                barreVie[i].Fill = imgCoeur;
                TxtCreditPoint.Text = this.Fenetre.PointCredit.ToString() + this.Fenetre.UNITE_PRIX;
            }
        }

        

        

        

        

        private void RetourEntreeSouris(object sender, MouseEventArgs e)
        {
            rectRetourMenu.Fill = Apparences.boutonRetourMenuAppuye;
        }

        private void RetourSortieSouris(object sender, MouseEventArgs e)
        {
            rectRetourMenu.Fill = Apparences.boutonRetourMenuRelache;
        }

        private void menuPrincipal(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "menuPrincipal";
            this.DialogResult = true;
        }

        private void RejouerSortieSouris(object sender, MouseEventArgs e)
        {
            rectRejouer.Fill = Apparences.boutonRejouerRelache;
        }

        private void Rejouer(object sender, MouseButtonEventArgs e)
        {
            this.Fenetre.FenetreAOuvrir = "jouer";
            this.DialogResult = true;
        }

        private void RejouerEntreeSouris(object sender, MouseEventArgs e)
        {
            rectRejouer.Fill = Apparences.boutonRejouerAppuye;
        }

        private void SoinEntreeSouris(object sender, MouseEventArgs e)
        {
            rectSoin.Fill = Apparences.soinSelectionne;
        }

        private void SoinSortieSouris(object sender, MouseEventArgs e)
        {
            rectSoin.Fill = Apparences.soin;
        }

        private void Soin(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.soinDescription;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_SOIN.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "soin";

        }

        private void BombeEntreeSouris(object sender, MouseEventArgs e)
        {
            rectBombe.Fill = Apparences.bombeSelectionne;
        }

        private void BombeSortieSouris(object sender, MouseEventArgs e)
        {
            rectBombe.Fill= Apparences.bombe;
        }

        private void Bombe(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.bombeDescription;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_BOMBE.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "bombe";

        }

        private void BouclierEntreeSouris(object sender, MouseEventArgs e)
        {
            rectBouclier.Fill = Apparences.bouclierSelectionne;
        }

        private void BouclierSortieSouris(object sender, MouseEventArgs e)
        {
            rectBouclier.Fill = Apparences.bouclier;
        }

        private void Bouclier(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.bouclierDescription;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_BOUCLIER.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "bouclier";
        }

        private void PistoletLaserEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPistoletLaser.Fill = Apparences.pistoletLaserSelectionne;
        }

        private void PistoletLaserSortieSouris(object sender, MouseEventArgs e)
        {
            rectPistoletLaser.Fill = Apparences.pistoletLaser;
        }

        private void PistoletLaser(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.pistoletLaserDescription;
            rectVaisseau.Fill = Apparences.vaisseau2;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_PISTOLET_LASER.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "pistoletLaser";
        }

        private void LanceBombeEntreeSouris(object sender, MouseEventArgs e)
        {
            rectLanceBombe.Fill = Apparences.lanceBombeSelectionne;
        }

        private void LanceBombeSortieSouris(object sender, MouseEventArgs e)
        {
            rectLanceBombe.Fill = Apparences.lanceBombe;
        }

        private void LanceBombe(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.lanceBombeDescription;
            rectVaisseau.Fill = Apparences.vaisseau3;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_LANCE_BOMBE.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "lanceBombe";
        }

        private void MiniGunEntreeSouris(object sender, MouseEventArgs e)
        {
            rectMiniGun.Fill = Apparences.miniGunSelectionne;
        }

        private void MiniGunSortieSouris(object sender, MouseEventArgs e)
        {
            rectMiniGun.Fill = Apparences.miniGun;
        }

        private void MiniGun(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.miniGunDescription;
            rectVaisseau.Fill = Apparences.vaisseau4;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_MINIGUN.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "miniGun";
        }

        private void PlusEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPlus.Fill = Apparences.plusAppuye;
        }

        private void PlusSortieSouris(object sender, MouseEventArgs e)
        {
            rectPlus.Fill = Apparences.plusRelache;
        }

        private void Plus(object sender, MouseButtonEventArgs e)
        {
            rectDescription.Fill = Apparences.plusDescription;
            rectPrix.Fill = Apparences.prixRelache;
            txtPrix.Text = PRIX_COEUR.ToString() + this.Fenetre.UNITE_PRIX;
            txtPrix.Foreground = Brushes.Black;
            itemSelectionne = "vie";
        }

        private void PrixEntreeSouris(object sender, MouseEventArgs e)
        {
            rectPrix.Fill = Apparences.prixAppuye;
        }

        private void PrixSortieSouris(object sender, MouseEventArgs e)
        {
            rectPrix.Fill= Apparences.prixRelache;
        }
        private void Acheter(object sender, MouseButtonEventArgs e)
        {
            if (this.Fenetre.PointCredit >= int.Parse(txtPrix.Text.Substring(0, 3)))
            {
                switch(itemSelectionne)
                {
                    case "soin":
                        {
                            if (this.Fenetre.Soins < MAX_SOIN)
                            {
                                this.Fenetre.Soins++;
                                this.Fenetre.PointCredit -= PRIX_SOIN;
                            }
                            break;
                        }
                    case "bombe":
                        {
                            if (this.Fenetre.Bombes < MAX_BOMBE)
                            {
                                this.Fenetre.Bombes++;
                                this.Fenetre.PointCredit -= PRIX_BOMBE;
                            }
                            break;
                        }
                    case "bouclier":
                        {
                            if (this.Fenetre.Boucliers < MAX_BOUCLIER)
                            {
                                this.Fenetre.Boucliers++;
                                this.Fenetre.PointCredit -= PRIX_BOUCLIER;
                            }
                            break;
                        }
                    case "pistoletLaser":
                        {
                            if (!this.Fenetre.PistoletLaser)
                            {
                                this.Fenetre.PistoletLaser  = true;
                                this.Fenetre.MiniGun = false;
                                this.Fenetre.Lancebombe = false;
                                this.Fenetre.PointCredit -= PRIX_PISTOLET_LASER;
                                this.Fenetre.TempsRechargement = 0;
                                this.Fenetre.LimiteBalleParTir = 50;
                            }
                            break;
                        }
                    case "lanceBombe":
                        {
                            if (!this.Fenetre.Lancebombe)
                            {
                                this.Fenetre.Lancebombe = true;
                                this.Fenetre.PistoletLaser  = false;
                                this.Fenetre.MiniGun = false;
                                this.Fenetre.PointCredit -= PRIX_LANCE_BOMBE;
                                this.Fenetre.TempsRechargement = 8;
                                this.Fenetre.LimiteBalleParTir = 3;
                            }
                            
                            break;
                        }
                    case "miniGun":
                        {
                            if (!this.Fenetre.MiniGun)
                            {
                                this.Fenetre.MiniGun = true;
                                this.Fenetre.PistoletLaser  = false;
                                this.Fenetre.Lancebombe = false;
                                this.Fenetre.PointCredit -= PRIX_SOIN;
                                this.Fenetre.TempsRechargement = 2;
                                this.Fenetre.LimiteBalleParTir = 25;
                            }
                            break;
                        }
                    case "vie":
                        {
                            if (this.Fenetre.VieJoueurDebutPartie < this.Fenetre.MAX_VIE)
                            {
                                this.Fenetre.VieJoueurDebutPartie++;
                                this.Fenetre.PointCredit -= PRIX_COEUR;
                            }
                            break;
                        }
                }
                ActualisationDonnees();
            }
            
        }

        private void TxtPrixEntreeSouris(object sender, MouseEventArgs e)
        {
            if (txtPrix.Foreground != null)
                rectPrix.Fill = Apparences.prixAppuye;
        }

        private void TxtPrixSortieSouris(object sender, MouseEventArgs e)
        {
            if (txtPrix.Foreground != null)
                rectPrix.Fill = Apparences.prixAppuye;
        }

    }
}
