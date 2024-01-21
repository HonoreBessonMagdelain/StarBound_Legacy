using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace StarBound_Legacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // LIGNES POUR TOUT LE PROGRAMME


        private String fenetreAOuvrir;
        public String FenetreAOuvrir
        {
            get { return fenetreAOuvrir; }
            set
            {
                if (value != "garage" && value != "jouer" && value != "credits" && value != "reglages" && value != "menuPrincipal" && value != "quitter")
                    throw new ArgumentException("Il faut rentrer un nom de fenetre coorect");
                fenetreAOuvrir = value;
            }
        }

        private double volumeSFXactuel;
        public double VolumeSFXactuel
        {
            get { return volumeSFXactuel; }
            set { volumeSFXactuel = value; }
        }

        private double volumeMusiqueActuel;
        public double VolumeMusiqueActuel
        {
            get { return volumeMusiqueActuel; }
            set { volumeMusiqueActuel = value; }
        }

        private int vieJoueurDebutPartie;
        public int VieJoueurDebutPartie
        {
            get { return vieJoueurDebutPartie; }
            set {
                if (value > MAX_VIE)
                    throw new ArgumentOutOfRangeException("la vie ne peut pas etre superieure à 10");
                vieJoueurDebutPartie = value; 
            }
        }
        private int pointCredit = 0;

        public int PointCredit
        {
            get { return pointCredit; }
            set { pointCredit = value; }
        }
        private int canonActuel = 1;

        public int CanonActuel
        {
            get { return canonActuel; }
            set { 
                if(value > NOMBRE_TYPES_CANON || value < NOMBRE_TYPES_CANON -(NOMBRE_TYPES_CANON-1))
                    throw new ArgumentOutOfRangeException("le type de canon doit être entre1 et 4");
                canonActuel = value; }
        }
        
        public readonly int NOMBRE_TYPES_CANON = 4;

        // propriétés pour l'obtention des objets dans le garage
        private bool pistoletLaser = false;

        public bool PistoletLaser
        {
            get { return pistoletLaser; }
            set { pistoletLaser = value; }
        }
        private bool lanceBombe = false;

        public bool Lancebombe
        {
            get { return lanceBombe; }
            set { lanceBombe = value; }
        }
        private bool miniGun = false;

        public bool MiniGun
        {
            get { return miniGun; }
            set { miniGun = value; }
        }
        private int soins = 0;

        public int Soins
        {
            get { return soins; }
            set { soins = value; }
        }
        private int bombes = 0;

        public int Bombes
        {
            get { return bombes; }
            set { bombes = value; }
        }
        private int boucliers = 0;

        public int Boucliers
        {
            get { return boucliers; }
            set { boucliers = value; }
        }
        //propriete de gestion du sons
        private double volumeSfx = 1;

        public double VolumeSfx
        {
            get { return volumeSfx; }
            set { volumeSfx = value; }
        }
        private double volumeSons = 1;

        public double VolumeSons
        {
            get { return volumeSons; }
            set { volumeSons = value; }
        }


        // creation des lecteurs de la musique
        public MediaPlayer musiqueMenu = new MediaPlayer();
        public MediaPlayer musiqueGameplay = new MediaPlayer();

        Random aleatoire = new Random();

        // unite des points
        public readonly String UNITE_PRIX = " pts";
        public readonly int MAX_VIE = 10;
        public readonly int MIN_VIE = 3;
        // liste des éléments rectangles
        private List<Rectangle> ElementsASupprimer = new List<Rectangle>();
        private List<Rectangle> ElementsAAjouter = new List<Rectangle>();
        
        // LIGNES POUR LE GAMEPLAY

        // FONCTIONNEMENT JEU

        // création variable minuterie
        private DispatcherTimer minuterie = new DispatcherTimer();
        
        // booléens pour detecter le tir du joueur
        private bool afficheDevbug = false;

        private const int NB_ENNEMI_DEPART = 2, NB_LIMITE_ENNEMI = 15, NB_ASTEROIDE_DEPART = 3, NB_LIMITE_ASTEROIDE = 5;
        //variable du score du joueur
        public int score = 0;
        public bool passpalier = false;
        public int palierActuel = 0;
        private ImageBrush imgCoeur = new ImageBrush();
        private ImageBrush imgDemiCoeur = new ImageBrush();
        Rectangle[] barreVie;


        bool quitter = false;
        bool jouer = false;

        // DECOR

        private const int TAILLE_PETITE_ETOILE = 15, TAILLE_MOY_ETOILE = 30, TAILLE_GRANDE_ETOILE = 50, TAILLE_PIEUVRE = 100;
        private const int NB_PETITE_ETOILE = 10, NB_MOY_ETOILE = 10, NB_GRANDE_ETOILE = 10;
        private int nbEnnemi = 0, nbAsteroid = 0;

        // entier nous permettant de charger les images des etoiles
        private int ImagesEtoiles = 0;
        private double vitesseEtoile1 = 1;
        private double vitesseEtoile2 = 2;
        private double vitesseEtoile3 = 3;
        private double vitessePieuvre = 2;

        // nombre de petites etoiles qui existent
        private int nombrePetitesEtoiles = 11;

        // JOUEUR

        private int hauteurBalleJoueur = 5;

        public int HauteurBalleJoueur
        {
            get { return hauteurBalleJoueur; }
            set { hauteurBalleJoueur = value; }
        }
        private int largeurBalleJoueur = 20;

        public int LargeurBalleJoueur
        {
            get { return largeurBalleJoueur; }
            set { largeurBalleJoueur = value; }
        }



        private bool tirer = false;
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush apparenceJoueur = new ImageBrush();
        // vitesse du joueur
        private int vitesseJoueur = 10;
        private int vieJoueur;
        private int vitesseBalle = 20;

        public int VitesseBalle
        {
            get { return vitesseBalle = 20; }
            set { vitesseBalle = value; }
        }


        //pour changer le temps de rechargement (cooldown)
        private int tempsRechargement = 5;

        public int TempsRechargement
        {
            get { return tempsRechargement; }
            set { tempsRechargement = value; }
        }

        // booléens pour les touches de deplacement
        private bool vaADroite, vaAGauche, vaEnHaut, vaEnBas = false;
        // booléens pour les touches d'objets spéciaux
        private bool utiliseBombe, utiliseSoin, utiliseBouclier = false;

        //limite le nombre de balle tirer par le joueur
        private const int LIMITE_BALLE_JOUEUR = 50;

        //pour augmenter le nombre de balle par salve
        private int limiteBalleParTir = 5;

        public int LimiteBalleParTir
        {
            get { return limiteBalleParTir; }
            set { limiteBalleParTir = value; }
        }

        private int balleParTir = 0;
        private int balletirer = 0;

        //variable des degat
        private const int DEGAT_ASTEROID = 3;
        private const int DEGAT_TIR_ENNEMI = 1;
        private const int DEGAT_ENNEMI = 2;

        // ENNEMIS

        // entier nous permettant de charger les images des ennemis
        private double vitesseBalleEnnemi = 10;
        private double vitesseEnnemi = 2;
        private double vitesseAsteroid = 5;
        private const int TAILLE_MIN_ASTEROID = 25, TAILLE_MAX_ASTEROID = 100;
        private ImageBrush apparenceEnnemi = new ImageBrush();
        private ImageBrush apparenceAsteroid = new ImageBrush();
        private const int TAILLE_ENNEMI = 50;
        private const int LONGUEUR_BALLE_ENNEMI = 30;
        private const int HAUTEUR_BALLE_ENNEMI = 5;
        private const double ACCELERATION_VITESSE_ENNEMI = 0.2;
        private const double ACCELERATION_VITESSE_BALLE_ENNEMI = 0.3;
        private const double ACCELERATION_VITESSE_ASTEROID = 0.2;
        private const double RATIO_TAILLE_ASTEROID = 2.5;
        private const double RATIO_TAILLE_ENNEMI = 1.5;
        private const double VITESSE_VERTICALE_ENNEMI = 2;
        private const double VIT_DEPART_ASTEROID = 8;
        private const double VIT_DEPART_ENNEMI = 2;
        private const double VIT_DEPART_BALLE_ENNEMI = 20;

        // timer tir et animation vaisseau
        private int timerTir = 0;
        private int timerTirMax = 5;
        private int animeVaisseau = 6;
        private int animeVaisseauMax = 6;
        private double minuterieBalle = 8;
        private double minuterieBalleLimite = 800;

        // les différents plans
        private int zIndexEnnemi = 6;
        private int zIndexAsteroids = 4;
        private int zIndexPetitesEtoiles = 1;
        private int zIndexMoyennesEtoiles = 2;
        private int zIndexGrandesEtoiles = 3;
        private int zIndexPieuvre = 3;
        private int zIndexBalleEnnemi = 5;
        private int zIndexBombeLancee = 7;

        // OBJETS

        private DispatcherTimer minuterieBombe = new DispatcherTimer();
        private static readonly int TAILLE_BOMBE = 40;
        private static readonly int TAILLE_BOUCLIER = 150;
        private int vitesseBombeLancee;
        private int comptageAcceleration = 1;
        private readonly int VITESSE_ACCELERATION_BOMBE = 8;
        private Rectangle rectBombeLancee = new Rectangle
        {
            Height = TAILLE_BOMBE,
            Width = TAILLE_BOMBE,
            Fill = Apparences.imgCoeurVide,
            Tag = "bombeLancee"
        };
        private Rectangle rectBouclierUtilise = new Rectangle
        {
            Height = TAILLE_BOUCLIER,
            Width = TAILLE_BOUCLIER,
            Fill = Apparences.bouclierUtilise,
            Tag = "bouclierUtilise"
        };

        // EFFETS

        private Rectangle rectExplosionBombe = new Rectangle
        {
            Height = TAILLE_BOMBE,
            Width = TAILLE_BOMBE,
            Fill = Apparences.explosionBombe
        };
        private int vitesseExpensionExplosion;
        private double abscisseExplosion;
        private double ordonneeExplosion;
        private int compteurDixSecondes = 0;

        public MainWindow()
        {
            #if DEBUG
            Console.WriteLine("Debug version");
            #endif
            InitializeComponent();
            Apparences.InitialisationImagesMainWindow();
            this.CanonActuel = 1;
            this.VieJoueurDebutPartie = MIN_VIE;
            this.FenetreAOuvrir = "menuPrincipal";
            // chargement de l’image du joueur 
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon1.png"));
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/Coeur.png"));
            imgDemiCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/DemiCoeur.png"));
            apparenceEnnemi.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Ennemis/Ennemi.png"));
            apparenceAsteroid.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Asteroids/Asteroid.png"));
            rectJoueur.Fill = apparenceJoueur;
            ControlFenetre();
        }
        public void initialisationJeux()
        {
            CreationEtoiles(NB_PETITE_ETOILE, TAILLE_PETITE_ETOILE, zIndexPetitesEtoiles); 
            CreationEtoiles(NB_MOY_ETOILE, TAILLE_MOY_ETOILE, zIndexMoyennesEtoiles);
            CreationEtoiles(NB_GRANDE_ETOILE, TAILLE_GRANDE_ETOILE, zIndexGrandesEtoiles);
            CreationPieuvre(TAILLE_PIEUVRE);
            CreationEnnemis(NB_ENNEMI_DEPART);
            CreationAsteroids(NB_ASTEROIDE_DEPART, TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
            Canvas.SetTop(rectExplosionBombe, -50);
            Canvas.SetLeft(rectExplosionBombe,-50);
            Canvas.SetTop(rectBouclierUtilise, -160);
            Canvas.SetLeft(rectBouclierUtilise,-160);
        }

        private void MoteurJeu(object sender, EventArgs e)
        {
            MettreAJourStatDebug();
            ActualisationStats();
            UtilisationBombe(Canvas.GetTop(rectJoueur),rectJoueur.Height, Canvas.GetLeft(rectJoueur), rectJoueur.Width);
            UtilisationSoin();
            UtilisationBouclier();
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(rectJoueur), Canvas.GetTop(rectJoueur),
            rectJoueur.Width, rectJoueur.Height);
            //animation vaisseau
            timerTir--;
            animeVaisseau++;
            if (animeVaisseau > animeVaisseauMax*2) { animeVaisseau = animeVaisseauMax; }
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau"+ animeVaisseau /6 + "canon" + this.CanonActuel +".png"));
            // déplacement à gauche et droite de vitessePlayer avec vérification des positions
            DeplacementsJoueur();
            GestionPaliers();

            foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "balleJoueur")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseBalle);
                    Rect balle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // suppression de la balle joueur en dehors du canvas
                    if (Canvas.GetLeft(x) > Canva.Width)
                    {
                        ElementsASupprimer.Add(x);
                    }
                    foreach (var y in Canva.Children.OfType<Rectangle>())
                    {
                        // si le rectangle est un ennemi
                        if (y is Rectangle && (string)y.Tag == "ennemi")
                        {
                            // création d’un rectangle correspondant à l’ennemi
                            Rect ennemi = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            // on vérifie la collision entre la balle du joueur et l'ennemi
                            if (balle.IntersectsWith(ennemi))
                            {
                                // on ajoute la balle a la liste à supprimer et on incremente le score
                                ElementsASupprimer.Add(x);
                                ReplacerElement(y);
                                score++;
                                minuterieBalleLimite--;
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "ennemi")
                {
                    if (Canvas.GetLeft(x) < -x.Width)
                    {
                        ReplacerElement(x);
                    }
                    if (Canvas.GetTop(x) < -x.Height -1)
                    {
                        Canvas.SetTop(x, Canva.Height);
                    }
                    else if (Canvas.GetTop(x) > Canva.Height + x.Height + 1)
                    {
                        Canvas.SetTop(x, -x.Height);
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseEnnemi);
                    Rect ennemi = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    Canvas.SetTop(x, Canvas.GetTop(x) + AttaquerJoueur(rectJoueur, x));
                    if (player.IntersectsWith(ennemi))
                    {
                        ReplacerElement(x);
                        if (!utiliseBouclier)
                            vieJoueur -= DEGAT_ENNEMI;
                    }
                    minuterieBalle -= 2;
                    if (minuterieBalle < 0)
                    {
                        CreationTirEnnemi((Canvas.GetTop(x) + x.Width / 2), Canvas.GetLeft(x));
                        // remise au max de la fréquence du tir ennemi. 
                        minuterieBalle = minuterieBalleLimite;
                    }
                }
                if (x is Rectangle && (string)x.Tag == "asteroid")
                {
                    if (Canvas.GetLeft(x) < -x.Width)
                    {
                        ReplacerElement(x);
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseAsteroid);
                    Rect asteroid = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player.IntersectsWith(asteroid))
                    {
                        ReplacerElement(x);
                        if (!utiliseBouclier)
                            vieJoueur -= DEGAT_ASTEROID;
                    }
                }
                if (x is Rectangle && (string)x.Tag == "balleEnnemi")
                {
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(Canva)-Canvas.GetLeft(x))
                    {
                        ElementsASupprimer.Add(x);
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseBalleEnnemi);
                    Rect balleEnnemi = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (balleEnnemi.IntersectsWith(player))
                    {
                        ElementsASupprimer.Add(x);
                        if (!utiliseBouclier)
                            vieJoueur -= DEGAT_TIR_ENNEMI;
                    }
                }
                if (x is Rectangle && (string)x.Tag == "etoile" && Canvas.GetLeft(x) > -x.Width)
                {
                    double vitesseEtoile = 0;
                    if ((int)x.Width == TAILLE_GRANDE_ETOILE)
                    {
                        vitesseEtoile = vitesseEtoile3;
                    }
                    if ((int)x.Width == TAILLE_MOY_ETOILE)
                    {
                        vitesseEtoile = vitesseEtoile2;
                    }
                    if ((int)x.Width == TAILLE_PETITE_ETOILE)
                    {
                        vitesseEtoile = vitesseEtoile1;
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseEtoile);
                }
                else if (x is Rectangle && (string)x.Tag == "etoile")
                {
                    Canvas.SetLeft(x, Canva.Width);
                    Canvas.SetTop(x, aleatoire.Next((int)Canva.Height - (int)x.Height));
                }
                if (x is Rectangle && (string)x.Tag == "pieuvre" && Canvas.GetLeft(x) > -x.Width)
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitessePieuvre);
                }
                else if (x is Rectangle && (string)x.Tag == "pieuvre")
                {
                    Canvas.SetLeft(x, Canva.Width * 2);
                    Canvas.SetTop(x, aleatoire.Next((int)Canva.Height - (int)x.Height));
                }
            }
            if (vieJoueur <= 0)
            {
                FinPartie();
            }
            foreach (Rectangle x in ElementsAAjouter)
            {
                Canva.Children.Add(x);
            }
            ElementsAAjouter.Clear();
            foreach (Rectangle x in ElementsASupprimer)
            {
                Canva.Children.Remove(x);
            }
            ElementsASupprimer.Clear();
        }
        private void CreationEnnemis(int limite)
        {
            for (int i = 0; i < limite; i++)
            {
                Rectangle ennemi = new Rectangle
                {
                    Width = TAILLE_ENNEMI * RATIO_TAILLE_ENNEMI,
                    Height = TAILLE_ENNEMI,
                    Fill = apparenceEnnemi,
                    Tag = "ennemi"
                };
                Canvas.SetLeft(ennemi, aleatoire.Next((int)Canva.Width, (int)Canva.Width + (int)ennemi.Width));
                Canvas.SetTop(ennemi, aleatoire.Next((int)ennemi.Height, (int)Canva.Height) - (int)ennemi.Height);
                Canvas.SetZIndex(ennemi, zIndexEnnemi);
                Canva.Children.Add(ennemi);
                nbEnnemi++;
            }
        }
        private void CreationAsteroids(int limite, int tailleMinAsteroid, int tailleMaxAsteroid)
        {
            for (int i = 0; i < limite; i++)
            {
                int tailleAsteroid = aleatoire.Next(tailleMinAsteroid, tailleMaxAsteroid);
                Rectangle asteroid = new Rectangle
                {
                    Width = tailleAsteroid * RATIO_TAILLE_ASTEROID,
                    Height = tailleAsteroid,
                    Fill = apparenceAsteroid,
                    Tag = "asteroid"
                };
                Canvas.SetLeft(asteroid, aleatoire.Next((int)Canva.Width, (int)Canva.Width + (int)asteroid.Width));
                Canvas.SetTop(asteroid, aleatoire.Next((int)asteroid.Height, (int)Canva.Height) - (int)asteroid.Height);
                Canvas.SetZIndex(asteroid, zIndexAsteroids);
                Canva.Children.Add(asteroid);
                nbAsteroid++;
            }
        }
        private void CreationEtoiles(int limite, int taille, int profondeur)
        {
            for (int i = 0; i < limite; i++)
            {
                int numeroEtoile = aleatoire.Next(1, nombrePetitesEtoiles + 1);
                ImageBrush etoileApparence = new ImageBrush();
                etoileApparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/PetitesEtoiles/Etoile" + numeroEtoile + ".png"));

                Rectangle nouvelleEtoile = new Rectangle
                {
                    Height = taille,
                    Width = taille,
                    Fill = etoileApparence,
                    Tag = "etoile"
                };
                Canvas.SetTop(nouvelleEtoile, aleatoire.Next((int)Canva.Height - (int)nouvelleEtoile.Height));
                Canvas.SetLeft(nouvelleEtoile, aleatoire.Next((int)Canva.Width - (int)nouvelleEtoile.Width));
                Canvas.SetZIndex(nouvelleEtoile, profondeur);
                Canva.Children.Add(nouvelleEtoile);
            }

        }
        private void CreationPieuvre(int taille)
        {
            ImageBrush pieuvreApparence = new ImageBrush();
            pieuvreApparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetFond/pieuvre.png"));

            Rectangle nouvellePieuvre = new Rectangle
            {
                Height = taille,
                Width = taille,
                Fill = pieuvreApparence,
                Tag = "pieuvre"
            };
            Canvas.SetTop(nouvellePieuvre, aleatoire.Next((int)Canva.Height - (int)nouvellePieuvre.Height));
            Canvas.SetLeft(nouvellePieuvre, Canva.Width*2);
            Canvas.SetZIndex(nouvellePieuvre, zIndexPieuvre);
            Canva.Children.Add(nouvellePieuvre);
        }
        private void ToucheEnfoncee(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                #if DEBUG
                    Console.WriteLine("touche gauche appuyer !");
                #endif  
                vaAGauche = true;
            }
            if (e.Key == Key.Right)
            {
                #if DEBUG
                    Console.WriteLine("touche droite appuyer !");
                #endif
                vaADroite = true;
            }
            if (e.Key == Key.Up)
            {
                #if DEBUG
                    Console.WriteLine("touche haut appuyer !");
                #endif
                vaEnHaut = true;
            }
            if (e.Key == Key.Down)
            {
                #if DEBUG
                    Console.WriteLine("touche bas appuyer !");
                #endif
                vaEnBas = true;
            }
            if (e.Key == Key.Space && timerTir < 0 && balleParTir <= LimiteBalleParTir)
            {
                #if DEBUG
                    Console.WriteLine("touche de tir appuyer !");
                #endif
                timerTir = TempsRechargement;
                // on vide la liste des items
                ElementsASupprimer.Clear();
                balletirer = 0;
                foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
                {
                    if (x is Rectangle && (string)x.Tag == "balleJoueur")
                    {
                        // increment le nombre de balle tirer afficher sur l'ecran
                        balletirer += 1;
                    }
                }
                if (balletirer <= LIMITE_BALLE_JOUEUR)
                {
                    #if DEBUG
                        Console.WriteLine(balletirer);
                    #endif
                    // création un nouveau tir
                    Rectangle nouvelleBalle = new Rectangle
                    {
                        Tag = "balleJoueur"
                    , //permet de tagger les rectangles
                        Height = HauteurBalleJoueur,
                        Width = LargeurBalleJoueur,
                        Fill = Brushes.White,
                        Stroke = Brushes.Red
                    };
                    Canvas.SetZIndex(nouvelleBalle, 4);
                    // on place le tir à l’endroit du joueur
                    Canvas.SetTop(nouvelleBalle, Canvas.GetTop(rectJoueur) + rectJoueur.Height - rectJoueur.Height / 4);
                    Canvas.SetLeft(nouvelleBalle, Canvas.GetLeft(rectJoueur) + rectJoueur.Width / 2);
                    // on place le tir dans le canvas
                    Canva.Children.Add(nouvelleBalle);
                    balleParTir++;
                }
            }
            if (e.Key == Key.Escape)
            {
                System.Windows.Application.Current.Shutdown();
            }
            if (e.Key == Key.Y && afficheDevbug)
            {
                //MAX_VIE correpond au nombre de coeur
                if (vieJoueur < this.MAX_VIE*2)
                {
                    vieJoueur++;
                }
            }
            if (e.Key == Key.F3)
            {
                if (!afficheDevbug)
                {
                    changeOpaciter(1);
                    afficheDevbug = true;
                }
                else
                {
                    changeOpaciter(0);
                    afficheDevbug= false;
                }
            }
            if (e.Key == Key.C)
            {
                #if DEBUG
                Console.WriteLine("touche C appuyée !");
                #endif
                utiliseBombe = true;
            }
            if (e.Key == Key.V)
            {
                #if DEBUG
                Console.WriteLine("touche V appuyée !");
                #endif
                utiliseSoin = true;
            }
            if (e.Key == Key.B)
            {
                #if DEBUG
                Console.WriteLine("touche B appuyée !");
                #endif
                utiliseBouclier = true;
            }
            if (e.Key == Key.T)
            {
                this.PointCredit += 10000;
            }
        }
        private void changeOpaciter(int opaciter)
        {
            foreach (var y in Canva.Children.OfType<TextBlock>())
            {
                if ((string)y.Tag == "Devbug")
                {
                    y.Opacity = opaciter;
                }
            }
            foreach (var y in Canva.Children.OfType<Label>())
            {
                if ((string)y.Tag == "Devbug")
                {
                    y.Opacity = opaciter;
                }
            }
        }
        private void ToucheRelachee(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction du relâchement de la touche
            if (e.Key == Key.Left)
            {
                vaAGauche = false;
            }
            if (e.Key == Key.Right)
            {
                vaADroite = false;
            }
            if (e.Key == Key.Up)
            {
                vaEnHaut = false;
            }
            if (e.Key == Key.Down)
            {
                vaEnBas = false;
            }
            if (e.Key == Key.Space)
            {
                tirer = false;
                balleParTir = 0;
            }
            
            
        }
        private void CreationTirEnnemi(double y, double x)
        {
            // création des tirs ennemis tirant vers l'objet joueur
            // x et y position du tir
            Rectangle NouvelleBalleEnnemi = new Rectangle
            {
                Height = HAUTEUR_BALLE_ENNEMI,
                Width = LONGUEUR_BALLE_ENNEMI,
                Fill = Brushes.Yellow,
                Tag = "balleEnnemi"
            };
            Canvas.SetZIndex(NouvelleBalleEnnemi, zIndexBalleEnnemi);
            Canvas.SetTop(NouvelleBalleEnnemi, y);
            Canvas.SetLeft(NouvelleBalleEnnemi, x);
            ElementsAAjouter.Add(NouvelleBalleEnnemi);
        }
        private void MettreAJourStatDebug()
        {
            TxtNbAsteroid.Text = nbAsteroid.ToString();
            TxtNbEnnemi.Text = nbEnnemi.ToString();
            TxtVitAsteroid.Text = vitesseAsteroid.ToString();
            TxtVitBalle.Text = vitesseBalle.ToString();
            TxtVitBalleEnnemi.Text = vitesseBalleEnnemi.ToString();
            TxtVitEnnemi.Text = vitesseEnnemi.ToString();
            TxtVitJoueur.Text = vitesseJoueur.ToString();
        }

        private void ReplacerElement(Rectangle element)
        {
            Canvas.SetLeft(element, Canva.Width);
            Canvas.SetTop(element, aleatoire.Next((int)element.Height, (int)Canva.Height - (int)element.Height));
        }

        private void ControlFenetre()
        {
            Musique musique = new Musique();
            musique.Fenetre = this;



            Canva.Height = SystemParameters.PrimaryScreenHeight;
            Canva.Width = SystemParameters.PrimaryScreenWidth;
            Canva.Focus();
            musique.LanceMusiqueMenu();
            while (!quitter && !jouer)
            {

                switch (FenetreAOuvrir)
                {
                    case "menuPrincipal":
                        {
                            MenuPrincipal menuPrincipal = new MenuPrincipal();
                            menuPrincipal.Fenetre = this;
                            menuPrincipal.ShowDialog();
                            break;
                        }
                    case "garage":
                        {
                            Garage garage = new Garage(this);
                            garage.ShowDialog();
                            break;
                        }
                    case "credits":
                        {
                            Credits credits = new Credits();
                            credits.Fenetre = this;
                            credits.ShowDialog();
                            break;
                        }
                    case "quitter":
                        {
                            quitter = true;
                            break;
                        }
                    case "reglages":
                        {
                            Reglages reglages = new Reglages();
                            reglages.Fenetre = this;
                            reglages.ShowDialog();
                            break;
                        }
                    case "jouer":
                        {
                            vieJoueur = this.vieJoueurDebutPartie * 2;
                            jouer = true;
                            score = 0;
                            palierActuel = 0;
                            nbEnnemi = 0;
                            nbAsteroid = 0;
                            barreVie = new Rectangle[10] { rectCoeur1, rectCoeur2, rectCoeur3, rectCoeur4, rectCoeur5, rectCoeur6, rectCoeur7, rectCoeur8, rectCoeur9, rectCoeur10 };
                            minuterie.Interval = TimeSpan.FromMilliseconds(16);
                            minuterie.Tick += MoteurJeu;
                            minuterie.Start();
                            Canvas.SetTop(rectJoueur, Canva.Height / 2);
                            Canvas.SetLeft(rectJoueur, rectJoueur.Width);
                            initialisationJeux();
                            musiqueMenu.Close();
                            musique.LanceMusiqueGameplay();
                            break;
                        }
                }
            }
            if (quitter)
                System.Windows.Application.Current.Shutdown();
        }
        private void ActualisationStats()
        {
            for (int i = 0; i < this.MAX_VIE; i++)
            {
                barreVie[i].Fill = Apparences.imgCoeurVide;
            }
            if (vieJoueur%2 == 1)
            {
                barreVie[(vieJoueur / 2)].Fill = imgDemiCoeur;
            }
            for (int i = 0; i < vieJoueur/2; i++)
            {
                barreVie[i].Fill = imgCoeur;
            }
            txtNombreBombe.Text = Bombes.ToString();
            txtNombreSoin.Text = Soins.ToString();
            txtNombreBouclier.Text = Boucliers.ToString();
            txtScore.Text = score.ToString() + this.UNITE_PRIX;
            txtPalier.Text = palierActuel.ToString();

        }
        private void DeplacementsJoueur()
        {
            if (vaAGauche && Canvas.GetLeft(rectJoueur) > -50)
            {
                Canvas.SetLeft(rectJoueur, Canvas.GetLeft(rectJoueur) - vitesseJoueur);
            }
            else if (vaADroite && Canvas.GetLeft(rectJoueur) + rectJoueur.Width < Canva.Width)
            {
                Canvas.SetLeft(rectJoueur, Canvas.GetLeft(rectJoueur) + vitesseJoueur);
            }
            if (vaEnHaut && Canvas.GetTop(rectJoueur) > 0)
            {
                Canvas.SetTop(rectJoueur, Canvas.GetTop(rectJoueur) - vitesseJoueur);
            }
            else if (vaEnBas && Canvas.GetTop(rectJoueur) + rectJoueur.Height < Canva.Height)
            {
                Canvas.SetTop(rectJoueur, Canvas.GetTop(rectJoueur) + vitesseJoueur);
            }
        }

        private static double AttaquerJoueur(Rectangle joueur, Rectangle ennemi)
        {
            double direction = Canvas.GetTop(joueur) - Canvas.GetTop(ennemi);
            double stopAttaque = Canvas.GetLeft(joueur) - Canvas.GetLeft(ennemi);
            if (stopAttaque >= 0)
            {
                return 0;
            }

            if (direction < -5)
            {
                return -VITESSE_VERTICALE_ENNEMI;
            }
            else if (direction > 5)
            {
                return VITESSE_VERTICALE_ENNEMI;
            }
            else
            {
                return 0;
            }
            
        }
        private void GestionPaliers()
        {
            if (score % 10 == 0 && passpalier)
            {
                if (nbEnnemi < NB_LIMITE_ENNEMI)
                {
                    CreationEnnemis(1);

                }
                if (nbAsteroid < NB_LIMITE_ASTEROIDE)
                {
                    CreationAsteroids(1, TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
                }
                palierActuel = score / 10;
                vitesseEnnemi += ACCELERATION_VITESSE_ENNEMI;
                vitesseBalleEnnemi += ACCELERATION_VITESSE_BALLE_ENNEMI;
                vitesseAsteroid += ACCELERATION_VITESSE_ASTEROID;

                passpalier = false;
            }
            if (score % 10 != 0 && !passpalier)
            {
                passpalier = true;
            }

        }
        private void FinPartie()
        {
            foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
            {
                if (x.Name != "rectJoueur" && x.Tag != null)
                {
                    ElementsASupprimer.Add(x);
                }
            }
            musiqueGameplay.Close();
            this.fenetreAOuvrir = "garage";
            jouer = false;
            passpalier = false;
            minuterie.Tick -= MoteurJeu;
            this.PointCredit += score;
            vitesseAsteroid = VIT_DEPART_ASTEROID;
            vitesseBalleEnnemi = VIT_DEPART_BALLE_ENNEMI;
            vitesseEnnemi = VIT_DEPART_ENNEMI;
            vaADroite = false;
            vaAGauche = false;
            vaEnBas = false;
            vaEnHaut = false;
            ControlFenetre();
        }
        private void UtilisationBombe(double setTop, double hauteur, double setLeft, double largeur)
        {
            if (utiliseBombe && Bombes > 0)
            {
                vitesseExpensionExplosion = 10;
                rectBombeLancee.Fill = Apparences.bombe;
                if (!Canva.Children.Contains(rectBombeLancee) && !Canva.Children.Contains(rectExplosionBombe))
                {
                    Canva.Children.Add(rectBombeLancee);
                    Canva.Children.Add(rectExplosionBombe);
                }
                Canvas.SetTop(rectBombeLancee, setTop + (hauteur/2));
                Canvas.SetLeft(rectBombeLancee, setLeft + (largeur/2));
                Canvas.SetTop(rectExplosionBombe, Canva.Height);
                ordonneeExplosion = Canva.Height;
                Canvas.SetLeft(rectExplosionBombe, Canvas.GetLeft(rectBombeLancee));
                abscisseExplosion = Canvas.GetLeft(rectBombeLancee);
                Canvas.SetZIndex(rectBombeLancee, zIndexBombeLancee);
                vitesseExpensionExplosion = 10;
                vitesseBombeLancee = 1;
                minuterieBombe.Interval = TimeSpan.FromMilliseconds(16);
                minuterieBombe.Tick += AnimationBombe; 
                minuterieBombe.Start();
                foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ennemi" || (string)x.Tag == "asteroid")
                    {
                        score++;
                        ElementsASupprimer.Add(x);
                    }
                }
                int tmpResetObstacle = nbEnnemi;
                nbEnnemi = 0;
                CreationEnnemis(tmpResetObstacle);
                tmpResetObstacle = nbAsteroid;
                nbAsteroid = 0;
                CreationAsteroids(tmpResetObstacle, TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
                utiliseBombe = false;
                Bombes = Bombes - 1;
            }
        }
        private void AnimationBombe(object sender, EventArgs e)
        {
            if (Canvas.GetTop(rectBombeLancee) + rectBombeLancee.Height < Canva.Height)
            {
            Canvas.SetTop(rectBombeLancee, Canvas.GetTop(rectBombeLancee) + vitesseBombeLancee);
                comptageAcceleration += 1;
                if (comptageAcceleration >= VITESSE_ACCELERATION_BOMBE)
                {
                    vitesseBombeLancee = vitesseBombeLancee * 2;
                    comptageAcceleration = 1;
                }
            }
            else
            {
                if(rectExplosionBombe.Height < 4000)
                {
                    Canvas.SetTop(rectExplosionBombe, ordonneeExplosion);
                    Canvas.SetLeft(rectExplosionBombe, abscisseExplosion);

                    ordonneeExplosion = ordonneeExplosion - (vitesseExpensionExplosion/2);
                    abscisseExplosion = abscisseExplosion - (vitesseExpensionExplosion / 2);
                    rectExplosionBombe.Opacity = 1 - rectExplosionBombe.Height/4000;
                    rectExplosionBombe.Height = rectExplosionBombe.Height + vitesseExpensionExplosion;
                    rectExplosionBombe.Width = rectExplosionBombe.Width + vitesseExpensionExplosion;
                    comptageAcceleration += 1;

                    if (comptageAcceleration >= VITESSE_ACCELERATION_BOMBE)
                    {
                        vitesseExpensionExplosion = vitesseExpensionExplosion * 2;
                        comptageAcceleration = 1;
                    }
                }
                else
                {
                    Canva.Children.Remove(rectBombeLancee);
                    Canva.Children.Remove(rectExplosionBombe);
                    rectExplosionBombe.Height = TAILLE_BOMBE;
                    rectExplosionBombe.Width = TAILLE_BOMBE;
                    rectBombeLancee.Height = TAILLE_BOMBE;
                    rectBombeLancee.Width = TAILLE_BOMBE;
                    Canvas.SetTop(rectExplosionBombe, -50);
                    Canvas.SetLeft(rectExplosionBombe, -50);
                    Canvas.SetTop(rectBombeLancee, -50);
                    Canvas.SetLeft(rectBombeLancee, -50);
                    minuterieBombe.Tick -= AnimationBombe;
                    minuterieBombe.Stop();
                    utiliseBombe = false;
                }
            }
        }
        private void UtilisationSoin()
        {
            if (utiliseSoin && this.Soins > 0) 
            {
                this.Soins -= 1;
                vieJoueur = this.VieJoueurDebutPartie * 2;
                utiliseSoin = false;
            }  
            
        }
        private void UtilisationBouclier()
        {
            if(utiliseBouclier && Boucliers > 0)
            {
                if (!Canva.Children.Contains(rectBouclierUtilise) )
                {
                    Canva.Children.Add(rectBouclierUtilise);
                }
                if (compteurDixSecondes < 300)
                {
                    compteurDixSecondes += 1;
                    Canvas.SetLeft(rectBouclierUtilise, Canvas.GetLeft(rectJoueur) - (rectBouclierUtilise.Width - rectJoueur.Width) / 2);
                    Canvas.SetTop(rectBouclierUtilise, Canvas.GetTop(rectJoueur) - (rectBouclierUtilise.Height - rectJoueur.Height) / 2);
                    Canvas.SetZIndex(rectBouclierUtilise, 6);

                }
                else
                {
                    compteurDixSecondes = 0;
                    utiliseBouclier = false;
                    Boucliers -= 1;
                    Canvas.SetTop(rectBouclierUtilise, -160);
                    Canvas.SetLeft(rectBouclierUtilise, -160);
                }
            }
            else
            {
                utiliseBouclier = false;
            }
        }
    }
}
