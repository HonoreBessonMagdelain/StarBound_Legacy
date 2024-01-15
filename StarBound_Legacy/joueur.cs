using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBound_Legacy
{
    public class joueur
    {
		private int vie;

		public int Vie
		{
			get { return vie; }
			set {
				if (value > MainWindow.MAX_VIE) throw new ArgumentOutOfRangeException("La vie ne peut pas depasser la vie maximum");
				vie = value; }
		}
		private int vieDebutPartie;

		public int VieDebutPartie
		{
			get { return vieDebutPartie; }
			set {
				if (value > MainWindow.MAX_VIE) throw new ArgumentOutOfRangeException("La vie ne peut pas depasser la vie maximum");
                vieDebutPartie = value; }
		}
		private objet canon;

		public objet Canon
		{
			get { return canon; }
			set { canon = value; }
		}
		private int nbBouclier;

		public int NbBouclier
		{
			get { return nbBouclier; }
			set { nbBouclier = value; }
		}




	}
}
