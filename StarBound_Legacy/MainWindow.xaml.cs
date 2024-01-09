using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StarBound_Legacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MAX_VIE = 10;
        public int vieJoueur = 3;



        public MainWindow()
        {
            InitializeComponent();
            MediaPlayer playMedia = new MediaPlayer(); // making a new instance of the media player
            var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Musiques/MusiqueAccueil.mp3"); // browsing to the sound folder and then the WAV file location
            playMedia.Open(uri); // inserting the URI to the media player
            playMedia.Play(); // playing the media file from the media player class
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.ShowDialog();
            


            if (menuPrincipal.DialogResult == false)
                Application.Current.Shutdown();
        }

        private void Media_Ended(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OuvertureGarage()
        {
            Garage garage = new Garage();
            garage.Owner = this;
            garage.ShowDialog();
        }
    }
}
