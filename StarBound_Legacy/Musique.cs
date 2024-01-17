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
        
        public void LanceMusiqueMenu()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueAccueil.mp3"); // chemin d'acces pour la musique

            this.Fenetre.musiqueMenu.Open(musique);
            this.Fenetre.musiqueMenu.Volume = 1;
            this.Fenetre.musiqueMenu.MediaEnded += new EventHandler(Media_Ended_Menu);
            this.Fenetre.musiqueMenu.Play(); // joue le fichier de la musique

        }
        public void LanceMusiqueGameplay()
        {
            var musique = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueGamePlay.mp3"); // chemin d'acces pour la musique

            this.Fenetre.musiqueGameplay.Open(musique);
            this.Fenetre.musiqueGameplay.Volume = 1;
            this.Fenetre.musiqueGameplay.MediaEnded += new EventHandler(Media_Ended_Gameplay);
            this.Fenetre.musiqueGameplay.Play(); // joue le fichier de la musique
        }
        private void Media_Ended_Gameplay(object? sender, EventArgs e)
        {
            this.Fenetre.musiqueGameplay.Position = TimeSpan.Zero;
            this.Fenetre.musiqueGameplay.Play();
        }
        private void Media_Ended_Menu(object? sender, EventArgs e)
        {
            this.Fenetre.musiqueGameplay.Position = TimeSpan.Zero;
            this.Fenetre.musiqueGameplay.Play();
        }
    }
}
