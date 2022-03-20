using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions; //serve per rimuovere pezzi di stringa

namespace funcion1Console
{
    class Program
    {
        public struct SuddivisioneFunzione
        {
            public string funzione;
            public int[] PezziFunzione;
            public string[] SingoliPezziFunzione;
            public string[] SingoliPezziFunzioneBackup;
            public int[] coefficenti;
            public int[] esponenti;
        }

        static void Main(string[] args)
        {

            SuddivisioneFunzione FUNZIONE;
            /*string funzione, SommaBackup="";*/
            //int[] PezziFunzione = new int[100]; //salva posizione in cui si trova operatore
            //string[] SingoliPezziFunzione = new string[100]; //singoli numeri
            int indice = 1, /*contatore1=0;*/ aumento = 0;
            //int[] coefficenti = new int[100]; //coefficenti delle x
            //int[] esponenti = new int[100]; //esponenti delle x
            double[,] coordinata = new double[2, 1000];
            /*int x = 0;*/
            //string[] SingoliPezziFunzioneBackup = new string[100];

            FUNZIONE.SingoliPezziFunzione = new string[100];
            FUNZIONE.PezziFunzione = new int[100];
            FUNZIONE.esponenti = new int [100];
            FUNZIONE.SingoliPezziFunzioneBackup = new string[100];
            FUNZIONE.coefficenti = new int [100];
            FUNZIONE.esponenti = new int [100];



            for (int i = 0; i < FUNZIONE.coefficenti.Length; i++)//pongo tutti i possibili coefficenti pari a 1, quindi come inesistenti
            {
                FUNZIONE.esponenti[i] = 1;
                FUNZIONE.coefficenti[i] = 1;
            }

            FUNZIONE.funzione = " "; //aggiungo lo spazio all'inizio
            FUNZIONE.funzione += Console.ReadLine(); //prendo in input

            IndividuazioneOperatori(FUNZIONE, ref indice);
            
            SuddivisioneSottostringhe(FUNZIONE, indice);

            IndividuazioneCoefficentiEsponenti(FUNZIONE, indice);

            for (int i = 0; i < 100; i++)
            {
                FUNZIONE.SingoliPezziFunzioneBackup[i] = FUNZIONE.SingoliPezziFunzione[i];
            }

            IndividuazioneCoordinate(FUNZIONE, indice, coordinata);
            

            for (int i = 0; i < 1; i++)//stampa le coordinate
            {
                for(int j=0;j<1000;j++)
                {
                    Console.WriteLine("x=" + coordinata[0, j] + "\t \t  y=" + coordinata[1, j]);
                }
                Console.WriteLine();
            }
        }

        public static void IndividuazioneOperatori (SuddivisioneFunzione Funzione, ref int Indice)//trovo la posizione degli operatori
        {
            Funzione.PezziFunzione[0] = 0; //assegno al primo pezzo del vettore la posizione 0
            for (int i = 0; i < Funzione.funzione.Length; i++) //qui salviamo in un array le posizioni in cui stanno gli operatori
            {
                if (Funzione.funzione.Substring(i, 1) == "+" || Funzione.funzione.Substring(i, 1) == "-" || Funzione.funzione.Substring(i, 1) == "*" || Funzione.funzione.Substring(i, 1) == "/")
                {
                    Funzione.PezziFunzione[Indice] = i; //qui ci sono operatori
                    Indice++; //quantità di operatori nella funzione
                }
            }
            Funzione.PezziFunzione[Indice] = Funzione.funzione.Length;
        }

        public static void SuddivisioneSottostringhe (SuddivisioneFunzione Funzione, int Indice)
        {
            int contatore = 0, aumento=0;
            while (contatore < Indice) //mi salvo in un array di stringe ogni sottostringa in cui ho suddiviso la mia funzione
            {
                if (contatore > 0)
                    aumento = 1;
                //SingoliPezziFunzione[contatore1]=" ";
                Funzione.SingoliPezziFunzione[contatore] = Funzione.funzione.Substring(Funzione.PezziFunzione[contatore] + aumento, (Funzione.PezziFunzione[contatore + 1] - Funzione.PezziFunzione[contatore] - aumento));
                //qui uso la variabile aumento perchè sennò nella sottostringa ci sarebbe acnhe l'operatore
                Funzione.SingoliPezziFunzione[contatore] += " ";
                contatore++;
            }
        }

