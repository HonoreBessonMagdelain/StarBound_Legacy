﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace StarBound_Legacy
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            
            InitializeComponent();
            
        }
        
        private void Garage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            MainWindow.FenetreAOuvrir = "garage";
        }
        


        private void Quitter(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;

        }

        private void Credits(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            MainWindow.FenetreAOuvrir = "credits";
        }
        private void Reglages(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            MainWindow.FenetreAOuvrir = "reglages";
        }
        private void Jouer(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            MainWindow.FenetreAOuvrir = "jouer";
        }
    }
}
