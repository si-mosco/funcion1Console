using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace funcion1Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string funzione;
            int[] PezziFunzione = new int[100];
            string[] SingoliPezziFunzione = new string[100];
            int indice = 1, contatore1=0,aumento=0;
            bool[] x = new bool[100];
            bool[] x2 = new bool[100];
            string[] elevazioni= new string[100];

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
                SingoliPezziFunzione[contatore1] = funzione.Substring(PezziFunzione[contatore1]+aumento, (PezziFunzione[contatore1 + 1] - PezziFunzione[contatore1]-aumento));
                contatore1++;
            }
            
            for (int i=0; i<SingoliPezziFunzione.Length; i++)//per ogni parola, confronto ogni lettera se uguale a x (presente=vero| non presente=falso)
            {
                for (int j=0; j<SingoliPezziFunzione[i].Length; j++)
                {
                    if (SingoliPezziFunzione[i].Substring(j,1).ToUpper()=="X")
                    {
                        x[i]=true;
                    }
                }
                for (int j=0; j<SingoliPezziFunzione[i].Length-1; j++)
                {
                    if (SingoliPezziFunzione[i].Substring(j, 2).ToUpper() == "X^")
                    {
                        x2[i] = true;
                        elevazioni[i]=SingoliPezziFunzione[i].Substring(j, (PezziFunzione[i + 1] - PezziFunzione[i] - 1));
                        Console.WriteLine("elevazione"+ i+1);
                        Console.WriteLine(elevazioni[i]);
                    }
                }
            }

            for (int i=0; i<10; i++)
            {
                Console.WriteLine(SingoliPezziFunzione[i]);
            }
            Console.ReadKey();
        }
    }
}
