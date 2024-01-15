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
        public readonly int MAX_VIE = 10;
        public readonly int MIN_VIE = 3;
        public int vieJoueurDebutPartie = 3;
        private const int TAILLE_PETITE_ETOILE = 15, TAILLE_MOY_ETOILE = 30, TAILLE_GRANDE_ETOILE = 50;
        private const int NB_PETITE_ETOILE = 20, NB_MOY_ETOILE = 15, NB_GRANDE_ETOILE = 10;
        
        // création variable minuterie
        private DispatcherTimer minuterie = new DispatcherTimer();
        // booléens pour gauche droite haut bas
        private bool vaADroite, vaAGauche, vaEnHaut, vaEnBas = false;
        // booléens pour detecter le tir du joueur
        private bool tirer = false;
        // liste des éléments rectangles
        private List<Rectangle> ElementsASupprimer = new List<Rectangle>();
        // entier nous permettant de charger les images des ennemis
        private int ImagesEnnemis = 0;
        // entier nous permettant de charger les images des etoiles
        private int ImagesEtoiles = 0;
        private int vitesseEtoile1 = 1;
        private int vitesseEtoile2 = 2;
        private int vitesseEtoile3 = 3;

        // nombre de petites etoiles qui existent
        private int nombrePetitesEtoiles = 5;
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush apparenceJoueur = new ImageBrush();
        // vitesse du joueur
        private int vitesseJoueur = 10;
        private int vitesseBalle = 20;
        //limite le nombre de balle tirer par le joueur
        private const int LIMITE_BALLE_JOUEUR = 30;
        private int balletirer = 0;
        Random aleatoire = new Random();


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
        private int vieJoueur;

        public int VieJoueur
        {
            get { return vieJoueur; }
            set {
                if (value < MIN_VIE || value > MAX_VIE)
                    throw new ArgumentOutOfRangeException("la vie doit être entre 3 et 10");
                vieJoueur = value; }
        }

        public MainWindow()
        {
            #if DEBUG
            Console.WriteLine("Debug version");
#endif
            vieJoueur = MIN_VIE;
            InitializeComponent();
            bool quitter = false;
            bool jouer = false;
            Canva.Height = SystemParameters.PrimaryScreenHeight;
            Canva.Width = SystemParameters.PrimaryScreenWidth;
            Canva.Focus();

            MediaPlayer playMedia = new MediaPlayer(); // instancie le Mediaplayer pour ajouter de la musique
            var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueAccueil.mp3"); // chemin d'acces pour la musique
            playMedia.Open(uri);
            playMedia.Play(); // joue le fichier de la musique

            this.FenetreAOuvrir = "menuPrincipal";
            // chargement de l’image du joueur 
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Vaisseaux/Vaisseau1canon1.png"));

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
                            Garage garage = new Garage();
                            garage.Fenetre = this;
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
                            jouer = true;
                            minuterie.Interval = TimeSpan.FromMilliseconds(16);
                            minuterie.Tick += MoteurJeu;
                            minuterie.Start();
                            
                            // assignement de skin du joueur au rectangle associé
                            rectJoueur.Fill = apparenceJoueur;
                            break;
                        }
                }
            }
            initialisationAstres();

            if ( quitter )
                System.Windows.Application.Current.Shutdown();
            
        }
        public void initialisationAstres()
        {
            CreationEtoiles(NB_PETITE_ETOILE, TAILLE_PETITE_ETOILE, 1); 
            CreationEtoiles(NB_MOY_ETOILE, TAILLE_MOY_ETOILE, 2);
            CreationEtoiles(NB_GRANDE_ETOILE, TAILLE_GRANDE_ETOILE, 3);
        }
        private void Media_Ended(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void MoteurJeu(object sender, EventArgs e)
        {

            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(rectJoueur), Canvas.GetTop(rectJoueur),
            rectJoueur.Width, rectJoueur.Height);
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
                }
                if (x is Rectangle && (string)x.Tag == "etoile" && Canvas.GetLeft(x) > 0)
                {
                    int vitesseEtoile = 0;
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
            }
            foreach (Rectangle x in ElementsASupprimer)
            {
                Canva.Children.Remove(x);
            }

        }

        private void CreationEnemie(int limite)
        {
            for (int i = 0; i < limite; i++)
            {
                Console.WriteLine("tomme");
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
            pieuvreApparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjFond/pieuvre.png"));

            Rectangle nouvellePieuvre = new Rectangle
            {
                Height = taille,
                Width = taille,
                Fill = pieuvreApparence,
                Tag = "pieuvre"
            };
            Canvas.SetTop(nouvellePieuvre, aleatoire.Next((int)Canva.Height - (int)nouvellePieuvre.Height));
            Canvas.SetLeft(nouvellePieuvre, aleatoire.Next((int)Canva.Width - (int)nouvellePieuvre.Width));
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

            if (e.Key == Key.Space)
            {
                #if DEBUG
                    Console.WriteLine("touche de tir appuyer !");
                #endif
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
            }
        }

    }
}
