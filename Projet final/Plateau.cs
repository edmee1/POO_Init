using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_final
{
    /// <summary>
    /// Class permetant de créer un plateau défini par 16 dés et leur face supérieur.
    /// </summary>
    class Plateau
    {
        private De[] des;
        /// <summary>
        /// Faces supérieur des dés sous forme de liste
        /// </summary>
        private List<char> face;
        /// <summary>
        /// Faces supérieur des dés sous forme d'une matrice 4*4
        /// </summary>
        private char[,] tabface;

        public Plateau(De[] des, Random r)
        {
            List<char> faces = new List<char>();
            this.des = des;
            for (int i = 0; i < des.Length; i++)
            {
                faces.Add(des[i].FaceSup);
            }
            this.face = faces;
            int a = 0;
            char[,] tabface = new char[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    tabface[i, j] = face[a];
                    a++;
                }
            }
            this.tabface = tabface;
        }
        public De[] Des
        {
            get { return des; }
            set { des = value; }
        }
        public char[,] Tabface
        {
            get { return tabface; }
            set { tabface = value; }
        }

        public List<char> Face
        {
            get { return face; }
            set { face = value; }
        }
        /// <summary>
        /// Permet d'afficher le plateau sous forme 4*4
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retour = "";
            int j = 0;
            while (j < 16)
            {
                for (int i = 0; i < 4; i++)
                {
                    retour = retour + " " + face[j];
                    j++;
                }
                retour = retour + "\n";
            }
            return retour;
        }


        /// <summary>
        /// Teste si le mot passé en paramètre respecte la contrainte d’adjacence.
        /// </summary>
        /// <param name="mot">Mot testé</param>
        /// <returns>True si le mot respect les contraintes d'adjacence, false sinon</returns>
        public bool Test_Plateau4(string mot)
        {
            bool retour = false;

            List<int[]>[] index = new List<int[]>[mot.Length];
            List<int[]>[] index2 = new List<int[]>[mot.Length];
            bool stop = false;

            /*Fonction permettant de récuperer tout les index des lettres du mots sur le plateau. 
             * Ces index sont rangé dans une liste mis dans un tableau. Chaque case du tableau est associé à une lettre du mot.
             * La liste contient les deiffernet index de la lettre (cas où il y a plusieur fois la même lettre sur le plateau).
             */
            for (int lettre = 0; lettre < mot.Length && stop == false; lettre++)
            {
                List<int[]> liste = new List<int[]>();
                index[lettre] = liste;
                if (face.IndexOf(mot[lettre]) == -1)
                {
                    //Si une lettre du mot n'est pas présente sur le plateau, on arrête directement le test.
                    stop = true;
                }
                else for (int i = 0; i < tabface.GetLength(0); i++)
                    {
                        for (int j = 0; j < tabface.GetLength(1); j++)
                        {
                            if (mot[lettre] == tabface[i, j])
                            {
                                //(i,j) est un des index de la lettre du mot sur le plateau                                
                                int[] tab = { i, j };
                                index[lettre].Add(tab);
                            }
                        }
                    }
            }
            //Si la première lettre est sur le plateau, elle vérifie la contrainte d'adjacente avec la lettre précédante (puisqu'il n'y en a pas)
            index2[0] = index[0];

            /*On va ici tester si les lettres du mots sont adjacentes, et si l'on ne revient pas sur un même lettre.
             * On va vérifié pour chaque face d'une lettre si elle est adjacente à une face d'une lettre précéente (qui était déja adjacente à la lettre précedente et ainsi de suite)
             * Si oui on marque ses coordonées dans une liste. 
             *Nous avons ainsi deux liste, une avec les coordonnée des lettres adjacentes aux lettres d'avant et une autre avec les coordonnée de toute les lettre du mots sur le plateau.
             */
            for (int lettre = 1; lettre < mot.Length && stop == false; lettre++)
            {
                List<int[]> liste2 = new List<int[]>();
                index2[lettre] = liste2;

                for (int listeindex = 0; listeindex < index[lettre].Count; listeindex++)
                {
                    for (int indexAdjacents = 0; indexAdjacents < index2[lettre - 1].Count; indexAdjacents++)
                    {
                        int i = index[lettre][listeindex][0];
                        int j = index[lettre][listeindex][1];
                        int[] tab = { i, j };
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                if (i == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1] || i + 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1] || i + 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1])
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                            else if (j == 3)
                            {
                                if (i == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1] || i + 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1] || i + 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1])
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                            else
                            {
                                if ((i == index2[lettre - 1][indexAdjacents][0] && (j + 1) == index2[lettre - 1][indexAdjacents][1]) || ((i + 1) == index2[lettre - 1][indexAdjacents][0] && (j + 1) == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && (j - 1) == index2[lettre - 1][indexAdjacents][1]) || ((i + 1) == index2[lettre - 1][indexAdjacents][0] && (j - 1) == index2[lettre - 1][indexAdjacents][1]) || ((i + 1) == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            if (j == 0)
                            {
                                if ((i - 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                            else if (j == 3)
                            {
                                if ((i - 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                            else
                            {
                                if ((i - 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))
                                {
                                    index2[lettre].Add(tab);
                                }
                            }
                        }
                        else if (j == 0)
                        {
                            if ((i - 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))
                            {
                                index2[lettre].Add(tab);
                            }
                        }
                        else if (j == 3)
                        {
                            if ((i - 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]))

                            {
                                index2[lettre].Add(tab);
                            }
                        }
                        else
                        {
                            if ((i + 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j == index2[lettre - 1][indexAdjacents][1]) || (i + 1 == index2[lettre - 1][indexAdjacents][0] && j - 1 == index2[lettre - 1][indexAdjacents][1]) || (i == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]) || (i - 1 == index2[lettre - 1][indexAdjacents][0] && j + 1 == index2[lettre - 1][indexAdjacents][1]))

                            {
                                index2[lettre].Add(tab);
                            }
                        }
                    }
                }
                if (index2[lettre].Count == 0)
                {
                    stop = true;
                }
            }

            //On étudie s'il existe un "chemin" formant le mot respectant les contraintes d'adjacence. Si oui le "stop" est false.
            if (stop == false)
            {
                retour = true;
            }
            return retour;
        }
    }
}
