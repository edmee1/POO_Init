using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_final
{
    /// <summary>
    /// Permet d'associer les mots du dictionnaire avec leur longueur et leur langue.
    /// </summary>
    public class Dictionnaire
    {
        /// <summary>
        /// Dictionary qui associe une liste de mots de la même longueur avec leur longueur. La clé est la longueur et la valeur la liste.
        /// </summary>
        private Dictionary<int, List<string>> Mots;
        private List<string>[] MotsTab;
        private string Langue;

        /// <summary>
        /// Constructeur du dictionnaire
        /// </summary>
        public Dictionnaire(string langue)
        {
            this.Langue = langue;

            Dictionary<int, List<string>> Mots = new Dictionary<int, List<string>>();
            string[] text = File.ReadAllLines("MotsPossibles.txt");
            string[] text1 = null;
            List<string>[] text2 = new List<string>[text.Length];

            int a = 2; //Nombre de lettre dans le mot
            int b = 0;
            for (int i = 1; i < text.Length; i = i + 2)
            {
                List<string> text0 = new List<string>();
                text1 = text[i].Split(' ');
                for (int j = 0; j < text1.Length; j++)
                {
                    //On met tout les mots de même longueur dans un même liste
                    text0.Add(text1[j]);
                }
                text1 = null;

                //On ajoute au dictionary la liste crée avec la longueur des mots dedans.
                Mots.Add(a, text0);

                text2[b] = text0;

                a++;
                b++;
            }
            this.Mots = Mots;
            this.MotsTab = text2;
        }

        /// <summary>
        /// Décrit le dictionnaire (nombre de mots par longueur du mot)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retour = "La langue du dictionnaire est: " + Langue + "\nIl y a: ";
            for (int i = 2; i < Mots.Count + 2; i++)
            {
                retour = retour + Mots[i].Count + " mots à " + i + " lettres, ";
            }

            return retour;
        }
        /// <summary>
        /// Recherche si un mot entré en paramètre appartient au dictionnaire
        /// </summary>
        /// <param name="mot"></param>
        /// <returns>True si le mot appartient bien au dictionnaire, False sinon.</returns>
        public bool RechDichoRecursif(string mot)
        {
            bool appartient = false;
            int taille = mot.Length;
            if (Mots.ContainsKey(taille) == true)
            {
                /*On recherche dans la liste des mots de la taille du mot entré en paramètre si ce mot appartient ou non à la liste.
                 * S'il n'appartient pas à cette liste, alors il n'appartient pas au dictionnaire */
                appartient = Mots[taille].Contains(mot);
            }

            return appartient;
        }

        /// <summary>
        /// Recherche si un mot entré en paramètre appartient au dictionnaire de façon dicotomique en utilisant une méthode récursive.
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public bool RechDichoRecursif2(string mot, int debut = 0, int fin = 13)
        {
            int milieu = (fin + debut) / 2;

            if (mot.Length > 15)
            { return false; }
            if (mot.Length == milieu + 2)
            {
                if (MotsTab[milieu].Contains(mot))
                {

                    return true;
                }
                else { return false; }
            }
            else if (mot.Length < milieu + 2)
            {
                return RechDichoRecursif2(mot, debut, milieu);

            }
            else
            {
                return RechDichoRecursif2(mot, milieu, fin);
            }
        }
    }
}
