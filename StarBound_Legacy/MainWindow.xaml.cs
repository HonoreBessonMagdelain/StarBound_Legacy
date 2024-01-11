using System;
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
        public const int MAX_VIE = 10;
        public int vieJoueurDebutPartie = 3;
        public int vieJoueur = 3;
        private String fenetreAOuvrir;
        // création variable minuterie
        private DispatcherTimer minuterie = new DispatcherTimer();
        // booléens pour gauche droite haut bas
        private bool vaADroite, vaAGauche, vaEnHaut, vaEnBas = false;
        // liste des éléments rectangles
        private List<Rectangle> ElementsASupprimer = new List<Rectangle>();
        // entier nous permettant de charger les images des ennemis
        private int ImagesEnnemis = 0;
        // entier nous permettant de charger les images des etoiles
        private int ImagesEtoiles = 0;
        // nombre de petites etoiles qui existent
        private int nombrePetitesEtoiles = 5;
        Random aleatoire = new Random();




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
        public MainWindow()
        {
            #if DEBUG
            Console.WriteLine("Debug version");
            #endif

            InitializeComponent();
            bool quitter = false;
            bool jouer = false;
            MediaPlayer playMedia = new MediaPlayer(); // making a new instance of the media player
            var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueAccueil.mp3"); // browsing to the sound folder and then the WAV file location
            playMedia.Open(uri); // inserting the URI to the media player
            playMedia.Play(); // playing the media file from the media player class
            this.FenetreAOuvrir = "menuPrincipal";
            
            
            while(!quitter && !jouer)
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
                    //                   case "reglages":
                    //                       {
                    //                           Reglages reglages = new Reglages();
                    //                           reglages.Fenetre = this;
                    //                           reglages.ShowDialog();
                    //                           break;
                    //                       }
                    case "jouer":
                        {
                            jouer = true;
                            break;
                        }
                }
            }
            initialisationAstres();

            if ( quitter )
                System.Windows.Application.Current.Shutdown();
            minuterie.Tick += MoteurJeu;
            minuterie.Interval = TimeSpan.FromMilliseconds(16);
            minuterie.Start();
        }
        public void initialisationAstres()
        {
            CreationPetitesEtoiles(5000); 
        }
        private void Media_Ended(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void MoteurJeu(object sender, EventArgs e)
        {
            
        }
        private void ToucheEnfoncee(object sender, KeyEventArgs e)
        {

        }
        private void Toucherelachee(object sender, KeyEventArgs e)
        {
        
        }
        private void CreationPetitesEtoiles(int limite)
        {
            for(int i = 0; i<limite; i++)
            {
                int numeroEtoile = aleatoire.Next(1, nombrePetitesEtoiles + 1);
                //int numeroEtoile = aleatoire.Next(1, nombrePetitesEtoiles + 1);

                ImageBrush etoileApparence = new ImageBrush();
                etoileApparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/petiteEtoile" + numeroEtoile + ".png"));

                Rectangle nouvelleEtoile = new Rectangle
                {
                    Height = aleatoire.Next(20, 45),
                    Width = aleatoire.Next(20, 45),
                    Fill = etoileApparence
                };
                Canvas.SetTop(nouvelleEtoile, aleatoire.Next((int)this.Fenetre.Height - (int)nouvelleEtoile.Height));
                Canvas.SetLeft(nouvelleEtoile, aleatoire.Next((int)this.Fenetre.Width - (int)nouvelleEtoile.Width));
                Canva.Children.Add(nouvelleEtoile);
            }

        }
    }
}
