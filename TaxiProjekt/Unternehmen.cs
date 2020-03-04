using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Unternehmen
    {
        public string Name;
        public Benutzer Gründer;
        public double Kapital;
        public List<Taxi> Fuhrpark;
        public double Verbindlichkeiten;
        public double montlKredit;


        public Unternehmen(string name, Benutzer gründer, double kapital)
        {
            this.Name = name;
            this.Gründer = gründer;
            this.Kapital = kapital;
            this.Fuhrpark = new List<Taxi>();
        }


        public override string ToString()
        {
            if(this.Fuhrpark.Count == 0)
            {
                return "Unternehmen:\t\t" + this.Name + "\nGruender:\t\t" + this.Gründer.Name + "\nVerbindlichkeiten:\t" + MainClass.ZahlenAnzeigen(this.Verbindlichkeiten) + "\nKapital:\t\t" + MainClass.ZahlenAnzeigen(this.Kapital) + " Euro\nFuhrpark:\t\tNoch keine Taxis gekauft";
            }
            string autos = "";
            foreach(var a in this.Fuhrpark)
            {
                autos += a.Händler + " " + a.Modell + "\t";
            }
            return "Unternehmen:\t\t" + this.Name + "\nGruender:\t\t" + this.Gründer.Name + "\nVerbindlichkeiten:\t" + MainClass.ZahlenAnzeigen(this.Verbindlichkeiten) + "\nKapital:\t\t" + MainClass.ZahlenAnzeigen(this.Kapital) + " Euro\nFuhrpark:\t\t" + autos;
        }


        public static Unternehmen unternehmenGruenden(Benutzer gründer)
        {
            Console.Write("\nWie soll ihr Unternehmen heissen:\t");
            string companyName = Console.ReadLine();
            List<string> unternehmensNamen = new List<string>();
            foreach(var a in gründer.TaxiUnternehmen)
            {
                unternehmensNamen.Add(a.Name);
            }

            while(true)
            {
                if(companyName != "" && companyName != null && !unternehmensNamen.Contains(companyName))
                {
                    Console.Write("\n\nWie viel Geld wollen Sie in das Unternehmen einlegen?\t");
                    string input = Console.ReadLine();
                    Regex kommaZahlen = new Regex("^[0-9.]+$");

                    while (!kommaZahlen.IsMatch(input))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\n\nUngueltige Eingabe!\nBitte neuen Wert eingeben:\t");
                        Console.ResetColor();
                        input = Console.ReadLine();
                    }

                    double einlage = Convert.ToDouble(input);

                    while (einlage > gründer.Kapital)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Sie haben nicht so viel Kapital!\nBitte passenden Betrag eingeben:\t");
                        Console.ResetColor();
                        einlage = Convert.ToDouble(Console.ReadLine());
                    }

                    gründer.Kapital -= einlage;

                    Unternehmen neuesUnternehmen = new Unternehmen(companyName, gründer, einlage);
                    Console.Clear();
                    Console.WriteLine("Das Unternehmen wurde gegruendet!\n\n");
                    Console.WriteLine(neuesUnternehmen + "\n\n");

                    return neuesUnternehmen;
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\n\nUngueltige Eingabe!\nBitte erneut eingeben:\t");
                Console.ResetColor();
                companyName = Console.ReadLine();
            }
        }




        
        

    }



}