        public static void IndividuazioneCoefficentiEsponenti (SuddivisioneFunzione Funzione, int Indice)//trovo i coefficenti e gli esponenti
        {
            for (int i = 0; i < Indice; i++)
            {
                for (int j = 0; j < Funzione.SingoliPezziFunzione[i].Length - 1; j++) //cerco l'incognita x in ogni pezzo di funzione
                {
                    if (Funzione.SingoliPezziFunzione[i].Substring(j, 2).ToUpper() == "X^")
                    {
                        Funzione.esponenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(j + 2, Funzione.SingoliPezziFunzione[i].Length - (j + 2))); //prendiamo l'esponente quindi se la sottostringa è uguale a "x^" sappiamo che dovremo salvarci nell'array l'elezione
                        Funzione.coefficenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(0, j));
                        j = Funzione.SingoliPezziFunzione[i].Length; //mettiamo questo così esce da for
                    }
                    else if (Funzione.SingoliPezziFunzione[i].Substring(j, 2).ToUpper() != "X^") //se non è elevato con la "^" allora
                    {
                        Funzione.esponenti[i] = 0; //nel dubbio poniamolo =0
                        if (Funzione.SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X") //se invece è presente la x allora cambiamo l'esponete in 
                        {
                            Funzione.esponenti[i] = 1;
                            if (j > 1)
                                Funzione.coefficenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(0, j));
                            j = Funzione.SingoliPezziFunzione[i].Length;
                        }
                        else
                        {
                            if (j == Funzione.SingoliPezziFunzione[i].Length - 2)
                                Funzione.coefficenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(0, Funzione.SingoliPezziFunzione[i].Length - 1));
                        }
                    }
                }
            }
        }

        public static void IndividuazioneCoordinate (SuddivisioneFunzione Funzione, int Indice, double[,] MatriceCoordinate)//trovo i coefficenti e gli esponenti
        {
            double x = 0;
            int contatore=0;
            string SommaBackup = "";
            while (contatore < 1000)//troviamo le coordinate di alcuni punti appartenenti alla funzione
            {
                SommaBackup = "";
                for (int i = 0; i < 100; i++)
                {
                    Funzione.SingoliPezziFunzione[i] = Funzione.SingoliPezziFunzioneBackup[i]; //ripristina i valori originali
                }
                MatriceCoordinate[0, contatore] = x;

                for (int i = 0; i < Indice; i++)
                {
                    for (int j = 0; j < Funzione.SingoliPezziFunzione[i].Length; j++)
                    {
                        if (Funzione.SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X")// trova la x
                        {
                            Funzione.SingoliPezziFunzione[i] = Regex.Replace(Funzione.SingoliPezziFunzione[i], "[x,X,^]", string.Empty); //toglie la x e la sostituisce con *(il valore della x) per esponente volte
                            Funzione.SingoliPezziFunzione[i] = Funzione.SingoliPezziFunzione[i].Remove(j, 1);
                            for (int p = 0; p < Funzione.esponenti[i]; p++)
                            {
                                Funzione.SingoliPezziFunzione[i] += "*";
                                Funzione.SingoliPezziFunzione[i] += x;
                            }
                            Funzione.SingoliPezziFunzione[i] = Regex.Replace(Funzione.SingoliPezziFunzione[i], "[,]", ".");//bicos DataTable.Compute vuole il punto e non la virgola
                        }
                    }
                }
                for (int i = 0; i < Indice; i++)
                {
                    SommaBackup += Funzione.funzione.Substring(Funzione.PezziFunzione[i], 1) + Funzione.SingoliPezziFunzione[i];
                }

                DataTable dt = new DataTable();
                var yy = dt.Compute(SommaBackup, "");
                double y = Convert.ToDouble(yy);
                MatriceCoordinate[1, contatore] = y;

                contatore++;
                x+=0.25;
            }
        }

        

        
    }
}

