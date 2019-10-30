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
            valeurCarte = tabValeur[0];
            for (int i = 1; i< 4;i++)
            {
                if (tabValeur[i] > valeurCarte)
                    valeurCarte = tabValeur[i];
            }
                return valeurCarte;
        }
        static public int CalculerNombreTypeCarte(Carte[] tabCarte, Joueur joueurPC)
        {
            int nmbre = 1;
            Sorte sorte1, sorte2, sorte3;
            sorte1 = tabCarte[joueurPC.mesCartes[0]].sorteCarte;
            sorte2 = tabCarte[joueurPC.mesCartes[1]].sorteCarte;
            sorte3 = tabCarte[joueurPC.mesCartes[2]].sorteCarte;

            if (sorte2 != sorte1)
                nmbre++;

            if (sorte3 != sorte2 && sorte3!= sorte1)
                nmbre++;

            return nmbre;
        }
        static public int DeterminerPlusPetiteCarte(Carte[] tabCarte, Joueur joueurPC)
        {
            int pluspetite=0;
            Carte carte1, carte2, carte3;
            carte1 = tabCarte[joueurPC.mesCartes[0]];
            carte2 = tabCarte[joueurPC.mesCartes[1]];
            carte3 = tabCarte[joueurPC.mesCartes[2]];
            if (carte2.valeurJeu > carte1.valeurJeu && carte2.valeurJeu > carte3.valeurJeu)
                pluspetite = 1;

             else if (carte3.valeurJeu > carte1.valeurJeu && carte3.valeurJeu > carte2.valeurJeu)
                pluspetite = 2;

            return joueurPC.mesCartes[pluspetite];
        }
        static void Main(string[] args)
        { 
            Carte[] tabCarte = new Carte[52];
            //bool partie = true;
            Joueur joueur1,joueurPC;
            int maxTableau;
            int carteRetournee;
            int carteChanger;
            bool nouveauTour=true;
            int nombreTypeCarte;

            
            joueur1 = new Joueur(0, 0, 0);
            joueurPC = new Joueur(0, 0, 0);

            
                InitialierTabCarte(ref tabCarte);
                maxTableau = 51;
                DistribuerCarte(ref joueur1, ref joueurPC, ref tabCarte, ref maxTableau);
                carteRetournee = Convert.ToInt32(generateurNb.Next(0, maxTableau + 1));
                PermuterCarte(ref tabCarte, carteRetournee, maxTableau);
                carteRetournee = maxTableau;
                maxTableau--;
            do
            {
                Console.WriteLine(" LES CARTES DE JOUEUR1 : ");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(i + 1 + " - sorte : " + tabCarte[joueur1.mesCartes[i]].sorteCarte.ToString() + " valeur : " + tabCarte[joueur1.mesCartes[i]].valeur);
                }
                Console.WriteLine(" LES CARTES DE JOUEURPC : ");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(i + 1 + " - sorte : " + tabCarte[joueurPC.mesCartes[i]].sorteCarte.ToString() + " valeur : " + tabCarte[joueurPC.mesCartes[i]].valeur);
                }
                Console.WriteLine(" Carte retournee sorte : " + tabCarte[carteRetournee].sorteCarte.ToString() + " valeur : " + tabCarte[carteRetournee].valeur);
                int choix;

                

                Console.WriteLine("Que voulez vous faire : \n 1 - prendre une nouvelle carte \n 2 - Prendre la carte retournee \n 3 - Fin de cette manche  (Vous de avoir une jeu qui vaut au moins 21 !!!)");
                choix = Convert.ToInt32(Console.ReadLine());
                switch (choix)
                {
                    case 1:
                        {
                            Console.WriteLine("Quelle carte souhaitez vous changer 1, 2 ou 3 : ");
                            carteChanger = Convert.ToInt32(Console.ReadLine());
                            ChangerCarte(ref tabCarte, carteChanger - 1, ref joueur1, ref maxTableau);
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
                            Console.WriteLine("Votre jeu de carte vaut : " + CalculerValeurCartes(tabCarte, joueur1));
                            if (CalculerValeurCartes(tabCarte, joueur1) < 21)
                                Console.WriteLine("votre jeu ne vaut pas au moins 21, vous ne pouvez pas mettre fin a ce tour  ");
                            else
                            {
                                Console.WriteLine("Fin de ce tour  ");
                                nouveauTour = false;
                            }
                            break;
                        }
                    default: break;
                }
                //Au tour de l'ordinateur de jouer
                if (true) //CalculerValeurCartes(tabCarte, joueurPC) < 21
                {
                    nombreTypeCarte = CalculerNombreTypeCarte(tabCarte, joueurPC);
                   
                    switch (nombreTypeCarte)
                    {
                        case 1: //Les trois carte du PC sont de meme sorte...
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            int plusPetiteCarte = DeterminerPlusPetiteCarte(tabCarte, joueurPC);
                            Console.WriteLine("plus petite carte " + tabCarte[plusPetiteCarte]);
                            break;
                        case 2:
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            break;
                        case 3:
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            break;
                        default:
                            break;
                    }
                }
                else
                {

                }

            } while (nouveauTour==true && maxTableau >1);
        }
    }
}
