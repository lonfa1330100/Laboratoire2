using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire2
{
    enum Sorte { Coeur = 1, Pique = 2, Carreau = 3, Trèfle = 4 };
    class Program
    {
        static Random generateurNb = new Random();
        public struct Carte
        {
            public Sorte sorteCarte;
            public int valeur;
            public int valeurJeu;

            public Carte(Sorte _sorteCarte, int _valeur, int _valeurJeu) : this()
            {
                sorteCarte = _sorteCarte;
                valeur = _valeur;
                valeurJeu = _valeurJeu;
            }
        }
        public struct Joueur
        {
            public int[] mesCartes;
            public int nbreVie;
            public Joueur(int carte1, int carte2, int carte3) : this()
            {
                mesCartes = new int[3];
                nbreVie = 3;
                mesCartes[0] = carte1;
                mesCartes[1] = carte2;
                mesCartes[2] = carte3;
            }
        }
        static void InitialierTabCarte(ref Carte[] tabCarte)
        {
            int k = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    tabCarte[k].sorteCarte = (Sorte)i;
                    tabCarte[k].valeur = j;
                    if (j == 1)
                        tabCarte[k].valeurJeu = 11;
                    else if (j > 9)
                        tabCarte[k].valeurJeu = 10;
                    else
                        tabCarte[k].valeurJeu = j;
                    k++;
                }
            }
        }
        static public void DistribuerCarte(ref Joueur joueur1, ref Joueur joueurPC, ref Carte[] tabCarte,ref int maxTableau)
        {
            int carteSelect;
            for (int i=0; i<3; i++)
            {
                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                joueur1.mesCartes[i] = maxTableau;
                maxTableau--;
                PermuterCarte(ref tabCarte, carteSelect, maxTableau);

                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                joueurPC.mesCartes[i] = maxTableau;
                maxTableau--;
                PermuterCarte(ref tabCarte, carteSelect, maxTableau);
            }
        }
        static public void PermuterCarte(ref Carte[] tabCarte, int carteSelect, int maxTableau)
        {
            Carte carteTemporaire;
            carteTemporaire = tabCarte[carteSelect];
            tabCarte[carteSelect] = tabCarte[maxTableau];
            tabCarte[maxTableau] = carteTemporaire;
        }
        static public void ChangerCarte( ref Carte[] tabCarte, int carteChanger, ref Joueur Joueur, ref int maxTableau )
        {
            int carteSelectionnee;
            carteSelectionnee = (int)generateurNb.Next(0, maxTableau+1);
            PermuterCarte(ref tabCarte, carteSelectionnee, maxTableau);
            Joueur.mesCartes[carteChanger] = maxTableau;
            maxTableau--;

        }
        static public void ChangerCarteRetournee(ref Carte[] tabCarte, int carteChanger, ref Joueur Joueur, ref int maxTableau, ref int carteRetournee)
        {
            int carteSelectionnee;
            Joueur.mesCartes[carteChanger] = carteRetournee;
            carteSelectionnee = (int)generateurNb.Next(0, maxTableau);
            PermuterCarte(ref tabCarte, carteSelectionnee, maxTableau);
            carteRetournee = maxTableau;
            maxTableau--;
        }
        static public int CalculerValeurCartes(Carte[] tabCarte, Joueur joueur)
        {
            int valeurCarte = 0;
            int[] tabValeur = { 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                int a =   (int)tabCarte[joueur.mesCartes[i]].sorteCarte;
                int b = tabCarte[joueur.mesCartes[i]].valeurJeu;
                tabValeur[a-1] = tabValeur[a - 1] + b;
            }

            Console.WriteLine("Coeur : "+ tabValeur[0] + " pique : "+ tabValeur[1] + " Carreaux : "+ tabValeur[2] + " Trefle : "+ tabValeur[3]);
            valeurCarte = tabValeur[0];
            for (int i = 1; i< 4;i++)
            {
                if (tabValeur[i] > valeurCarte)
                    valeurCarte = tabValeur[i];
            }
                return valeurCarte;
        }
        static void Main(string[] args)
        { 
            Carte[] tabCarte = new Carte[52];
            //bool partie = true;
            Joueur joueur1,joueurPC;
            int maxTableau;
            int carteRetournee;
            int carteChanger;
            //bool nouveauTour=true;

            InitialierTabCarte(ref tabCarte);
            joueur1 = new Joueur(0, 0, 0);
            joueurPC = new Joueur(0, 0, 0);
            maxTableau = 51;
            DistribuerCarte(ref joueur1, ref joueurPC, ref tabCarte, ref maxTableau);
            carteRetournee = Convert.ToInt32(generateurNb.Next(0, maxTableau + 1));
            PermuterCarte(ref tabCarte, carteRetournee, maxTableau);
            carteRetournee = maxTableau;
            maxTableau--;

            Console.WriteLine(" LES CARTES DE JOUEUR1 : ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i+1 +" - sorte : " + tabCarte[joueur1.mesCartes[i]].sorteCarte.ToString() + " valeur : " + tabCarte[joueur1.mesCartes[i]].valeur);
            }
            Console.WriteLine(" Carte retournee sorte : " + tabCarte[carteRetournee].sorteCarte.ToString() + " valeur : " + tabCarte[carteRetournee].valeur);
            int choix;

            Console.WriteLine(" Carte retournee index : " + carteRetournee + " max: " + maxTableau);

            Console.WriteLine("Que voulez vous faire : ");
            choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                {
                    Console.WriteLine("Quelle carte souhaitez vous changer 1, 2 ou 3 : ");
                    carteChanger = Convert.ToInt32(Console.ReadLine());
                    ChangerCarte(ref tabCarte, carteChanger-1 , ref joueur1, ref maxTableau);
                    break;
                }
                case 2:
                {
                    Console.WriteLine("Quelle carte souhaitez vous changer 1, 2 ou 3 : ");
                    carteChanger = Convert.ToInt32(Console.ReadLine());
                    ChangerCarteRetournee(ref tabCarte, carteChanger - 1, ref joueur1, ref maxTableau, ref carteRetournee);
                    break;
                }
                case 3:
                {
                        Console.WriteLine( "Votre jeu de carte vaut : " + CalculerValeurCartes(tabCarte, joueur1));
                    break;
                }
                default: break;

            }
             
        }
    }
}
