using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StarBound_Legacy
{
    internal class Apparences
    {
        public static ImageBrush boutonRetourMenuAppuye = new ImageBrush();
        public static ImageBrush boutonRetourMenuRelache = new ImageBrush();
        public static ImageBrush boutonRejouerAppuye = new ImageBrush();
        public static ImageBrush boutonRejouerRelache = new ImageBrush();
        public static ImageBrush fondObjetGarage = new ImageBrush();
                
        public static ImageBrush soin = new ImageBrush();
        public static ImageBrush soinSelectionne = new ImageBrush();
        public static ImageBrush bombe = new ImageBrush();
        public static ImageBrush bombeSelectionne = new ImageBrush();
        public static ImageBrush bouclier = new ImageBrush();
        public static ImageBrush bouclierSelectionne = new ImageBrush();
                
        public static ImageBrush pistoletLaser = new ImageBrush();
        public static ImageBrush pistoletLaserSelectionne = new ImageBrush();
        public static ImageBrush lanceBombe = new ImageBrush();
        public static ImageBrush lanceBombeSelectionne = new ImageBrush();
        public static ImageBrush miniGun = new ImageBrush();
        public static ImageBrush miniGunSelectionne = new ImageBrush();
                
        public static ImageBrush soinDescription = new ImageBrush();
        public static ImageBrush bouclierDescription = new ImageBrush();
        public static ImageBrush bombeDescription = new ImageBrush();
                
        public static ImageBrush pistoletLaserDescription = new ImageBrush();
        public static ImageBrush lanceBombeDescription = new ImageBrush();
        public static ImageBrush miniGunDescription = new ImageBrush();
                
        public static ImageBrush plusDescription = new ImageBrush();
        public static ImageBrush plusRelache = new ImageBrush();
        public static ImageBrush plusAppuye = new ImageBrush();
                
        public static ImageBrush vaisseau2 = new ImageBrush();
        public static ImageBrush vaisseau3 = new ImageBrush();
        public static ImageBrush vaisseau4 = new ImageBrush();
                
        public static ImageBrush prixRelache = new ImageBrush();
        public static ImageBrush prixAppuye = new ImageBrush();

        
        public static void InitialisationImagesGarage()
        {
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

        }
        public static void InitialisationImagesMainWindow()
        {
            bombe.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ObjetsSpeciaux/BombeNucleaire.png"));
        }
    }
}
