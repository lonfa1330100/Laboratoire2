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
        static public void PermuterCarte(ref Carte[] tabCarte, int carteSelect,ref int maxTableau)
        {
            Carte carteTemporaire;
            carteTemporaire = tabCarte[carteSelect];
            tabCarte[carteSelect] = tabCarte[maxTableau];
            tabCarte[maxTableau] = carteTemporaire;
            maxTableau--;
        }
        static public void DistribuerCarte(ref Joueur joueur1, ref Joueur joueurPC, ref Carte[] tabCarte,ref int maxTableau)
        {
            int carteSelect;
            for (int i=0; i<3; i++)
            {
                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                joueur1.mesCartes[i] = tabCarte[carteSelect];
                PermuterCarte(ref tabCarte, carteSelect, ref maxTableau);

                carteSelect = (int)generateurNb.Next(0, maxTableau + 1);
                joueurPC.mesCartes[i] = tabCarte[carteSelect];
                PermuterCarte(ref tabCarte, carteSelect, ref maxTableau);
            }
        }
        static void Main(string[] args)
        {
            Carte[] tabCarte = new Carte[52];
            //bool partie = true;
            Joueur joueur1,joueurPC;
            int maxTableau;

            InitialierTabCarte(ref tabCarte);
            joueur1 = new Joueur(tabCarte[0], tabCarte[0], tabCarte[0]);
            joueurPC = new Joueur(tabCarte[0], tabCarte[0], tabCarte[0]);

            maxTableau = 51;
            DistribuerCarte(ref joueur1, ref joueurPC, ref  tabCarte, ref maxTableau);

            for(int i = 46; i<52; i++)
            {
                Console.WriteLine("sorte : " + tabCarte[i].sorteCarte.ToString()+" valeur : " + tabCarte[i].valeur);
            }
            Console.WriteLine(" LES CARTES DE JOUEUR1 : " );
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("sorte : " +joueur1.mesCartes[i].sorteCarte.ToString() + " valeur : " + joueur1.mesCartes[i].valeur);
            }
            Console.WriteLine(" LES CARTES DE JOUEURPC : ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("sorte : " + joueurPC.mesCartes[i].sorteCarte.ToString() + " valeur : " + joueurPC.mesCartes[i].valeur);
            }
        }
    }
}
