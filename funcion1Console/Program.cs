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
            int indice = 1, contatore1=0;

            funzione = Console.ReadLine();
            PezziFunzione[0] = 0;
            for (int i = 0; i < funzione.Length; i++) //qui salviamo in un array le posizioni in cui stanno gli operatori
            {
                if (funzione.Substring(i, 1) == "+" || funzione.Substring(i, 1) == "-" || funzione.Substring(i, 1) == "*" || funzione.Substring(i, 1) == "/")
                {
                    PezziFunzione[indice] = i;
                    indice++; //quantità di operatori nella funzione
                }
            }
            while (contatore1 <= indice) //mi salvo in un array di stringe ogni sottostringa in cui ho suddiviso la mia funzione
            {
                SingoliPezziFunzione[contatore1] = funzione.Substring(PezziFunzione[contatore1], (PezziFunzione[contatore1 + 1] - PezziFunzione[contatore1]));
                contatore1++;
            }

            for (int i=0; i<100; i++)
            {
                Console.WriteLine(SingoliPezziFunzione[i]);
            }
            Console.ReadKey();
        }
    }
}
