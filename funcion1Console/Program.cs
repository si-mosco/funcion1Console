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
            string /*funzione,*/ SommaBackup="";
            //int[] PezziFunzione = new int[100]; //salva posizione in cui si trova operatore
            //string[] SingoliPezziFunzione = new string[100]; //singoli numeri
            int indice = 1, contatore1 = 0, aumento = 0;
            //int[] coefficenti = new int[100]; //coefficenti delle x
            //int[] esponenti = new int[100]; //esponenti delle x
            decimal[,] coordinata = new decimal[2, 10];
            int x = 0;
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
            FUNZIONE.funzione =  " ";
            FUNZIONE.funzione += Console.ReadLine();
            FUNZIONE.PezziFunzione[0] = 0;
            for (int i = 0; i < FUNZIONE.funzione.Length; i++) //qui salviamo in un array le posizioni in cui stanno gli operatori
            {
                if (FUNZIONE.funzione.Substring(i, 1) == "+" || FUNZIONE.funzione.Substring(i, 1) == "-" || FUNZIONE.funzione.Substring(i, 1) == "*" || FUNZIONE.funzione.Substring(i, 1) == "/")
                {
                    FUNZIONE.PezziFunzione[indice] = i; //qui ci sono operatori
                    indice++; //quantità di operatori nella funzione
                }
            }
            FUNZIONE.PezziFunzione[indice] = FUNZIONE.funzione.Length;

            while (contatore1 < indice) //mi salvo in un array di stringe ogni sottostringa in cui ho suddiviso la mia funzione
            {
                if (contatore1 > 0)
                    aumento = 1;
                //SingoliPezziFunzione[contatore1]=" ";
                FUNZIONE.SingoliPezziFunzione[contatore1] = FUNZIONE.funzione.Substring(FUNZIONE.PezziFunzione[contatore1] + aumento, (FUNZIONE.PezziFunzione[contatore1 + 1] - FUNZIONE.PezziFunzione[contatore1] - aumento));
                FUNZIONE.SingoliPezziFunzione[contatore1] += " ";
                contatore1++;
            }

            for (int i = 0; i < contatore1; i++)
            {
                for (int j = 0; j < FUNZIONE.SingoliPezziFunzione[i].Length - 1; j++) //cerco l'incognita x in ogni pezzo di funzione
                {
                    if (FUNZIONE.SingoliPezziFunzione[i].Substring(j, 2).ToUpper() == "X^")
                    {
                        FUNZIONE.esponenti[i] = int.Parse(FUNZIONE.SingoliPezziFunzione[i].Substring(j + 2, FUNZIONE.SingoliPezziFunzione[i].Length - (j + 2))); //prendiamo l'esponente quindi se la sottostringa è uguale a "x^" sappiamo che dovremo salvarci nell'array l'elezione
                        FUNZIONE.coefficenti[i] = int.Parse(FUNZIONE.SingoliPezziFunzione[i].Substring(0, j));
                        j = FUNZIONE.SingoliPezziFunzione[i].Length; //mettiamo questo così esce da for
                    }
                    else if (FUNZIONE.SingoliPezziFunzione[i].Substring(j, 2).ToUpper() != "X^") //se non è elevato con la "^" allora
                    {
                        FUNZIONE.esponenti[i] = 0; //nel dubbio poniamolo =0
                        if (FUNZIONE.SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X") //se invece è presente la x allora cambiamo l'esponete in 
                        {
                            FUNZIONE.esponenti[i] = 1;
                            if (j > 1)
                                FUNZIONE.coefficenti[i] = int.Parse(FUNZIONE.SingoliPezziFunzione[i].Substring(0, j));
                            j = FUNZIONE.SingoliPezziFunzione[i].Length;
                        }
                        else
                        {
                            if (j == FUNZIONE.SingoliPezziFunzione[i].Length - 2)
                                FUNZIONE.coefficenti[i] = int.Parse(FUNZIONE.SingoliPezziFunzione[i].Substring(0, FUNZIONE.SingoliPezziFunzione[i].Length - 1));
                        }
                    }
                }
            }
            
            for (int i = 0; i < 100; i++)
            {
                FUNZIONE.SingoliPezziFunzioneBackup[i] = FUNZIONE.SingoliPezziFunzione[i];
            }
            


            while (x<10)//troviamo le coordinate di alcuni punti appartenenti alla funzione
            {
                SommaBackup = "";
                for (int i = 0; i < 100; i++)
                {
                    FUNZIONE.SingoliPezziFunzione[i] = FUNZIONE.SingoliPezziFunzioneBackup[i]; //ripristina i valori originali
                }
                coordinata[0, x] = x;

                for (int i = 0; i < contatore1; i++)
                {
                    for (int j = 0; j < FUNZIONE.SingoliPezziFunzione[i].Length; j++)
                    {
                        if (FUNZIONE.SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X")// trova la x
                        {
                            FUNZIONE.SingoliPezziFunzione[i]=Regex.Replace(FUNZIONE.SingoliPezziFunzione[i], "[x,X,^]", string.Empty); //toglie la x e la sostituisce con *(il valore della x) per esponente volte
                            FUNZIONE.SingoliPezziFunzione[i] = FUNZIONE.SingoliPezziFunzione[i].Remove(j, 1);
                            for (int p = 0; p < FUNZIONE.esponenti[i]; p++)
                            {
                                FUNZIONE.SingoliPezziFunzione[i] += "*";
                                FUNZIONE.SingoliPezziFunzione[i] += x;
                            }
                        }
                    }
                }
                for (int i=0;i< contatore1; i++)
                {
                    SommaBackup += FUNZIONE.funzione.Substring(FUNZIONE.PezziFunzione[i], 1)+FUNZIONE.SingoliPezziFunzione[i];
                }
                Console.WriteLine(SommaBackup);
                DataTable dt = new DataTable();
                var yy = dt.Compute(SommaBackup, "");
                decimal y=Convert.ToDecimal(yy);
                coordinata[1, x]=y;

                x++;
            }

            for (int i = 0; i < 1; i++)
            {
                for(int j=0;j<10;j++)
                {
                    Console.WriteLine("x=" + coordinata[0, j] + " y=" + coordinata[1, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

