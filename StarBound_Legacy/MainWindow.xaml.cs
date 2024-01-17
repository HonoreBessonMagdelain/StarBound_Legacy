using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
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
        // LIGNE POUR TOUT LE PROGRAMME


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

        private const int NB_ENNEMI_DEPART = 2, NB_LIMITE_ENNEMIE = 10, NB_ASTEROIDE_DEPART = 3, NB_LIMITE_ASTEROIDE = 10;
        //variable du score du joueur
        public int score = 0;
        public bool passpalier = false;
        public int palierActuel = 0;


        bool quitter = false;
        bool jouer = false;

        // DECOR

        private const int TAILLE_PETITE_ETOILE = 15, TAILLE_MOY_ETOILE = 30, TAILLE_GRANDE_ETOILE = 50, TAILLE_PIEUVRE = 100;
        private const int NB_PETITE_ETOILE = 10, NB_MOY_ETOILE = 2, NB_GRANDE_ETOILE = 1;
        private int nb_ennemi = NB_ENNEMI_DEPART, nb_asteroide = NB_ASTEROIDE_DEPART;

        // entier nous permettant de charger les images des etoiles
        private int ImagesEtoiles = 0;
        private double vitesseEtoile1 = 1;
        private double vitesseEtoile2 = 2;
        private double vitesseEtoile3 = 3;
        private double vitessePieuvre = 2;
        private double vitesseEnnemie = 1;

        // nombre de petites etoiles qui existent
        private int nombrePetitesEtoiles = 5;

        
        // JOUEUR

        private bool tirer = false;
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush apparenceJoueur = new ImageBrush();
        // vitesse du joueur
        private int vitesseJoueur = 10;
        private int vieJoueur;
        private int vitesseBalle = 20;
        //limite le nombre de balle tirer par le joueur
        private const int LIMITE_BALLE_JOUEUR = 50;
        private const int LIMITE_BALLE_PAR_TIR = 5;
        private int limiteBalleParTir = 0;
        private int balletirer = 0;


        // ENNEMIS

        // entier nous permettant de charger les images des ennemis
        private int ImagesEnnemis = 0;
        private double vitesseBalleEnnemie;

        private ImageBrush apparenceEnnemie = new ImageBrush();
        
        
        
        

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

            this.VieJoueurDebutPartie = MIN_VIE;
            this.FenetreAOuvrir = "menuPrincipal";
            // chargement de l’image du joueur 
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon1.png"));
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
        }
        
        private void MoteurJeu(object sender, EventArgs e)
        {
            
            vitesseBalleEnnemie = vitesseEnnemie * 3;
            txtScore.Text = score.ToString();
            txtPalier.Text = palierActuel.ToString();
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(rectJoueur), Canvas.GetTop(rectJoueur),
            rectJoueur.Width, rectJoueur.Height);

            //animation vaisseau
            timerTir--;
            animeVaisseau++;
            if (animeVaisseau > animeVaisseauMax*2) { animeVaisseau = animeVaisseauMax; }
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau"+ animeVaisseau /6 + "canon1.png"));
            apparenceEnnemie.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Logo/marque.png"));

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
                CreationEnnemis(1);
                palierActuel++;
                vitesseEnnemie += 0.1;
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
                        if (y is Rectangle && (string)y.Tag == "ennemie")
                        {
                            // création d’un rectangle correspondant à l’ennemi
                            Rect ennemie = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            // on vérifie la collision
                            // appel à la méthode IntersectsWith pour détecter la collision
                            if (balle.IntersectsWith(ennemie))
                            {
                                // on ajoute la balle a la liste à supprimer et on incremente le score
                                ElementsASupprimer.Add(x);
                                ReplacerElement(y);
                                score++;
                                if (minuterieBalleLimite >= 300)
                                {
                                    minuterieBalleLimite--;
                                }
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "ennemie")
                {
                    if (Canvas.GetLeft(x) < -100)
                    {
                        ReplacerElement(x);
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseEnnemie);
                    Rect ennemie = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player.IntersectsWith(ennemie))
                    {
                        ReplacerElement(x);
                        vieJoueur--;
                    }
                    minuterieBalle -= 2;
                    if (minuterieBalle < 0)
                    {
                        CreationTirEnnemi((Canvas.GetTop(x) + x.Width / 2), Canvas.GetLeft(x));
                        // remise au max de la fréquence du tir ennemi. 
                        minuterieBalle = minuterieBalleLimite;
                    }

                }
                if (x is Rectangle && (string)x.Tag == "balleEnnemie")
                {
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(Canva)-Canvas.GetLeft(x))
                    {
                        ElementsASupprimer.Add(x);
                    }
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseBalleEnnemie);
                    Rect balleEnnemie = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (balleEnnemie.IntersectsWith(player))
                    {
                        ElementsASupprimer.Add(x);
                        vieJoueur--;
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
                Rectangle ennemie = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = apparenceEnnemie,
                    Tag = "ennemie"
                };
                Canvas.SetLeft(ennemie, aleatoire.Next((int)Canva.Width, (int)Canva.Width + (int)ennemie.Width));
                Canvas.SetTop(ennemie, aleatoire.Next((int)ennemie.Height, (int)Canva.Height) - (int)ennemie.Height);
                Canvas.SetZIndex(ennemie, 4);
                Canva.Children.Add(ennemie);
            }
        }
        private void CreationEtoiles(int limite, int taille, int profondeur)
        {
            for (int i = 0; i < limite; i++)
            {
                int numeroEtoile = aleatoire.Next(1, nombrePetitesEtoiles + 1);
                ImageBrush etoileApparence = new ImageBrush();
                etoileApparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/PetitesEtoiles/petiteEtoile" + numeroEtoile + ".png"));

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

            if (e.Key == Key.Space && timerTir < 1 && limiteBalleParTir <= LIMITE_BALLE_PAR_TIR)
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
                    limiteBalleParTir++;
                }
            }
            if (e.Key == Key.Escape)
            {
                System.Windows.Application.Current.Shutdown();
            }
            if (e.Key == Key.F4)
            {
                if (afficheDevbug)
                {
                    txtPalier.Opacity = 1;
                    afficheDevbug = false;
                }
                else
                {
                    txtPalier.Opacity=0;
                    afficheDevbug= true;
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
                limiteBalleParTir = 0;
            }
        }
        private void CreationTirEnnemi(double y, double x)
        {
            // création des tirs ennemies tirant vers l'objet joueur
            // x et y position du tir
            Rectangle NouvelleBalleEnnemie = new Rectangle
            {
                Height = 15,
                Width = 40,
                Fill = Brushes.Yellow,
                Tag = "balleEnnemie"
            };
            Canvas.SetTop(NouvelleBalleEnnemie, y);
            Canvas.SetLeft(NouvelleBalleEnnemie, x);
            ElementsAAjouter.Add(NouvelleBalleEnnemie);
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



            txtPalier.Opacity = 0;
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
                            vieJoueur = this.vieJoueurDebutPartie;
                            jouer = true;
                            score = 0;
                            palierActuel = 0;
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
    }
}
