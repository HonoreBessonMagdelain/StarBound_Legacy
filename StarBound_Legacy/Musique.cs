using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StarBound_Legacy
{
    internal class Musique
    {
        private MainWindow fenetre;

        public MainWindow Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }

        public static MediaPlayer defaite = new MediaPlayer();
        public static MediaPlayer musiqueMenu = new MediaPlayer();
        public static MediaPlayer musiqueGameplay = new MediaPlayer();
        public static MediaPlayer tirJoueur = new MediaPlayer();
        public static MediaPlayer tirEnnemi = new MediaPlayer();
        public static MediaPlayer bombeNucleaire = new MediaPlayer();
        public static MediaPlayer mortEnnemi = new MediaPlayer();
        public static MediaPlayer degatJoueur = new MediaPlayer();
        public static MediaPlayer soin = new MediaPlayer();
        public static MediaPlayer bouclier = new MediaPlayer();
        public void LanceMusiqueMenu()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueAccueil.wav"); // chemin d'acces pour la musique

            musiqueMenu.Open(musique);
            musiqueMenu.Volume = this.Fenetre.VolumeSons;
            musiqueMenu.MediaEnded += new EventHandler(Media_Ended_Menu);
            musiqueMenu.Play(); // joue le fichier de la musique

        }
        public void LanceMusiqueGameplay()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueGamePlay.wav"); // chemin d'acces pour la musique

            musiqueGameplay.Open(musique);
            musiqueGameplay.Volume = this.Fenetre.VolumeSons;
            musiqueGameplay.MediaEnded += new EventHandler(Media_Ended_Gameplay);
            musiqueGameplay.Play(); // joue le fichier de la musique
        }
        private void Media_Ended_Gameplay(object? sender, EventArgs e)
        {
            musiqueGameplay.Position = TimeSpan.Zero;
            musiqueGameplay.Play();
        }
        private void Media_Ended_Menu(object? sender, EventArgs e)
        {
            musiqueMenu.Position = TimeSpan.Zero;
            musiqueMenu.Play();
        }
        public void LanceTirJoueur()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/TirJoueur.wav"); // chemin d'acces pour la musique

            tirJoueur.Open(musique);
            tirJoueur.Volume = this.Fenetre.VolumeSFXactuel;
            tirJoueur.Play(); // joue le fichier de la musique

        }
        public void LanceTirEnnemi()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/TirEnnemi.wav"); // chemin d'acces pour la musique

            tirEnnemi.Open(musique);
            tirEnnemi.Volume = this.Fenetre.VolumeSFXactuel;
            tirEnnemi.Play(); // joue le fichier de la musique

        }
        public void LanceBouclier()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/Bouclier.wav"); // chemin d'acces pour la musique

            bouclier.Open(musique);
            bouclier.Volume = this.Fenetre.VolumeSFXactuel;
            bouclier.Play(); // joue le fichier de la musique

        }
        public void LanceDefaite()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/Defaite.wav"); // chemin d'acces pour la musique

            defaite.Open(musique);
            defaite.Volume = this.Fenetre.VolumeSFXactuel;
            defaite.Play(); // joue le fichier de la musique

        }
        public void LanceBombeNucleaire()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/BombeNucleaire.wav"); // chemin d'acces pour la musique

            bombeNucleaire.Open(musique);
            bombeNucleaire.Volume = this.Fenetre.VolumeSFXactuel;
            bombeNucleaire.Play(); // joue le fichier de la musique

        }
        public void LanceDegatJoueur()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/DegatJoueur.wav"); // chemin d'acces pour la musique

           degatJoueur.Open(musique);
           degatJoueur.Volume = this.Fenetre.VolumeSFXactuel;
           degatJoueur.Play(); // joue le fichier de la musique

        }
        public void LanceSoin()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/Soin.wav"); // chemin d'acces pour la musique

            soin.Open(musique);
            soin.Volume = this.Fenetre.VolumeSFXactuel;
            soin.Play(); // joue le fichier de la musique

        }
        public void LanceMortEnnemi()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Bruitages/MortEnnemi.wav"); // chemin d'acces pour la musique

            mortEnnemi.Open(musique);
            mortEnnemi.Volume = this.Fenetre.VolumeSFXactuel;
            mortEnnemi.Play(); // joue le fichier de la musique

        }
    }
}
