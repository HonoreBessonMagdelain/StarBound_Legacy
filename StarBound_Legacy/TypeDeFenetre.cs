using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBound_Legacy
{
    internal class TypeDeFenetre
    {
		private String etatFenetre;

		public String EtatFenetre
        {
			get { return this.etatFenetre; }
			set {
				if (value != "garage" && value != "credits" && value != "reglages" && value != "quitter" && value != "jouer")
					throw new ArgumentException("L'état de la fenêtre renseigné n'est pas correct");
				this.etatFenetre = value;
				}
		}
        public TypeDeFenetre(String etatFenetre)
        {
            this.EtatFenetre= etatFenetre;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
