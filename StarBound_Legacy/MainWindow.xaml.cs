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
            set { vieJoueurDebutPartie = value; }
        }
        private int pointCredit = 0;

        public int PointCredit
        {
            get { return pointCredit; }
            set { pointCredit = value; }
        }
        private int canonActuel;

        public int CanonActuel
        {
            get { return canonActuel; }
            set { 
                if(value > NOMBRE_TYPES_CANON || value < NOMBRE_TYPES_CANON -(NOMBRE_TYPES_CANON-1))
                    throw new ArgumentOutOfRangeException("le type de canon doit être entre1 et 4");
                canonActuel = value; }
        }
        public readonly int NOMBRE_TYPES_CANON = 4;


        // creation des lecteurs de la musique
        public MediaPlayer musiqueMenu = new MediaPlayer();
        public MediaPlayer musiqueGameplay = new MediaPlayer();

        Random aleatoire = new Random();

        public readonly int MAX_VIE = 10;
        public readonly int MIN_VIE = 3;
        // liste des éléments rectangles
        private List<Rectangle> ElementsASupprimer = new List<Rectangle>();
        private List<Rectangle> ElementsAAjouter = new List<Rectangle>();
        



        // LIGNES POUR LE GAMEPLAY


        // FONCTIONNEMENT JEU

        // création variable minuterie
        private DispatcherTimer minuterie = new DispatcherTimer();
        // booléens pour gauche droite haut bas
        private bool vaADroite, vaAGauche, vaEnHaut, vaEnBas = false;
        // booléens pour detecter le tir du joueur
        private bool afficheDevbug = false;

        private const int NB_ENNEMI_DEPART = 2, NB_LIMITE_ENNEMI = 15, NB_ASTEROIDE_DEPART = 3, NB_LIMITE_ASTEROIDE = 5;
        //variable du score du joueur
        public int score = 0;
        public bool passpalier = false;
        public int palierActuel = 0;
        private ImageBrush imgCoeur = new ImageBrush();
        private ImageBrush imgDemiCoeur = new ImageBrush();
        private ImageBrush imgCoeurVide = new ImageBrush();
        Rectangle[] barreVie;


        bool quitter = false;
        bool jouer = false;

        // DECOR

        private const int TAILLE_PETITE_ETOILE = 15, TAILLE_MOY_ETOILE = 30, TAILLE_GRANDE_ETOILE = 50, TAILLE_PIEUVRE = 100;
        private const int NB_PETITE_ETOILE = 10, NB_MOY_ETOILE = 2, NB_GRANDE_ETOILE = 1;
        private int nb_ennemi = 0, nb_asteroide = 0;

        // entier nous permettant de charger les images des etoiles
        private int ImagesEtoiles = 0;
        private double vitesseEtoile1 = 1;
        private double vitesseEtoile2 = 2;
        private double vitesseEtoile3 = 3;
        private double vitessePieuvre = 2;

        // nombre de petites etoiles qui existent
        private int nombrePetitesEtoiles = 11;

        
        // JOUEUR

        private bool tirer = false;
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush apparenceJoueur = new ImageBrush();
        // vitesse du joueur
        private int vitesseJoueur = 10;
        private int vieJoueur;
        private int vitesseBalle = 20;

        //pour changer le temps de rechargement (cooldown)
        private int tempsRechargement = 600;

        public int TempsRechargement
        {
            get { return tempsRechargement; }
            set { tempsRechargement = value; }
        }

        //limite le nombre de balle tirer par le joueur
        private const int LIMITE_BALLE_JOUEUR = 50;

        //pour augmenter le nombre de balle par salve
        private int limiteBalleParTir = 5;
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
        private const int TAILLE_MIN_ASTEROID = 25, TAILLE_MAX_ASTEROID = 250;
        private ImageBrush apparenceEnnemi = new ImageBrush();
        private ImageBrush apparenceAsteroid = new ImageBrush();
        private const int TAILLE_ENNEMI = 50;
        private const int LONGUEUR_BALLE_ENNEMI = 40;
        private const int HAUTEUR_BALLE_ENNEMI = 15;
        private const double ACCELERATION_VITESSE_ENNEMI = 0.2;
        private const double ACCELERATION_VITESSE_BALLE_ENNEMI = 0.3;
        private const double ACCELERATION_VITESSE_ASTEROID = 0.2;
        private const double RATIO_TAILLE_ASTEROID = 2.5;
        private const double RATIO_TAILLE_ENNEMI = 1.5;
        private const double VITESSE_VERTICALE_ENNEMI = 2;


        // timer tir et animation vaisseau
        private int timerTir = 0;
        private int timerTirMax = 2;
        private int animeVaisseau = 6;
        private int animeVaisseauMax = 6;
        private double minuterieBalle = 8;
        private double minuterieBalleLimite = 800;
        

        public MainWindow()
        {
            #if DEBUG
            Console.WriteLine("Debug version");
            #endif
            
            InitializeComponent();
            this.CanonActuel = 1;
            this.VieJoueurDebutPartie = MIN_VIE;
            this.FenetreAOuvrir = "menuPrincipal";
            // chargement de l’image du joueur 
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon1.png"));
            imgCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/Coeur.png"));
            imgDemiCoeur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/DemiCoeur.png"));
            apparenceEnnemi.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Ennemis/Ennemi.png"));
            apparenceAsteroid.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Asteroids/Asteroid.png"));
            imgCoeurVide.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Coeurs/CoeurVide.png"));
            rectJoueur.Fill = apparenceJoueur;
            ControlFenetre();
            
        }
        public void initialisationJeux()
        {
            CreationEtoiles(NB_PETITE_ETOILE, TAILLE_PETITE_ETOILE, 1); 
            CreationEtoiles(NB_MOY_ETOILE, TAILLE_MOY_ETOILE, 2);
            CreationEtoiles(NB_GRANDE_ETOILE, TAILLE_GRANDE_ETOILE, 3);
            CreationPieuvre(TAILLE_PIEUVRE);
            CreationEnnemis(NB_ENNEMI_DEPART);
            CreationAsteroids(NB_ASTEROIDE_DEPART, TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
        }
        
        private void MoteurJeu(object sender, EventArgs e)
        {
            MettreAJourStat();
            ActualisationBarreVie();
            txtScore.Text = score.ToString();
            txtPalier.Text = palierActuel.ToString();
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(rectJoueur), Canvas.GetTop(rectJoueur),
            rectJoueur.Width, rectJoueur.Height);

            //animation vaisseau
            timerTir--;
            animeVaisseau++;
            if (animeVaisseau > animeVaisseauMax*2) { animeVaisseau = animeVaisseauMax; }
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau"+ animeVaisseau /6 + "canon" + this.CanonActuel +".png"));
            
            


            // déplacement à gauche et droite de vitessePlayer avec vérification des positions
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

            if (score % 10 == 0 && passpalier)
            {
                if (nb_ennemi < NB_LIMITE_ENNEMI)
                {
                    CreationEnnemis(1);
                    
                }
                if (nb_asteroide < NB_LIMITE_ASTEROIDE)
                {
                    CreationAsteroids(1, TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
                }
                palierActuel++;
                vitesseEnnemi += ACCELERATION_VITESSE_ENNEMI;
                vitesseBalleEnnemi += ACCELERATION_VITESSE_BALLE_ENNEMI;
                vitesseAsteroid += ACCELERATION_VITESSE_ASTEROID;
                passpalier = false;
            }
            if (score % 10 != 0 && !passpalier)
            {
                passpalier = true;
            }
            foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "balleJoueur")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseBalle);
                    Rect balle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
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
                            // on vérifie la collision
                            // appel à la méthode IntersectsWith pour détecter la collision
                            if (balle.IntersectsWith(ennemi))
                            {
                                // on ajoute la balle a la liste à supprimer et on incremente le score
                                ElementsASupprimer.Add(x);
                                ReplacerElement(y);
                                score++;
                                if (minuterieBalleLimite >= this.TempsRechargement)
                                {
                                    minuterieBalleLimite--;
                                }
                            }
                        }
                        if (y is Rectangle && (string)y.Tag == "asteroid")
                        {
                            Rect asteroid = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (balle.IntersectsWith(asteroid))
                            {
                                int tailleAsteroid = aleatoire.Next(TAILLE_MIN_ASTEROID, TAILLE_MAX_ASTEROID);
                                y.Height = tailleAsteroid;
                                y.Width = tailleAsteroid * RATIO_TAILLE_ASTEROID;
                                ElementsASupprimer.Add(x);
                                ReplacerElement(y);
                                
                                score++;
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
                    Canvas.SetTop(x, Canvas.GetTop(x) + AttacquerJoueur(rectJoueur, x));
                    if (player.IntersectsWith(ennemi))
                    {
                        ReplacerElement(x);
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
                foreach (Rectangle x in Canva.Children.OfType<Rectangle>())
                {
                    if (x.Name != "rectJoueur" && x.Tag != null)
                    {
                        ElementsASupprimer.Add(x);
                    }
                }
                this.fenetreAOuvrir = "garage";
                jouer = false;
                minuterie.Tick -= MoteurJeu;
                this.PointCredit += score;
                ControlFenetre();
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
                Canvas.SetZIndex(ennemi, 6);
                Canva.Children.Add(ennemi);
                nb_ennemi++;
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
                Canvas.SetZIndex(asteroid, 4);
                Canva.Children.Add(asteroid);
                nb_asteroide++;
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
            Canvas.SetZIndex(nouvellePieuvre, 3);
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

            if (e.Key == Key.Space && timerTir < 1 && balleParTir <= limiteBalleParTir)
            {
                #if DEBUG
                    Console.WriteLine("touche de tir appuyer !");
#endif
                timerTir = timerTirMax;
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
                        Height = 5,
                        Width = 20,
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
            if (e.Key == Key.F3)
            {
                if (afficheDevbug)
                {
                    changeOpaciter(1);
                    afficheDevbug = false;
                }
                else
                {
                    changeOpaciter(0);
                    afficheDevbug= true;
                }
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
            Canvas.SetZIndex(NouvelleBalleEnnemi, 5);
            Canvas.SetTop(NouvelleBalleEnnemi, y);
            Canvas.SetLeft(NouvelleBalleEnnemi, x);
            ElementsAAjouter.Add(NouvelleBalleEnnemi);
        }


        private void MettreAJourStat()
        {
            TxtNbAsteroid.Text = nb_asteroide.ToString();
            TxtNbEnnemi.Text = nb_ennemi.ToString();
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
        private void ActualisationBarreVie()
        {
            for (int i = 0; i < 10 / 2; i++)
            {
                barreVie[i].Fill = imgCoeurVide;
            }
            if (vieJoueur%2 == 1)
            {
                barreVie[(vieJoueur / 2)].Fill = imgDemiCoeur;
            }
            for (int i = 0; i < vieJoueur/2; i++)
            {
                barreVie[i].Fill = imgCoeur;
            }
            
        }

        private static double AttacquerJoueur(Rectangle joueur, Rectangle ennemi)
        {
            double direction = Canvas.GetTop(joueur) - Canvas.GetTop(ennemi);
            double stopAttaque = Canvas.GetLeft(joueur) - Canvas.GetLeft(ennemi);
            if (stopAttaque >= 0)
            {
                return 0;
            }

            if (direction < 0)
            {
                return -VITESSE_VERTICALE_ENNEMI;
            }
            else if (direction > 0)
            {
                return VITESSE_VERTICALE_ENNEMI;
            }
            else
            {
                return 0;
            }
            
        }
    }
}
