using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Taxi
    {
        public string Modell;
        public string Bauart;
        public string Händler;
        public int Baujahr;
        public double Kilometerstand;
        public double Preis;
        public Antriebsart Antrieb;
        public int Sitzplaetze;

        public Taxi(string modell, string händler, int baujahr, double kilometerstand, double preis, Antriebsart antrieb, int sitzplaetze, string bauart)
        {
            this.Modell = modell;
            this.Händler = händler;
            this.Baujahr = baujahr;
            this.Kilometerstand = kilometerstand;
            this.Preis = preis;
            this.Antrieb = antrieb;
            this.Sitzplaetze = sitzplaetze;
            this.Bauart = bauart;
        }
        

        public override string ToString()
        {
            return "Auto: " + this.Händler + " " + this.Modell;
        }


        public static string kompletteAusagbe(Taxi auto)
        {
            return "\n\nAuto:\t\t" + auto.Händler + " " + auto.Modell + "\nAntrieb:\t" + auto.Antrieb + "\nBauart:\t\t" + auto.Bauart + "\nSitzplaetze:\t" + auto.Sitzplaetze + "\nBaujahr:\t" + auto.Baujahr + "\nKilometerstand:\t" + auto.Kilometerstand + "\n\nPreis:\t" + MainClass.ZahlenAnzeigen(auto.Preis) + " Euro.";
        }


        public static string TaxiNameAusgabe(Taxi auto)
        {
            return auto.Händler + " " + auto.Modell;
        }


        public static Taxi Audi_A4_1 = new Taxi("A4 35 TFSI S tronic", "Audi", 2020, 0, 38550, Antriebsart.Audi_35TFSI_Stronic, 5, "Avant");
        public static Taxi Audi_A4_2 = new Taxi("A4 40 TDI S tronic", "Audi", 2020, 0, 43000, Antriebsart.Audi_40TDI_Stronic, 5, "Limousine");
        public static Taxi Audi_A6 = new Taxi("A6 50 TDI tiptronic", "Audi", 2020, 0, 63500, Antriebsart.Audi_50TDI_Tiptronic, 5, "Avant");
        public static Taxi Audi_Q7 = new Taxi("Q7 45 TDI quattro tiptronic", "Audi", 2020, 0, 71300, Antriebsart.Audi_45TDI_quattro_Tiptronic, 7, "SUV");
        public static Taxi Audi_A8 = new Taxi("A8 60 TFSI quattro tiptronic", "Audi", 2020, 0, 117159, Antriebsart.Audi_60TFSI_quattro_Tiptronic, 4, "Limousine");

        public static Taxi BMW_3er_1 = new Taxi("320i", "BMW", 2020, 0, 40850, Antriebsart.BMW_320i, 5, "Limousine");
        public static Taxi BMW_3er_2 = new Taxi("320d", "BMW", 2020, 0, 45000, Antriebsart.BMW_320d, 5, "Touring");
        public static Taxi BMW_X5 = new Taxi("X5 xDrive40i", "BMW", 2020, 0, 72300, Antriebsart.BMW_xDrive_40i, 5, "SUV");
        public static Taxi BMW_7er = new Taxi("750d", "BMW", 2020, 0, 118400, Antriebsart.BMW750d_xDrive, 4, "Limousine");
        public static Taxi BMW_X7 = new Taxi("X7 xDrive30d", "BMW", 2020, 0, 87000, Antriebsart.BMW_xDrive_30d, 7, "SUV");
        public static Taxi BMW_5er = new Taxi("520d", "BMW", 2020, 0, 53500, Antriebsart.BM520d, 5, "SUV");

        public static Taxi MercedesBenz_C1 = new Taxi("C 200", "Mercedes Benz", 2020, 0, 41953.45, Antriebsart.MercedesBenz_200, 5, "T-Modell");
        public static Taxi MercedesBenz_C2 = new Taxi("C 220d", "Mercedes Benz", 2020, 0, 45047.45, Antriebsart.MercedesBenz_200d, 5, "Limousine");
        public static Taxi MercedesBenz_E1 = new Taxi("E 300de", "Mercedes Benz", 2020, 0, 58429, Antriebsart.MercedesBenz_300de, 5, "T-Modell");
        public static Taxi MercedesBenz_E2 = new Taxi("E 200d", "Mercedes Benz", 2020, 0, 46112.50, Antriebsart.MercedesBenz_200d, 5, "Limousine");
        public static Taxi MercedesBenz_GLE = new Taxi("GLE 450 4MATIC", "Mercedes Benz", 2020, 0, 72649.50, Antriebsart.MercedesBenz_450_4MATIC, 5, "SUV");
        public static Taxi MercedesBenz_S = new Taxi("S 450 4MATIC", "Mercedes Benz", 2020, 0, 102857.65, Antriebsart.MercedesBenz_450_4MATIC, 4, "Limousine");

        
        public static int TaxinamenAusgabe(int unternehmen, int hersteller)
        {   
            //TODO Eingabe überprüfen
            string taxiAuswahl;
            switch(hersteller)
            {
                case 0:
                    for(int i = 0; i < Hersteller.Audi.Fuhrpark.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + Taxi.TaxiNameAusgabe(Hersteller.Audi.Fuhrpark[i]));
                    }
                    Console.Write("\n\nBitte waehlen Sie ein Modell aus:\t");
                    taxiAuswahl = Console.ReadLine();
                    Regex zahl = new Regex("^[0-9]+$");
                    while(true)
                    {
                        if(zahl.IsMatch(taxiAuswahl) && Convert.ToInt32(taxiAuswahl) > 0 && Convert.ToInt32(taxiAuswahl) <= Hersteller.Audi.Fuhrpark.Count)
                        {
                            return Convert.ToInt32(taxiAuswahl) - 1;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\nUngueltige Eingabe!\nBitte neues Modell waehlen:\t");
                        Console.ResetColor();
                        taxiAuswahl = Console.ReadLine();
                        //todo Console.Clear();
                    }
                case 1:
                    for (int i = 0; i < Hersteller.BMW.Fuhrpark.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + Taxi.TaxiNameAusgabe(Hersteller.BMW.Fuhrpark[i]));
                    }
                    Console.Write("\n\nBitte waehlen Sie ein Modell aus:\t");
                    taxiAuswahl = Console.ReadLine();
                    Regex zahl1 = new Regex("^[0-9]+$");
                    while (true)
                    {
                        if (zahl1.IsMatch(taxiAuswahl) && Convert.ToInt32(taxiAuswahl) > 0 && Convert.ToInt32(taxiAuswahl) <= Hersteller.BMW.Fuhrpark.Count)
                        {
                            return Convert.ToInt32(taxiAuswahl) - 1;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\nUngueltige Eingabe!\nBitte neues Modell waehlen:\t");
                        Console.ResetColor();
                        taxiAuswahl = Console.ReadLine();
                        //todo Console.Clear();
                    }
                case 2:
                    for (int i = 0; i < Hersteller.MercedesBenz.Fuhrpark.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + Taxi.TaxiNameAusgabe(Hersteller.MercedesBenz.Fuhrpark[i]));
                    }
                    Console.Write("\n\nBitte waehlen Sie ein Modell aus:\t");
                    taxiAuswahl = Console.ReadLine();
                    Regex zahl2 = new Regex("^[0-9]+$");
                    while (true)
                    {
                        if (zahl2.IsMatch(taxiAuswahl) && Convert.ToInt32(taxiAuswahl) > 0 && Convert.ToInt32(taxiAuswahl) <= Hersteller.BMW.Fuhrpark.Count)
                        {
                            return Convert.ToInt32(taxiAuswahl) - 1;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("\nUngueltige Eingabe!\nBitte neues Modell waehlen:\t");
                        Console.ResetColor();
                        taxiAuswahl = Console.ReadLine();
                        //Console.Clear();
                    }
                default:
                    TaxinamenAusgabe(unternehmen, hersteller);
                    return -1;
            }
        }


        public static void EinTaxiAnzeigen(int hersteller, int taxi)
        {
            switch (hersteller)
            {
                case 0:
                    if (Hersteller.HerstellerListe[hersteller] == "Audi")
                    {
                        Console.WriteLine(Taxi.kompletteAusagbe(Hersteller.Audi.Fuhrpark[taxi]));
                        Console.ReadKey();
                    }
                    break;
                case 1:
                    if (Hersteller.HerstellerListe[hersteller] == "BMW")
                    {
                        Console.WriteLine(Taxi.kompletteAusagbe(Hersteller.BMW.Fuhrpark[taxi]));
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    if (Hersteller.HerstellerListe[hersteller] == "Mercedes Benz")
                    {
                        Console.WriteLine(Taxi.kompletteAusagbe(Hersteller.MercedesBenz.Fuhrpark[taxi]));
                        Console.ReadKey();
                    }
                    break;
            }
        }


        public static void TaxiKauf(int unternehmen, int hersteller, int taxi) // TODO müssten hier in die Else nicht die aussgae reinkommen -> zu wenig Geld?
        {
            switch(hersteller)
            {
                case 0:
                    if(Benutzer.player.TaxiUnternehmen[unternehmen].Kapital >= Hersteller.Audi.Fuhrpark[taxi].Preis)
                    {
                        Benutzer.player.TaxiUnternehmen[unternehmen].Fuhrpark.Add(Hersteller.Audi.Fuhrpark[taxi]);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nSie haben das Taxi gekauft!");
                        Console.ResetColor();
                        Benutzer.player.TaxiUnternehmen[unternehmen].Kapital -= Hersteller.Audi.Fuhrpark[taxi].Preis;
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\nSie haben nicht genuegend Geld um sich das Taxi zu kaufen!");
                    Console.ResetColor();
                    return;
                case 1:
                    if (Benutzer.player.TaxiUnternehmen[unternehmen].Kapital >= Hersteller.BMW.Fuhrpark[taxi].Preis)
                    {
                        Benutzer.player.TaxiUnternehmen[unternehmen].Fuhrpark.Add(Hersteller.BMW.Fuhrpark[taxi]);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nSie haben das Taxi gekauft!");
                        Console.ResetColor();
                        Benutzer.player.TaxiUnternehmen[unternehmen].Kapital -= Hersteller.BMW.Fuhrpark[taxi].Preis;
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\nSie haben nicht genuegend Geld um sich das Taxi zu kaufen!");
                    Console.ResetColor();
                    return;
                case 2:
                    if (Benutzer.player.TaxiUnternehmen[unternehmen].Kapital >= Hersteller.MercedesBenz.Fuhrpark[taxi].Preis)
                    {
                        Benutzer.player.TaxiUnternehmen[unternehmen].Fuhrpark.Add(Hersteller.MercedesBenz.Fuhrpark[taxi]);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nSie haben das Taxi gekauft!");
                        Console.ResetColor();
                        Benutzer.player.TaxiUnternehmen[unternehmen].Kapital -= Hersteller.MercedesBenz.Fuhrpark[taxi].Preis;
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\nSie haben nicht genuegend Geld um sich das Taxi zu kaufen!");
                    Console.ResetColor();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\nKeine mögliche Wahl!\nBitte nochmal waehlen!");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    return;
            }
        }


        public static int TaxiWahl()
        {
            string userInput = "";

            Console.Write(@"
Wollen Sie dieses Taxi kaufen?
                                    (1) Ja
                                    (2) Zurueck zum Hauptmenue");
            Console.WriteLine("\n");
            userInput = Console.ReadLine();
            Regex moeglicherInput = new Regex("^[12]$");
            while (!moeglicherInput.IsMatch(userInput))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\nUngueltige Eingabe!\nBitte neuen Wert eingeben:\t");
                Console.ResetColor();
                userInput = Console.ReadLine();
            }
            return Convert.ToInt32(userInput);
        }
    }
}
