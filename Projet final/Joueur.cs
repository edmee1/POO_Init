using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_final
{
    /// <summary>
    /// Class permetant de créer un joueur, définit par son nom, son score et ses mots trouvés.
    /// </summary>
    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> listmots = new List<string>();

        /// <summary>
        /// Constructeur de la class Joueur
        /// </summary>
        /// <param name="nom">Nom du joueur</param>
        public Joueur(string nom)
        {
            this.nom = nom;
            this.score = 0;
        }
        /// <summary>
        /// Propriéte du nom du joueur
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        /// <summary>
        /// Propriéte du score du joueur
        /// </summary>
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        /// <summary>
        /// Propriétés de la liste de mots déjà joué par le joueur.
        /// </summary>
        public List<string> Listmots
        {
            get { return listmots; }
            set { listmots = value; }
        }
        /// <summary>
        /// Verifie si un mot rentré en paramètre appartient à la liste des mots du joueur
        /// </summary>
        /// <param name="mot"></param>
        /// <returns> Truee si le mot appartient à la liste, et False sinon</returns>
        public bool Contain(string mot)
        {
            return listmots.Contains(mot);
        }
        /// <summary>
        /// Permet d'ajouter à la liste des mots du joueur le mot entré en paramètre
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            listmots.Add(mot);
        }
        /// <summary>
        /// Permet d'afficher la liste des mots du joueur
        /// </summary>
        /// <returns>Un string contenant les mots de la liste</returns>
        public string Contains_ToString()
        {
            string retour = "";
            for (int i = 0; i < listmots.Count; i++)
            {
                retour = retour + listmots[i].ToString() + " ";
            }
            return retour;
        }
        /// <summary>
        /// Décrit le joueur (son score, son nom et sa liste de mots
        /// </summary>
        /// <returns>Un string contenant la déscription du joueur</returns>
        public override string ToString()
        {
            string retour = "Le joueur nommé: " + nom + " a un score de " + score;
            if (listmots.Count != 0)
            {
                retour = retour + "\n Ses mots joués sont: ";
                for (int i = 0; i < listmots.Count; i++)
                {
                    retour = retour + listmots[i].ToString() + " ";
                }
            }
            else
            {
                retour = retour + "\n Il n'a pas encore trouvé de mots";
            }
            return retour;

        }

    }
}
