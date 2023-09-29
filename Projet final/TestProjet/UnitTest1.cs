using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Projet_final;
using System.IO;

namespace TestProjet
{
    [TestClass]
    public class TJoueur
    {

        [TestMethod]
        public void Test_Contains()
        {
            Joueur J1 = new Joueur("J1");
            Assert.IsFalse(J1.Contain("abc"));
            J1.Add_Mot("abc");
            Assert.IsTrue(J1.Contain("abc"));
        }
        [TestMethod]
        public void Test_ToString()
        {
            Joueur J1 = new Joueur("J1");
            Assert.AreEqual("Le joueur nommé: J1 a un score de 0\n Il n'a pas encore trouvé de mots", J1.ToString());
            J1.Add_Mot("abc");
            J1.Add_Mot("def");
            J1.Add_Mot("gef");
            Assert.AreEqual("Le joueur nommé: J1 a un score de 0\n Ses mots joués sont: abc def gef ", J1.ToString());
        }

    }
    [TestClass]
    public class TDé
    {
        [TestMethod]
        public void Test_Lancer()
        {
            string[] text = File.ReadAllLines("Des.txt");

            string[] faces1 = text[0].Split(';');
            De dé = new De(1);
            Random r = new Random();
            int ligne = r.Next(0, 6);
            dé.Lance(ligne);
            Assert.AreEqual(Convert.ToChar(faces1[ligne]), dé.FaceSup);

            ligne = 0;
            dé.Lance(ligne);
            Assert.AreEqual('B', dé.FaceSup);
        }
        [TestMethod]
        public void Test_ToString()
        {
            De dé = new De(1);
            int ligne = 0;
            dé.Lance(ligne);
            Assert.AreEqual("Le dé n°1 est composé des lettres: B A J O Q M\n Sa face supérieur est: B", dé.ToString());

        }
    }
    [TestClass]
    public class TDictionnaire
    {
        [TestMethod]
        public void Test_Recherche()
        {
            Dictionnaire Dico = new Dictionnaire("Français");
            Assert.IsTrue(Dico.RechDichoRecursif("JEU"));
            Assert.IsTrue(Dico.RechDichoRecursif("RECHERCHE"));
            Assert.IsFalse(Dico.RechDichoRecursif("azer"));
        }
        [TestMethod]
        public void Test_Recherche2()
        {
            Dictionnaire Dico = new Dictionnaire("Français");
            Assert.IsTrue(Dico.RechDichoRecursif2("JEU"));
            Assert.IsTrue(Dico.RechDichoRecursif2("RECHERCHE"));
            Assert.IsFalse(Dico.RechDichoRecursif2("azer"));
        }
    }
}
