using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_final
{
    /// <summary>
    /// Permet de créer un dé défini par un numéro, 6 faces, et une face supérieur.
    /// </summary>
    public class De
    {
        private int num;
        private string[] faces;
        private char facesup;

        /// <summary>
        /// Constructeur du dé
        /// </summary>
        /// <param name="num">Numéro du dé</param>
        public De(int num)
        {
            //On atribut au dé 6 faces, prises sur la ligne correspondant au numéro du dé.
            string[] text = File.ReadAllLines("Des.txt");
            this.faces = text[num - 1].Split(';');

            this.num = num;
        }
        /// <summary>
        /// Propriété de la face supérieur du dé
        /// </summary>
        public char FaceSup
        {
            get { return facesup; }
            set { facesup = value; }
        }

        /// <summary>
        /// Détermine la face supérieur du dé.
        /// </summary>
        /// <param name="numface">Entier choisit aléatoirement dans le main entre et 5 qui correspond au numéro de la face supérieur</param>
        public void Lance(int numface)
        {
            string lettre1 = faces[numface];
            facesup = Convert.ToChar(lettre1);

        }
        /// <summary>
        /// Permet d'afficher les caracteristiques du dé (son numéro, ses faces et sa face supérieur)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retour = "Le dé n°" + num.ToString() + " est composé des lettres:";
            for (int i = 0; i < faces.Length; i++)
            {
                retour =   retour + " " +faces[i].ToString();
            }
            if (facesup != 0)
            {
                retour = retour + "\n Sa face supérieur est: " + FaceSup;
            }
            else { retour += ". Le dé n'a pas encore été lancé;"; }

            return retour;
        }

    }
}
