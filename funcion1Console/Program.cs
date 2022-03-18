using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace funcion1Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string funzione;
            int[] PezziFunzione = new int[100]; //salva posizione in cui si trova operatore
            string[] SingoliPezziFunzione = new string[100]; //singoli numeri
            int indice = 1, contatore1 = 0, aumento = 0;
            int[] coefficenti = new int[100]; //coefficenti delle x
            int[] esponenti = new int[100]; //esponenti delle x
            decimal[,] coordinata = new decimal[2,10];

            string ciao="cos(2+6)*3";
            //nt ooo = int.Parse(ciao);

            DataTable dt = new DataTable();
            var v = dt.Compute(ciao, "");

            Console.WriteLine(v);
            Console.ReadKey();

            for (int i = 0; i < coefficenti.Length; i++)//pongo tutti i possibili coefficenti pari a 1, quindi come inesistenti
            {
                esponenti[i] = 1;
                coefficenti[i] = 1;
            }

            funzione = Console.ReadLine();
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
                        esponenti[i] =0; //nel dubbio poniamolo =0
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
                                coefficenti[i] = int.Parse(SingoliPezziFunzione[i].Substring(0, SingoliPezziFunzione[i].Length-1));
                        }
                    }
                }
            }

            /*for (int i = 0; i < 10; i++)
            {

            }*/

            for (int i = 0; i < contatore1; i++) //stampa pezzo - esponente - coefficente
            {
                Console.WriteLine($"{SingoliPezziFunzione[i]} \t {esponenti[i]} \t {coefficenti[i]}");
            }
            Console.ReadKey();
        }
    }
}

