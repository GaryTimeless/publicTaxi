using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Benutzer
    {
        public static Benutzer player;

        public string Name;
        public double Kapital;
        public List<Unternehmen> TaxiUnternehmen;
        

        public Benutzer(string name)
        {
            this.Name = name;
            this.Kapital = 1000000;
            this.TaxiUnternehmen = new List<Unternehmen>();
        }


        public override string ToString()
        {
            if(TaxiUnternehmen.Count == 0 || TaxiUnternehmen == null)
            {
                return "\n\nBenutzerinformationen:\n\nName:\t\t" + this.Name + "\nKapital:\t" + MainClass.ZahlenAnzeigen(this.Kapital) + " Euro\nUnternehmen:\tSie haben noch keine Unternehmen";
            }
            int index = 0;
            string unternehmen = "";
            foreach(var a in Benutzer.player.TaxiUnternehmen)
            {
                index++;
                unternehmen += index + ". " +  a.Name + "\n\t\t";
            }
            return "\n\nBenutzerinformationen:\n\nName:\t\t" + this.Name + "\nKapital:\t" + MainClass.ZahlenAnzeigen(this.Kapital) + " Euro\nUnternehmen:\t" + unternehmen;
        }


        public void zeigeUnternehmen(Benutzer gründer)
        {
            if(gründer.TaxiUnternehmen.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nNoch keine Unternehmen gegruendet!");
                Console.ResetColor();
                return;
            }
            Console.WriteLine();
            for (int i = 0; i < gründer.TaxiUnternehmen.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + gründer.TaxiUnternehmen[i].Name);
            }
        }


        public int einkaufUnternehmen()
        {
            Console.WriteLine();
            Benutzer.player.zeigeUnternehmen(Benutzer.player);
            Console.Write("\nMit welchem Unternehmen wollen Sie ein Taxi kaufen?\t");

            string gewähltesUnternehmen = Console.ReadLine();
            Regex zahl = new Regex("^[1-9]+$");

            while(true)
            {
                if(zahl.IsMatch(gewähltesUnternehmen) && Convert.ToInt32(gewähltesUnternehmen) > 0 && Convert.ToInt32(gewähltesUnternehmen) <= Benutzer.player.TaxiUnternehmen.Count)
                {
                    Console.Write("\nSie haben das Unternehmen ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Benutzer.player.TaxiUnternehmen[Convert.ToInt32(gewähltesUnternehmen) - 1].Name);
                    Console.ResetColor();
                    Console.Write(" gewaehlt.\n");
                    Console.ReadKey();
                    Console.Clear();
                    return Convert.ToInt32(gewähltesUnternehmen) - 1;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nKeine gueltige Zahl!\nBitte eine Zahl eingeben:\t");
                Console.ResetColor();
                gewähltesUnternehmen = Console.ReadLine();
            }
        }
    }
}
