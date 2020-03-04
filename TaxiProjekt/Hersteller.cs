using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Hersteller
    {
        public List<Taxi> Fuhrpark;

        public static Hersteller Audi = new Hersteller();
        public static Hersteller BMW = new Hersteller();
        public static Hersteller MercedesBenz = new Hersteller();

        public static List<string> HerstellerListe = new List<string> { "Audi", "BMW", "Mercedes Benz" };
        
        public Hersteller()
        {
            this.Fuhrpark = new List<Taxi>();
        }


        public static void FuhrparkGenerieren()
        {
            Audi.Fuhrpark.Add(Taxi.Audi_A4_1);
            Audi.Fuhrpark.Add(Taxi.Audi_A4_2);
            Audi.Fuhrpark.Add(Taxi.Audi_A6);
            Audi.Fuhrpark.Add(Taxi.Audi_Q7);
            Audi.Fuhrpark.Add(Taxi.Audi_A8);

            BMW.Fuhrpark.Add(Taxi.BMW_3er_1);
            BMW.Fuhrpark.Add(Taxi.BMW_3er_2);
            BMW.Fuhrpark.Add(Taxi.BMW_X5);
            BMW.Fuhrpark.Add(Taxi.BMW_7er);
            BMW.Fuhrpark.Add(Taxi.BMW_X7);
            BMW.Fuhrpark.Add(Taxi.BMW_5er);

            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_C1);
            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_C2);
            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_E1);
            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_E2);
            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_GLE);
            MercedesBenz.Fuhrpark.Add(Taxi.MercedesBenz_S);
        }


        public static int HerstellerWahl()
        {
            Console.Write("\n\nBitte waehlen Sie einen Hersteller:\n\n");
            for (int i = 0; i < HerstellerListe.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + HerstellerListe[i]);
            }
            Console.WriteLine("\n");
            string auswahlUnternehmen = Console.ReadLine();
            Regex zahl = new Regex("^[0-9]+$");

            while(true)
            {
                if(zahl.IsMatch(auswahlUnternehmen) && Convert.ToInt32(auswahlUnternehmen) > 0 && Convert.ToInt32(auswahlUnternehmen) <= HerstellerListe.Count)
                {
                    Console.Write("\n\nSie haben den Hersteller ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Hersteller.HerstellerListe[Convert.ToInt32(auswahlUnternehmen) - 1]);
                    Console.ResetColor();
                    Console.Write(" gewaehlt.\n");
                    Console.ReadKey();
                    Console.Clear();
                    return Convert.ToInt32(auswahlUnternehmen) - 1;
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Diesen Hersteller gibt es noch nicht!\nBitte waehlen Sie einen neuen:\t");
                Console.ResetColor();
                auswahlUnternehmen = Console.ReadLine();
            }
        }
    }
}
