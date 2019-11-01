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
            public Carte[] mesCartes;
            public int nbreVie;
            public Joueur(Carte carte1, Carte carte2, Carte carte3) : this()
            {
                mesCartes = new Carte[3];
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
        static public void DistribuerCarte(ref Joueur joueur1, ref Joueur joueurPC,ref Carte[] tabCarte,ref int maxTableau)
        {
            int carteSelect;
            for (int i = 0; i < 3; i++)
            {
                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                PermuterCarte(ref tabCarte, carteSelect, maxTableau);
                joueur1.mesCartes[i] = tabCarte[maxTableau];
                maxTableau--;
                

                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                PermuterCarte(ref tabCarte, carteSelect, maxTableau);
                joueurPC.mesCartes[i] = tabCarte[maxTableau];
                maxTableau--;
                
            }
        }
        
        static public void PermuterCarte(ref Carte[] tabCarte, int carteSelect, int maxTableau)
        {
            Carte carteTemporaire;
            carteTemporaire = tabCarte[carteSelect];
            tabCarte[carteSelect] = tabCarte[maxTableau];
            tabCarte[maxTableau] = carteTemporaire;
        }
        static public void ChangerCarte(ref Carte[] tabCarte, int carteChanger, ref Joueur Joueur, ref int maxTableau)
        {
            int carteSelectionnee;
            carteSelectionnee = (int)generateurNb.Next(0, maxTableau + 1);
            PermuterCarte(ref tabCarte, carteSelectionnee, maxTableau);
            Joueur.mesCartes[carteChanger] = tabCarte[maxTableau];
            maxTableau--;

        }
        static public void ChangerCarteRetournee(ref Carte[] tabCarte, int carteChanger, ref Joueur Joueur, ref int maxTableau, ref int carteRetournee)
        {
            int carteSelectionnee;
            Joueur.mesCartes[carteChanger] = tabCarte[carteRetournee];
            carteSelectionnee = (int)generateurNb.Next(0, maxTableau+1);
            PermuterCarte(ref tabCarte, carteSelectionnee, maxTableau);
            carteRetournee = maxTableau;
            maxTableau--;
        }
        static public int CalculerValeurCartes( Joueur joueur)
        {
            int valeurCarte ;
            int[] tabValeur = { 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                int a = (int)joueur.mesCartes[i].sorteCarte;
                int b = joueur.mesCartes[i].valeurJeu;
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
        static public int CalculerNombreTypeCarte(Joueur joueurPC)
        {
            int nmbre = 1;
            Sorte sorte1, sorte2, sorte3;
            sorte1 = joueurPC.mesCartes[0].sorteCarte;
            sorte2 = joueurPC.mesCartes[1].sorteCarte;
            sorte3 = joueurPC.mesCartes[2].sorteCarte;

            if (sorte2 != sorte1)
                nmbre++;

            if (sorte3 != sorte2 && sorte3!= sorte1)
                nmbre++;

            return nmbre;
        }
        static public int DeterminerPlusPetiteCarte(Joueur joueurPC)
        {
            int pluspetite=0;
            Carte carte1, carte2, carte3;
            carte1 = joueurPC.mesCartes[0];
            carte2 = joueurPC.mesCartes[1];
            carte3 = joueurPC.mesCartes[2];
            if (carte2.valeurJeu < carte1.valeurJeu && carte2.valeurJeu < carte3.valeurJeu)
                pluspetite = 1;

             else if (carte3.valeurJeu < carte1.valeurJeu && carte3.valeurJeu < carte2.valeurJeu)
                pluspetite = 2;

            return pluspetite;
        }
        static public Sorte Sorte2Cartes(Joueur joueur)
        {
            Sorte sorte2Cartes = (Sorte)1;
            int[] nombreCarteParSorte = { 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                int a = (int)joueur.mesCartes[i].sorteCarte;
                nombreCarteParSorte[a - 1] = nombreCarteParSorte[a - 1] + 1;
            }

            int k = 0;
            while (nombreCarteParSorte[k] != 2)
            {
                k++;
                if (nombreCarteParSorte[k] == 2) sorte2Cartes = (Sorte)(k+1);
                
            } 
            return sorte2Cartes;
        }
        static public Sorte Sorte1Carte(Joueur joueur)
        {
            Sorte sorte1Carte = (Sorte)1;
            int[] nombreCarteParSorte = { 0, 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                int a = (int)joueur.mesCartes[i].sorteCarte;
                nombreCarteParSorte[a-1] = nombreCarteParSorte[a - 1] + 1;
            }

            int k = 0;
            while(nombreCarteParSorte[k] != 1)
            {
                k++;
                if (nombreCarteParSorte[k] == 1) sorte1Carte = (Sorte)(k + 1);
            } 
            return sorte1Carte;
        }
        static public int ValeurCartes(Joueur joueurPC, Sorte sorte)
        {
            int valeur = 0;
            for(int i=0; i<3; i++)
            {
                if(joueurPC.mesCartes[i].sorteCarte == sorte)
                {
                    valeur += joueurPC.mesCartes[i].valeurJeu;
                }
            }
            return valeur;
        }
        static public int Index1Carte(Joueur joueurPC, Sorte sorte1Carte)
        {
            int index;
            if (joueurPC.mesCartes[0].sorteCarte == sorte1Carte)
            {
                index = 0;
            }
            else if (joueurPC.mesCartes[1].sorteCarte == sorte1Carte)
                index = 1;
            else index = 2;

            return index;
        }
        static public int Index2Cartes(Joueur joueurPC, Sorte sorte2Cartes)
        {
            int index = 0,  val=12;
            
            for(int i=0; i<3; i++)
            {
                if(joueurPC.mesCartes[i].sorteCarte == sorte2Cartes)
                {
                    if (joueurPC.mesCartes[i].valeurJeu < val)
                    {
                        val = joueurPC.mesCartes[i].valeurJeu;
                        index = i;
                    }
                }

            }
              
            return index;
        }
        static void Main(string[] args)
        { 
            Carte[] tabCarte = new Carte[52];
            //bool partie = true;
            Joueur joueur1,joueurPC;
            Carte initCarte = new Carte(0,0,0);  //pour initialiser mes joueurs
            int maxTableau;
            int carteRetournee;
            int carteChanger;
            bool nouveauTour=true;
            int nombreTypeCarte;

            
            joueur1 = new Joueur(initCarte, initCarte, initCarte);
            joueurPC = new Joueur(initCarte, initCarte, initCarte);

            
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
                    Console.WriteLine(i + 1 + " - sorte : " + joueur1.mesCartes[i].sorteCarte.ToString() + " valeur : " + joueur1.mesCartes[i].valeurJeu);
                }
                Console.WriteLine(" LES CARTES DE JOUEURPC : ");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(i + 1 + " - sorte : " + joueurPC.mesCartes[i].sorteCarte.ToString() + " valeur : " + joueurPC.mesCartes[i].valeurJeu);
                }
                Console.WriteLine(" Carte retournee sorte : " + tabCarte[carteRetournee].sorteCarte.ToString() + " valeur : " + tabCarte[carteRetournee].valeurJeu);
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
                            Console.WriteLine("Votre jeu de carte vaut : " + CalculerValeurCartes( joueur1));
                            if (CalculerValeurCartes(joueur1) < 21)
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
                    nombreTypeCarte = CalculerNombreTypeCarte(joueurPC);
                   
                    switch (nombreTypeCarte)
                    {
                        case 1: //Les trois carte du PC sont de meme sorte...
                        {
                            int plusPetiteCarte = DeterminerPlusPetiteCarte(joueurPC);
                            int autreCarte1 = 1, autreCarte2 = 2;
                            Console.WriteLine("plus petite carte " + plusPetiteCarte);
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            if (plusPetiteCarte == 1)
                            {
                                autreCarte1 = 0;
                                autreCarte2 = 2;
                            }
                            else if (plusPetiteCarte == 2)
                            {
                                autreCarte1 = 0;
                                autreCarte2 = 1;
                            }
                            if (tabCarte[carteRetournee].sorteCarte == joueurPC.mesCartes[autreCarte1].sorteCarte || tabCarte[carteRetournee].sorteCarte == joueurPC.mesCartes[autreCarte2].sorteCarte)
                                ChangerCarteRetournee(ref tabCarte, plusPetiteCarte,ref joueurPC, ref maxTableau, ref carteRetournee);
                            else
                                ChangerCarte(ref tabCarte, plusPetiteCarte, ref joueurPC, ref maxTableau);
                            break;
                        }
                            
                        case 2:
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            
                            Sorte sorte2Cartes = Sorte2Cartes(joueurPC), sorte1Carte = Sorte1Carte(joueurPC);
                            int valeur2Cartes = ValeurCartes(joueurPC, sorte2Cartes), valeur1Carte = ValeurCartes(joueurPC, sorte1Carte);
                            int index1Carte =Index1Carte(joueurPC, sorte1Carte), index2Cartes =Index2Cartes(joueurPC, sorte2Cartes) ;
                            Console.WriteLine("Sorte 2 Carte : " + sorte2Cartes + " valeur 2 cartes : " + valeur2Cartes);
                            Console.WriteLine("Sorte 1 Carte : " + sorte1Carte + " valeur 1 cartes : " + valeur1Carte);
                            Console.WriteLine("Index 1 Carte : " + index1Carte + " Index 2 Cartes : " + index2Cartes);
                            if (valeur2Cartes > valeur1Carte)
                            {
                                if(sorte2Cartes == tabCarte[carteRetournee].sorteCarte)
                                    ChangerCarteRetournee(ref tabCarte, index1Carte, ref joueurPC, ref maxTableau, ref carteRetournee);
                                
                                else
                                    ChangerCarte(ref tabCarte, index1Carte, ref joueurPC, ref maxTableau);
                                
                            }
                            else
                            {
                                if (tabCarte[carteRetournee].sorteCarte == sorte2Cartes || tabCarte[carteRetournee].sorteCarte == sorte1Carte)
                                    ChangerCarteRetournee(ref tabCarte, index2Cartes, ref joueurPC, ref maxTableau, ref carteRetournee);
                                else
                                    ChangerCarte(ref tabCarte, index2Cartes, ref joueurPC, ref maxTableau);
                                
                            }
                            break;
                        case 3:
                        {
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            int plusPetiteCarte = DeterminerPlusPetiteCarte(joueurPC);
                            int autreCarte1 = 1, autreCarte2 = 2;
                            Console.WriteLine("plus petite carte " + plusPetiteCarte);
                            Console.WriteLine("Nombre Carte PC " + nombreTypeCarte);
                            if (plusPetiteCarte == 1)
                            {
                                autreCarte1 = 0;
                                autreCarte2 = 2;
                            }
                            else if (plusPetiteCarte == 2)
                            {
                                autreCarte1 = 0;
                                autreCarte2 = 1;
                            }
                            if (tabCarte[carteRetournee].sorteCarte == joueurPC.mesCartes[autreCarte1].sorteCarte || tabCarte[carteRetournee].sorteCarte == joueurPC.mesCartes[autreCarte2].sorteCarte)
                                ChangerCarteRetournee(ref tabCarte, plusPetiteCarte, ref joueurPC, ref maxTableau, ref carteRetournee);
                            else
                                ChangerCarte(ref tabCarte, plusPetiteCarte, ref joueurPC, ref maxTableau);
                            break;
                            }
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