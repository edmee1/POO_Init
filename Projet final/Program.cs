using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_final
{
    class Program
    {
        /// <summary>
        /// Attribue un nombre de point au joueur en fonction de la taille du mot entré, et rajoute ses points au score du joueur.
        /// </summary>
        /// <param name="mot">Mot inscrit par le joueur</param>
        /// <param name="joueur">Joueur</param>
        public static void PointAttribué(string mot, Joueur joueur)
        {
            int point = 0;
            if (mot.Length == 3)
            {
                point = 2;
            }
            if (mot.Length == 4)
            {
                point = 3;
            }
            if (mot.Length == 5)
            {
                point = 4;
            }
            if (mot.Length == 6)
            {
                point = 5;
            }
            if (mot.Length >= 7)
            {
                point = 7;
            }
            joueur.Score += point;
            Console.Write("Points accordés: " + point + "\t\t");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenu au jeu du Boogle\nJoueur 1, veuillez écrire votre nom");
            string nom = Console.ReadLine();
            Joueur J1 = new Joueur(nom);

            Console.WriteLine("Joueur 2, veuillez écrire votre nom");
            nom = Console.ReadLine();
            Joueur J2 = new Joueur(nom);

            Console.WriteLine("\nBonjour " + J1.Nom + " et " + J2.Nom);

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();

            Console.WriteLine("Le but de ce jeu est de trouver le plus de mots possible sur le plateaux! ");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\nA chaque tour, vous aurez 1 minute pour trouver le plus de mots." +
                "\nLes mots doivent être de 3 lettres au minimum, peuvent être au singulier ou au pluriel, conjugués ou non, et ne doivent pas avoir déja été trouvé" +
                "\nPlus le mots est grand, plus vous gagnez de points");
            System.Threading.Thread.Sleep(2000);


            Console.WriteLine("\nLe barème des points:" +
                "\nTaille du mot: \t\t Points: " +
                "\n 3 \t\t\t 2 \n 4 \t\t\t 3 \n 5 \t\t\t 4 \n 6 \t\t\t 5 \n 7+ \t\t\t 7 \n\n");

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Quel langue souhaitez-vous avoir pour votre dictionnaire?(seul le Français est disponible)");
            string langue = Console.ReadLine();
            Dictionnaire di = new Dictionnaire(langue);

            DateTime débutjeu = DateTime.Now;
            Random r = new Random();
            Joueur[] joueurs = { J1, J2 };
            int numjoueur = 0;
            int tour = 1;

            De[] ensembledes = new De[16];

            Console.WriteLine("Souhaitez-vous avoir des informations sur le dictionnaire utilisé? \nOui: Tapez 1 \tNon: Tapez 2");
            int choix = Convert.ToInt32(Console.ReadLine());
            if (choix == 1)
            {
                Console.WriteLine(di.ToString());
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("\nSouhaitez-vous avoir un déscriptif complet de vos dés? \nOui: Tapez 1 \tNon: Tapez 2");
            choix = Convert.ToInt32(Console.ReadLine());
            if (choix == 1)
            {
                for (int i = 1; i < 17; i++)
                {
                    De des = new De(i);
                    ensembledes[i - 1] = des;
                    Console.WriteLine(ensembledes[i - 1].ToString());
                }
            }

            Console.Write("\nPressez n'importe qu'elle touche lorsque vous êtes prêts à commencer\n");
            Console.ReadKey();

            while (débutjeu.AddMinutes(6) > DateTime.Now)
            {
                tour++;
                if ((tour % 2) == 0)
                { numjoueur = 0; }
                else
                { numjoueur = 1; }
                Console.WriteLine("\nA vous de joueur," + joueurs[numjoueur].Nom);

                DateTime débuttour = DateTime.Now;

                for (int i = 1; i < 17; i++)
                {
                    De des = new De(i);
                    int numface = r.Next(0, 6);
                    des.Lance(numface);

                    ensembledes[i - 1] = des;
                }
                Plateau plateau = new Plateau(ensembledes, r);
                Console.WriteLine(plateau.ToString());
                System.Threading.Thread.Sleep(2000);

                while (débuttour.AddMinutes(1) > DateTime.Now)
                {
                    Console.Write("\nEntrez un mot: ");
                    string mot = Console.ReadLine().ToUpper();

                    if (mot.Length >= 3)
                    {
                        if (joueurs[numjoueur].Contain(mot) != true)
                        {
                            if (plateau.Test_Plateau4(mot) == true)
                            {
                                if (di.RechDichoRecursif2(mot) == true)
                                {
                                    Console.WriteLine("Mot valide.");

                                    PointAttribué(mot, joueurs[numjoueur]);
                                    joueurs[numjoueur].Add_Mot(mot);

                                    Console.Write("Votre score est maintenant de: " + joueurs[numjoueur].Score + "\nLes mots déjà cités sont: ");
                                    Console.WriteLine(joueurs[numjoueur].Contains_ToString());
                                }
                                else { Console.WriteLine("Le mot n'est pas dans le dictionnaire"); }
                            }
                            else { Console.WriteLine("Le mot ne marche pas sur le plateau"); }
                        }
                        else { Console.WriteLine("Mot déjà trouvé"); }
                    }
                    else { Console.WriteLine("Mot trop court"); }

                    if (débuttour.AddSeconds(48) < DateTime.Now && débuttour.AddSeconds(53) > DateTime.Now)
                    {
                        Console.WriteLine("\nAttention il ne vous reste plus que: " + (débuttour.AddMinutes(1) - DateTime.Now).Seconds + " secondes");
                    }

                }
                Console.WriteLine("\nFin du tour" + "\n" + joueurs[numjoueur].ToString() + "\n");

                System.Threading.Thread.Sleep(2000);
            }

            Console.WriteLine("\nFin du jeu!");
            System.Threading.Thread.Sleep(2000);
            if (J1.Score > J2.Score)
            {
                Console.WriteLine(J1.Nom + " a gagné avec un score de " + J1.Score + " contre " + J2.Score + " pour " + J2.Nom);
            }
            else if (J1.Score < J2.Score)
            {
                Console.WriteLine(J2.Nom + " a gagné avec un score de " + J2.Score + " contre " + J1.Score + " pour " + J1.Nom);
            }
            else
            {
                Console.WriteLine("Les 2 joueurs sont a égalité avec un score de: " + J1.Score);
            }
            System.Threading.Thread.Sleep(2000);
            Console.ReadKey();
        }
    }
}
