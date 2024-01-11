using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBound_Legacy
{
    public class objet
    {
		const String TypeCanon = "canon", TYPE_BOUCLIER = "bouclier", TYPE_BOMBE = "bombe", TYPE_SOIN = "soin", TYPE_SAC_POINT = "sac_point";
		private String nom;

		public String Nom
		{
			get { return nom; }
			set {
				if (String.IsNullOrEmpty(value)) throw new ArgumentNullException("Le nom ne peut pas etre nul ou vide");
				nom = value; }
		}
		private String type;

		public String Type
		{
			get { return type; }
			set { type = value; }
		}


	}
}
