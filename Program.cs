using System;
using System.Threading;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Nyomj egy gombot a kezdéshez!");
            //Változó deklarálások
            var input = Console.ReadKey();
            Console.Clear();
            int gyorsulas = 1;
            int sebesseg = 1000;
            var autoSzin = ConsoleColor.Blue;
            var fuSzin = ConsoleColor.Green;
            var utSzin = ConsoleColor.DarkGray;
            var semmiSzin = ConsoleColor.Black;
            int szelesseg = 30;
            int magassag = 20;
            int utSzelesseg = 2;
            int utKezdes = szelesseg/2-utSzelesseg/2;
            int autoX = 2;
            int autoY = utKezdes + utSzelesseg / 2;
            long pontszam = 0;
            int[] segedSor = new int[szelesseg];
            Random rand = new Random();

            char[,] palya = new char[magassag, szelesseg];
            char[,] palyaSeged = new char[magassag, szelesseg];

            //Sorok Összeragasztáas
            for (int i = 0; i < magassag; i++)
            {
                //Sor megrajzolása
                for (int l = 0; l < szelesseg; l++)
                {
                    for (int f = 0; f < utKezdes; f++) { 
                        palya[i, l] = 'f';      // p=pálya, a=autó, f=fű
                        l++;
                    }
                    for (int f = 0; f < utSzelesseg; f++) {
                        palya[i, l] = 'p';      // p=pálya, a=autó, f=fű
                        l++;
                    }
                    for (int f = 0; f < szelesseg-utKezdes-utSzelesseg; f++) {
                        palya[i, l] = 'f';      // p=pálya, a=autó, f=fű
                        l++;
                    }

                    //Út mozgatása
                    utKezdes += rand.Next(-1, 2);
                    if (utKezdes > szelesseg)
                        utKezdes--;
                    else if (utKezdes < 1)
                        utKezdes++;

                }
            }
            
            //Játékciklus
            while (true)
            {
                if (Console.KeyAvailable == true)
                {
                    input = Console.ReadKey();
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            autoY--;
                            break;
                        case ConsoleKey.RightArrow:
                            autoY++;
                            break;
                    }
                }

                for (int i = 0; i < magassag; i++)
                {
                    for (int l = 0; l < szelesseg; l++)
                    {
                        //Színek megrajzolása
                        if (i == autoX && l == autoY)
                        {
                            Console.ForegroundColor = autoSzin;
                            Console.BackgroundColor = autoSzin;
                            if (palya[i, l] == 'f')
                            {
                                Console.Clear();
                                Environment.Exit(0);
                            }
                        }
                        else if (palya[i, l] == 'p')
                        {
                            Console.ForegroundColor = utSzin;
                            Console.BackgroundColor = utSzin;
                        }
                        else if (palya[i, l] == 'f')
                        {
                            Console.ForegroundColor = fuSzin;
                            Console.BackgroundColor = fuSzin;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        Console.Write('*');
                        //Új sor megkezdése
                        if (l == szelesseg - 1) {
                        Console.ForegroundColor = semmiSzin;
                        Console.BackgroundColor = semmiSzin;
                        Console.Write("\n");
                    }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"Score: {pontszam}");

                //Pály segédilstájának feltöltése 1-el eltolva és az utolsó sort random feltöltve
                for (int i = 1; i < magassag; i++)
                {
                    for (int l = 0; l < szelesseg; l++)
                    {
                        palyaSeged[i - 1, l] = palya[i, l];
                    }

                    for (int l = 0; l < szelesseg; l+=0)
                    {
                        for (int f = 0; f < utKezdes; f++)
                        {
                            palyaSeged[magassag - 1, l] = 'f';      // p=pálya, a=autó, f=fű
                            l++;
                        }
                        for (int f = 0; f < utSzelesseg; f++)
                        {
                             palyaSeged[magassag - 1, l] = 'p';      // p=pálya, a=autó, f=fű
                             l++;

                        }
                        for (int f = 0; f < szelesseg - utKezdes - utSzelesseg; f++)
                        {
                            palyaSeged[magassag - 1, l] = 'f';      // p=pálya, a=autó, f=fű
                            l++;
                        }


                    }

                }
                //Út mozgatása
                utKezdes += rand.Next(-1, 2);
                if (utKezdes + utSzelesseg > szelesseg)
                    utKezdes = szelesseg - utSzelesseg;
                else if (utKezdes < 0)
                    utKezdes = 0;

                palya = palyaSeged;

                
                sebesseg -= gyorsulas;
                if (sebesseg <= 1){
                    sebesseg = 1;
                }
                pontszam += 1000;
                //Késleltetés és tisztítás
                Thread.Sleep(sebesseg);
                Console.Clear();
            }
        }
    }
}