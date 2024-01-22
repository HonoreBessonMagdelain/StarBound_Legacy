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

        private readonly int PRIX_SOIN = 150;
        private readonly int PRIX_BOMBE = 250;
        private readonly int PRIX_BOUCLIER = 100;
        private readonly int PRIX_PISTOLET_LASER = 200;
        private readonly int PRIX_LANCE_BOMBE = 100;
        private readonly int PRIX_MINIGUN = 150;
        private readonly int PRIX_COEUR = 150;

        private const int MAX_SOIN = 5;
        private const int MAX_BOMBE = 1;
        private const int MAX_BOUCLIER = 5;

        private String itemSelectionne;
        private readonly String OBJETS_POSSEDES = "Objets possedes : ";

        //constante pour pistolet laser
        private const int LARGEUR_BALLE_LASER = 30, HAUTEUR_BALLE_LASER = 3, TPS_RECHARGE_LASER = 1, BALLE_PAR_TIR_LASER = 30;
        private const int NUM_PISTOLET_LASER = 2;
        private const int VIT_BALLE_LASER = 40;
        //constante pour lance-bombe
        private const int LARGEUR_BALLE_LANCE_BOMBE = 20, HAUTEUR_BALLE_LANCE_BOMBE = 15, TPS_RECHARGE_LANCE_BOMBE = 6, BALLE_PAR_TIR_LANCE_BOMBE = 4;
        private const int NUM_LANCE_BOMBE = 3;
        private const int VIT_BALLE_LANCE_BOMBE = 10;
        //cosntante pour minigun
        private const int LARGEUR_BALLE_MINIGUN = 20, HAUTEUR_BALLE_MINIGUN = 5, TPS_RECHARGE_MINIGUN = 2, BALLE_PAR_TIR_MINIGUN = 25;
        private const int NUM_MINIGUN = 4;
        private const int VIT_BALLE_MINIGUN = 25;

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

        

        private void ActualisationSoinsPossedes()
        {
            txtNombrePossedes.Text = OBJETS_POSSEDES + this.Fenetre.Soins.ToString();
        }
        private void ActualisationBombesPossedees()
        {
            txtNombrePossedes.Text = OBJETS_POSSEDES + this.Fenetre.Bombes.ToString();
        }
        private void ActualisationBoucliersPossedes()
        {
            txtNombrePossedes.Text = OBJETS_POSSEDES + this.Fenetre.Boucliers.ToString();

        }
        private void ActualisationLanceBombesPossede()
        {
            String possede;
            if (this.Fenetre.Lancebombe)
                possede = "1";
            else possede = "0";
            txtNombrePossedes.Text = OBJETS_POSSEDES + possede;
        }
        private void ActualisationPistoletLaserPossede()
        {
            String possede;
            if (this.Fenetre.PistoletLaser)
                possede = "1";
            else possede = "0";
            txtNombrePossedes.Text = OBJETS_POSSEDES + possede;
        }
        private void ActualisationMiniGunPossede()
        {
            String possede;
            if (this.Fenetre.MiniGun)
                possede = "1";
            else possede = "0";
            txtNombrePossedes.Text = OBJETS_POSSEDES + possede;
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
            ActualisationSoinsPossedes();

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
            ActualisationBombesPossedees() ;
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
            ActualisationBoucliersPossedes();
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
            ActualisationPistoletLaserPossede();


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
            ActualisationLanceBombesPossede() ;
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
            ActualisationMiniGunPossede();
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
            txtNombrePossedes.Text = "";
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
                                ActualisationSoinsPossedes();
                            }
                            break;
                        }
                    case "bombe":
                        {
                            if (this.Fenetre.Bombes < MAX_BOMBE)
                            {
                                this.Fenetre.Bombes++;
                                this.Fenetre.PointCredit -= PRIX_BOMBE;
                                ActualisationBombesPossedees();
                            }
                            break;
                        }
                    case "bouclier":
                        {
                            if (this.Fenetre.Boucliers < MAX_BOUCLIER)
                            {
                                this.Fenetre.Boucliers++;
                                this.Fenetre.PointCredit -= PRIX_BOUCLIER;
                                ActualisationBoucliersPossedes();
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
                                this.Fenetre.CanonActuel = NUM_PISTOLET_LASER;
                                ChangeCaracteristiqueCanon(HAUTEUR_BALLE_LASER, LARGEUR_BALLE_LASER, TPS_RECHARGE_LASER, BALLE_PAR_TIR_LASER, VIT_BALLE_LASER);
                                ActualisationPistoletLaserPossede();
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
                                this.Fenetre.CanonActuel = NUM_LANCE_BOMBE;
                                this.Fenetre.PointCredit -= PRIX_LANCE_BOMBE;
                                ChangeCaracteristiqueCanon(HAUTEUR_BALLE_LANCE_BOMBE, LARGEUR_BALLE_LANCE_BOMBE, TPS_RECHARGE_LANCE_BOMBE, BALLE_PAR_TIR_LANCE_BOMBE, VIT_BALLE_LANCE_BOMBE);
                                ActualisationLanceBombesPossede();
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
                                this.Fenetre.CanonActuel = NUM_MINIGUN;
                                this.Fenetre.PointCredit -= PRIX_SOIN;
                                ChangeCaracteristiqueCanon(HAUTEUR_BALLE_MINIGUN, LARGEUR_BALLE_MINIGUN, TPS_RECHARGE_MINIGUN, BALLE_PAR_TIR_MINIGUN, VIT_BALLE_MINIGUN);
                                ActualisationMiniGunPossede();
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
        private void ChangeCaracteristiqueCanon(int hauteur, int largeur, int tps_recharge, int limiteParTir, int vitesseBalle)
        {
            this.Fenetre.HauteurBalleJoueur = hauteur;
            this.Fenetre.LargeurBalleJoueur = largeur;
            this.Fenetre.TempsRechargement = tps_recharge;
            this.Fenetre.LimiteBalleParTir = limiteParTir;
            this.Fenetre.VitesseBalle = vitesseBalle;
        }

    }
}
