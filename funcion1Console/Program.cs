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
        static void Main(string[] args)
        {
            string funzione, SommaBackup="";
            int[] PezziFunzione = new int[100]; //salva posizione in cui si trova operatore
            string[] SingoliPezziFunzione = new string[100]; //singoli numeri
            int indice = 1, contatore1 = 0, aumento = 0;
            int[] coefficenti = new int[100]; //coefficenti delle x
            int[] esponenti = new int[100]; //esponenti delle x
            decimal[,] coordinata = new decimal[2, 10];
            int x = 1;

            string[] SingoliPezziFunzioneBackup = new string[100];

            string ciao = "2^2+6*3";

            //DataTable dt = new DataTable();
            //var v = dt.Compute(ciao, "");
            //Console.WriteLine(v);
            //Console.ReadKey();

            for (int i = 0; i < coefficenti.Length; i++)//pongo tutti i possibili coefficenti pari a 1, quindi come inesistenti
            {
                esponenti[i] = 1;
                coefficenti[i] = 1;
            }
            funzione =  " ";
            funzione += Console.ReadLine();
            PezziFunzione[0] = 0;
            for (int i = 0; i < funzione.Length; i++) //qui salviamo in un array le posizioni in cui stanno gli operatori
            {
                if (funzione.Substring(i, 1) == "+" || funzione.Substring(i, 1) == "-" || funzione.Substring(i, 1) == "*" || funzione.Substring(i, 1) == "/")
                {
                    PezziFunzione[indice] = i; //qui ci sono operatori
                    indice++; //quantità di operatori nella funzione
                }
            }
            PezziFunzione[indice] = funzione.Length;
            /*for (int i = 0; i < indice; i++)
            {
                Console.WriteLine(PezziFunzione[i]);
            }*/
            Console.ReadKey();
            while (contatore1 < indice) //mi salvo in un array di stringe ogni sottostringa in cui ho suddiviso la mia funzione
            {
                if (contatore1 > 0)
                    aumento = 1;
                //SingoliPezziFunzione[contatore1]=" ";
                SingoliPezziFunzione[contatore1] /*+*/= funzione.Substring(PezziFunzione[contatore1] + aumento, (PezziFunzione[contatore1 + 1] - PezziFunzione[contatore1] - aumento));
                SingoliPezziFunzione[contatore1] += " ";
                contatore1++;
            }

            for (int i = 0; i < contatore1; i++)
            {
                for (int j = 0; j < SingoliPezziFunzione[i].Length - 1; j++) //cerco l'incognita x in ogni pezzo di funzione
                {
                    if (SingoliPezziFunzione[i].Substring(j, 2).ToUpper() == "X^")
                    {
                        esponenti[i] = int.Parse(SingoliPezziFunzione[i].Substring(j + 2, SingoliPezziFunzione[i].Length - (j + 2))); //prendiamo l'esponente quindi se la sottostringa è uguale a "x^" sappiamo che dovremo salvarci nell'array l'elezione
                        coefficenti[i] = int.Parse(SingoliPezziFunzione[i].Substring(0, j));
                        j = SingoliPezziFunzione[i].Length; //mettiamo questo così esce da for
                    }
                    else if (SingoliPezziFunzione[i].Substring(j, 2).ToUpper() != "X^") //se non è elevato con la "^" allora
                    {
                        esponenti[i] = 0; //nel dubbio poniamolo =0
                        //Console.WriteLine("Ciao"+i);
                        //Console.ReadKey();
                        if (SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X") //se invece è presente la x allora cambiamo l'esponete in 
                        {
                            esponenti[i] = 1;
                            if (j > 1)
                                coefficenti[i] = int.Parse(SingoliPezziFunzione[i].Substring(0, j));
                            j = SingoliPezziFunzione[i].Length;
                        }
                        else
                        {
                            if (j == SingoliPezziFunzione[i].Length - 2)
                                coefficenti[i] = int.Parse(SingoliPezziFunzione[i].Substring(0, SingoliPezziFunzione[i].Length - 1));
                        }
                    }
                }
            }
            
            SingoliPezziFunzioneBackup=SingoliPezziFunzione;


            while (x<10)//troviamo le coordinate di alcuni punti appartenenti alla funzione
            {
                SommaBackup = "";
                SingoliPezziFunzione = SingoliPezziFunzioneBackup;//ripristina pezzi originari
                coordinata[0, x] = x;

                for (int i = 0; i < /*SingoliPezziFunzione.Length*/contatore1; i++)
                {
                    for (int j = 0; j < SingoliPezziFunzione[i].Length; j++)
                    {
                        if (SingoliPezziFunzione[i].Substring(j, 1).ToUpper() == "X")// trova la x
                        {
                            SingoliPezziFunzione[i]=Regex.Replace(SingoliPezziFunzione[i], "[x,X,^]", string.Empty); //toglie la x e la sostituisce con *(il valore della x) per esponente volte
                            SingoliPezziFunzione[i] = SingoliPezziFunzione[i].Remove(j, 1);
                            for (int p = 0; p < esponenti[i]; p++)
                            {
                                SingoliPezziFunzione[i] += $"*{x}";
                            }
                        }
                    }
                }
                for (int i=0;i< /*SingoliPezziFunzione.Length*/contatore1; i++)
                {
                    SommaBackup += funzione.Substring(PezziFunzione[i], 1)+SingoliPezziFunzione[i];
                }

                Console.WriteLine(SommaBackup);
                Console.ReadKey();

                DataTable dt = new DataTable();
                var yy = dt.Compute(SommaBackup, "");
                decimal y=Convert.ToDecimal(yy);
                coordinata[1, x]=y;

                x++;
            }
            

            for (int i = 0; i < contatore1; i++) //stampa pezzo - esponente - coefficente
            {
                Console.WriteLine($"{SingoliPezziFunzione[i]} \t {esponenti[i]} \t {coefficenti[i]}");
            }
            Console.ReadKey();
        }
    }
}

