using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StarBound_Legacy
{
    internal class Objets
    {
        const String TypeCanon = "canon", TYPE_BOUCLIER = "bouclier", TYPE_BOMBE = "bombe", TYPE_SOIN = "soin", TYPE_SAC_POINT = "sac_point";
        private String nom;

        


        public String Nom
        {
            get { return nom; }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException("Le nom ne peut pas etre nul ou vide !!!");
                nom = value;
            }
        }
        private String typeObjet;

        public String TypeObjet
        {
            get { return typeObjet; }
            set
            {
                if (value != TypeCanon && value != TYPE_BOUCLIER && value != TYPE_SOIN && value != TYPE_SAC_POINT) throw new ArgumentOutOfRangeException("La valeur n'est pas comprise dans les type existant !!!");
                typeObjet = value;
            }
        }
        private int dommageMultiplicateur;

        public int DommageMultiplicateur
        {
            get { return dommageMultiplicateur; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException("Le multiplicateur de dommage ne peut pas etre inférieur a 1");
                dommageMultiplicateur = value;
            }
        }
        private bool utilisable;

        public bool Utilisable
        {
            get { return utilisable; }
            set { utilisable = value; }
        }
        private double chanceDeTomber;

        

        public double ChanceDeTomber
        {
            get { return chanceDeTomber; }
            set
            {
                if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("La chance de tomber doit etre comprise entre 0 et 1 exclu");
                chanceDeTomber = value;
            }
        }

        
    }
}
