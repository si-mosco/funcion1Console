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
            public string[] TondeAperte;
            public string[] TondeChiuse;
            public int[,] InizioFineTonde;
            public string[] TondeAperteChiuse;
        }

        static void Main(string[] args)
        {

            SuddivisioneFunzione FUNZIONE;
            int indice = 1;
            double[,] coordinata = new double[2, 1000];

            FUNZIONE.SingoliPezziFunzione = new string[100];
            FUNZIONE.PezziFunzione = new int[100];
            FUNZIONE.esponenti = new int[100];
            FUNZIONE.SingoliPezziFunzioneBackup = new string[100];
            FUNZIONE.coefficenti = new int[100];
            FUNZIONE.esponenti = new int[100];
            FUNZIONE.TondeAperte = new string[100];
            FUNZIONE.TondeChiuse = new string[100];
            FUNZIONE.InizioFineTonde = new int[2, 100];
            FUNZIONE.TondeAperteChiuse = new string[100];


            for (int i = 0; i < FUNZIONE.coefficenti.Length; i++)//pongo tutti i possibili coefficenti pari a 1, quindi come inesistenti
            {
                FUNZIONE.esponenti[i] = 1;
                FUNZIONE.coefficenti[i] = 1;
            }

            FUNZIONE.funzione = " "; //aggiungo lo spazio all'inizio
            FUNZIONE.funzione += Console.ReadLine(); //prendo in input

            FUNZIONE.funzione = DenominatoreParentesi(FUNZIONE); //aggiungo le parentesi al denominatore nel caso in cu non ci siano
            Console.WriteLine($"FUNZIONE:{FUNZIONE.funzione}");

            FUNZIONE.funzione = Parentesi(FUNZIONE);

            Console.WriteLine("FUNZIONE" + FUNZIONE.funzione);

            IndividuazioneOperatori(FUNZIONE, ref indice);

            SuddivisioneSottostringhe(FUNZIONE, indice);

            for (int i = 0; i < indice; i++)
            {
                Console.WriteLine(FUNZIONE.SingoliPezziFunzione[i]);
            }
            //Console.ReadKey();

            IndividuazioneCoefficentiEsponenti(FUNZIONE, indice);

            for (int i = 0; i < 100; i++)
            {
                FUNZIONE.SingoliPezziFunzioneBackup[i] = FUNZIONE.SingoliPezziFunzione[i];
            }

            IndividuazioneCoordinate(FUNZIONE, indice, coordinata);


            for (int i = 0; i < 1; i++)//stampa le coordinate
            {
                for (int j = 0; j <= 100; j++)
                {
                    Console.WriteLine("x=" + coordinata[0, j] + "\t \t  y=" + coordinata[1, j]);
                }
                Console.WriteLine();
            }
        }

        public static void IndividuazioneOperatori(SuddivisioneFunzione Funzione, ref int Indice)//trovo la posizione degli operatori
        {
            Funzione.PezziFunzione[0] = 0; //assegno al primo pezzo del vettore la posizione 0
            Console.WriteLine("NUOVA POSIZIONE:" + Funzione.funzione.Length);
            for (int i = 0; i < Funzione.funzione.Length; i++) //qui salviamo in un array le posizioni in cui stanno gli operatori
            {
                if (Funzione.funzione.Substring(i, 1) == "+" || Funzione.funzione.Substring(i, 1) == "-" || Funzione.funzione.Substring(i, 1) == "*" || Funzione.funzione.Substring(i, 1) == "/")
                {
                    Funzione.PezziFunzione[Indice] = i; //qui ci sono operatori
                    Indice++; //quantità di operatori nella funzione
                }
            }
            Funzione.PezziFunzione[Indice] = Funzione.funzione.Length;
            //Indice++;
        }

        public static void SuddivisioneSottostringhe(SuddivisioneFunzione Funzione, int Indice)
        {
            //Console.WriteLine("ORA CHE ENTRO QUI È PARI A:"+Funzione.funzione);
            int contatore = 0, aumento = 0, indicatore = 0;
            while (contatore < Indice) //mi salvo in un array di stringe ogni sottostringa in cui ho suddiviso la mia funzione
            {
                if (contatore > 0)
                    aumento = 1;
                Funzione.SingoliPezziFunzione[contatore] = Funzione.funzione.Substring(Funzione.PezziFunzione[contatore] + aumento, (Funzione.PezziFunzione[contatore + 1] - Funzione.PezziFunzione[contatore] - aumento));
                //qui uso la variabile aumento perchè sennò nella sottostringa ci sarebbe acnhe l'operatore
                Funzione.SingoliPezziFunzione[contatore] += " ";

                for (int j = 0; j < Funzione.SingoliPezziFunzione[contatore].Length; j++)
                {
                    if (Funzione.SingoliPezziFunzione[contatore].Substring(j, 1).ToUpper() == "(") //se trova una tonda aperta la salva in un array in cui mi segno con l'indice la poszione in cui si trova la tonda
                    {
                        Funzione.InizioFineTonde[0, indicatore] = contatore;
                        Funzione.TondeAperte[contatore] = "(";
                        Funzione.SingoliPezziFunzione[contatore] = Funzione.SingoliPezziFunzione[contatore].Remove(j, 1);
                    }
                    else if (Funzione.SingoliPezziFunzione[contatore].Substring(j, 1).ToUpper() == ")")
                    {
                        Funzione.InizioFineTonde[1, indicatore] = contatore;
                        Funzione.TondeChiuse[contatore] = ")";
                        Funzione.SingoliPezziFunzione[contatore] = Funzione.SingoliPezziFunzione[contatore].Remove(j, 1);
                        indicatore++;
                    }
                }

                contatore++;
            }
        }

        public static void IndividuazioneCoefficentiEsponenti(SuddivisioneFunzione Funzione, int Indice)//trovo i coefficenti e gli esponenti
        {
            for (int i = 0; i < Indice; i++)
            {
                for (int j = 0; j < Funzione.SingoliPezziFunzione[i].Length - 1; j++) //cerco l'incognita x in ogni pezzo di funzione
                {
                    if (Funzione.SingoliPezziFunzione[i].Substring(j, 2).ToUpper() == "X^")//parte di x^
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
                        else if (Funzione.SingoliPezziFunzione[i].Substring(j, 1) == "^")
                        {
                            //Funzione.coefficenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(0, j));
                            Funzione.esponenti[i] = int.Parse(Funzione.SingoliPezziFunzione[i].Substring(j + 1, Funzione.SingoliPezziFunzione[i].Length - (j + 1)));
                            j = Funzione.SingoliPezziFunzione[i].Length;
                        }
                    }
                }
            }
        }

        public static void IndividuazioneCoordinate(SuddivisioneFunzione Funzione, int Indice, double[,] MatriceCoordinate)//trovo i coefficenti e gli esponenti
        {
            double x = -50;
            double y;
            int contatore = 0;
            string SommaBackup = "";
            while (contatore < 100)//troviamo le coordinate di alcuni punti appartenenti alla funzione
            {
                SommaBackup = "";
                for (int i = 0; i < 100; i++) //quando lo rifai con la x diversa te la ripristina
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
                        else if (j < Funzione.SingoliPezziFunzione[i].Length - 1 && Funzione.SingoliPezziFunzione[i].Substring(j + 1, 1) == "^" && Funzione.TondeChiuse[i] != ")")
                        {
                            Funzione.SingoliPezziFunzione[i] = Regex.Replace(Funzione.SingoliPezziFunzione[i], "[m,^]", string.Empty);//stesso procedimento di prima per elevazione di una cifra normale
                            Funzione.SingoliPezziFunzione[i] = Funzione.SingoliPezziFunzione[i].Remove(j + 1, 1);
                            string SingoloPezzoFunzione = Funzione.SingoliPezziFunzione[i]; //salviamo in una variabile di backup il numero da continuare a elevare
                            for (int p = 0; p < Funzione.esponenti[i] - 1; p++)
                            {
                                Funzione.SingoliPezziFunzione[i] += "*";
                                Funzione.SingoliPezziFunzione[i] += SingoloPezzoFunzione;
                            }

                        }
                        else if (j < Funzione.SingoliPezziFunzione[i].Length - 1 && Funzione.SingoliPezziFunzione[i].Substring(j + 1, 1) == "^" && Funzione.TondeChiuse[i] == ")")
                        {
                            Funzione.SingoliPezziFunzione[i] = Regex.Replace(Funzione.SingoliPezziFunzione[i], "[m,^]", string.Empty);
                            Funzione.SingoliPezziFunzione[i] = Funzione.SingoliPezziFunzione[i].Remove(j + 1, 1);

                        }
                    }
                }

                ElevazioneTonde(Funzione, Indice);

                for (int i = 0; i < Indice; i++)
                {
                    SommaBackup += Funzione.funzione.Substring(Funzione.PezziFunzione[i], 1) + Funzione.TondeAperte[i] + Funzione.SingoliPezziFunzione[i] + Funzione.TondeChiuse[i] + Funzione.TondeAperteChiuse[i];
                }
                Console.WriteLine("SOMMA:" + SommaBackup);
                y = Risoluzione(SommaBackup);
                MatriceCoordinate[1, contatore] = y;

                contatore++;
                x += 1;
            }
        }

        public static double Risoluzione(string Funzione)//funzione per risolvere una espressione
        {
            DataTable dt = new DataTable();
            var soluzione1 = dt.Compute(Funzione, "");
            double soluzione2 = Convert.ToDouble(soluzione1);
            return soluzione2;
        }

        public static void ElevazioneTonde(SuddivisioneFunzione Funzione, int Indice)
        {
            int f = 0;
            while (f < Indice - 3)
            {
                string parentesi = "("; //inizializo la stringa con "("
                int indiceImportante = 0, controllo = 0, AggiungiSegno = 0;
                while (Funzione.TondeChiuse[f] != ")" && f < Indice - 3) //se non trovo ")" e finchè resto minore delle posizioni occupate
                {
                    if (Funzione.TondeAperte[f] == "(") //prima controllo se trovo la tonda perchè se non la trovo non ha senso inserire nella stringa di salvataggio
                    {
                        controllo = 1;
                    }

                    if (controllo == 1)
                    {
                        if (AggiungiSegno < 1)
                            parentesi += Funzione.SingoliPezziFunzione[f];
                        else
                        {
                            parentesi += Funzione.funzione.Substring(Funzione.PezziFunzione[f], 1); //È SBAGLIATO QUESTO COMANDO E MI HA FATTO DANNARE
                            parentesi += Funzione.SingoliPezziFunzione[f];

                        }
                    }
                    AggiungiSegno++;
                    f++;
                }
                parentesi += Funzione.funzione.Substring(Funzione.PezziFunzione[f], 1); //devo aggiungere ancora il segno
                parentesi += Funzione.SingoliPezziFunzione[f];
                indiceImportante = f;
                f++;
                f++;//
                f++;//
                parentesi += ")";
                Funzione.TondeAperteChiuse[indiceImportante] = "";
                Console.WriteLine("PARENTESI:" + parentesi);
                for (int u = 0; u < Funzione.esponenti[indiceImportante] - 1; u++)
                {
                    Funzione.TondeAperteChiuse[indiceImportante] += "*";
                    Funzione.TondeAperteChiuse[indiceImportante] += parentesi;
                }
            }
        }

        public static string Parentesi(SuddivisioneFunzione Funzione) //aggiungiamo un +0 prima della chiusera di ogni parentesi
        {
            string RisFin = Funzione.funzione;
            for (int i = 0; i < Funzione.funzione.Length; i++)
            {
                if (Funzione.funzione.Substring(i, 1) == ")")
                {
                    RisFin = Funzione.funzione.Insert(i, "+0");
                    i += 2;
                    Funzione.funzione = RisFin;
                }
            }
            return RisFin;
        }

        public static string DenominatoreParentesi(SuddivisioneFunzione Funzione) //aggiungiamo un +0 prima della chiusera di ogni parentesi
        {
            string RisFin = Funzione.funzione;
            Console.WriteLine("VECCHIA LUNGHEZZA DENOMINATORE PARENTESI:" + Funzione.funzione.Length);
            for (int i = 0; i < Funzione.funzione.Length - 1; i++)
            {
                if (Funzione.funzione.Substring(i, 1) == "/" && Funzione.funzione.Substring(i + 1, 1) != "(") //se c'è denominatore e poi non c'è una tonda
                {
                    RisFin = Funzione.funzione.Insert(i + 1, "(");
                    int k = 0;
                    for (k = i + 1; k < Funzione.funzione.Length - 1; k++)
                    {
                        Funzione.funzione = RisFin;
                        RisFin = Funzione.funzione;
                        if (Funzione.funzione.Substring(k, 1) == "/" || Funzione.funzione.Substring(k, 1) == "+" ||
                            Funzione.funzione.Substring(k, 1) == "-" || Funzione.funzione.Substring(k, 1) == "*")
                        {
                            Console.WriteLine($"K:{k} FUNZIONE:{Funzione.funzione}, VALORE:{Funzione.funzione.Substring(k, 1)}");
                            RisFin = Funzione.funzione.Insert(k, ")");
                            k = Funzione.funzione.Length + 1; //esc subito dalla funzione
                        }
                    }
                    if (k != Funzione.funzione.Length + 2)
                    {
                        Funzione.funzione = RisFin;
                        RisFin = Funzione.funzione;
                        RisFin = Funzione.funzione.Insert(Funzione.funzione.Length, ")");
                    }
                    i += 2;
                    Funzione.funzione = RisFin;
                }
            }
            return RisFin;
        }

    }
}

