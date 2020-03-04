using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TaxiWorld
{
    internal class Program
    {
        public static void Intro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Yellow;
            string ausgabe = @"
  _________   _  __ ____   _       ______  ____  __    ____ 
 /_  __/   | | |/ //  _/  | |     / / __ \/ __ \/ /   / __ \
  / / / /| | |   / / /    | | /| / / / / / /_/ / /   / / / /
 / / / ___ |/   |_/ /     | |/ |/ / /_/ / _, _/ /___/ /_/ / 
/_/ /_/  |_/_/|_/___/     |__/|__/\____/_/ |_/_____/_____/  
                                                            
";
            foreach (var a in ausgabe)
            {
                Console.Write(a);
                Thread.Sleep(5);
            }
            Console.ResetColor();
            Console.WriteLine(@"

Waehlen Sie eine Option aus:
                                (1) Neues Spiel starten
                                (2) Spielstand laden
                                (3) Beenden");

            Regex einstelligeZahl = new Regex("^[123]$");
            string spielanfang = Console.ReadLine();
            while (!einstelligeZahl.IsMatch(spielanfang))
            {
                Console.Write("Ungueltige Eingabe! Bitte erneut eine Zahl eingeben:\t");
                Thread.Sleep(200);
                spielanfang = Console.ReadLine();
            }

            switch (Convert.ToInt32(spielanfang))
            {
                case 1:
                    NewGame();
                    break;
                case 2:
                    Console.Clear();
                    SuL.ladeDaten();
                    Console.WriteLine("\nSpielstand erfolgreich geladen.\nLade Hauptmenue \n");
                    Thread.Sleep(1000);
                    LoadScreen();
                    break;
                case 3:
                    EndGame();
                    break;
            }
        }


        public static void NewGame()
        {
            Console.Clear();
            Console.Write("Sie beginnen ein neues Spiel bei TAXI-WORLD!\nZunaechst brauchen Sie einen Benutzernamen:\t");
            string benutzername = Console.ReadLine();
            Regex leerzeichen = new Regex("^[ ]+$");
            while (benutzername == "" || benutzername == null || leerzeichen.IsMatch(benutzername))
            {
                Console.Write("\nUngueltige Eingabe, bitte neuen Namen eingeben:\t\t");
                benutzername = Console.ReadLine();
            }
            Benutzer.player = new Benutzer(benutzername);
            SuL.speicherDaten();
            //todo Console.WriteLine("\n\nIhr Benutzer wurde erstellt!");
            Console.WriteLine(Benutzer.player);
            Console.ReadKey();
        }


        public static string ZahlenAnzeigen(double zahl)
        {
            StringBuilder zahlen = new StringBuilder();
            List<char> zahlen2 = new List<char>();
            int komma = 0;
            foreach (var a in zahl.ToString())
            {
                zahlen.Append((char)a);
                zahlen2.Add((char)a);
            }

            if (zahlen2.Contains((char)','))
            {
                komma = zahlen2.Count - zahlen2.IndexOf(',');
            }

            for (int i = zahlen.Length - 3 - komma; i > 0; i -= 3)
            {
                zahlen.Insert(i, (char)'.');
            }
            return zahlen.ToString();
        }


        public static void LoadScreen()
        {
            string buffer = "Loading..."; //TODO Nur pünktchen sollen sich wiederholen
            for (int i = 0; i < 3; i++)
            {
                foreach (var a in buffer)
                {
                    Console.Write(a);
                    Thread.Sleep(60);
                }
                Console.WriteLine();
                Thread.Sleep(170);
            }
            Console.Clear();
        }


        public static void EndGame()
        {
            //TODO Speichern
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string ausgabe2 = @"
   _____       _      __   __                        __     __ 
  / ___/____  (_)__  / /  / /_  ___  ___  ____  ____/ /__  / /_
  \__ \/ __ \/ / _ \/ /  / __ \/ _ \/ _ \/ __ \/ __  / _ \/ __/
 ___/ / /_/ / /  __/ /  / /_/ /  __/  __/ / / / /_/ /  __/ /_  
/____/ .___/_/\___/_/  /_.___/\___/\___/_/ /_/\__,_/\___/\__/  
    /_/                                                       ";
            Console.WriteLine(ausgabe2);
            Console.ResetColor();
            Console.WriteLine("\n\n\n");
            Environment.Exit(0);
        }


        public static void Hauptmenü()
        {
            Console.Clear();
            Console.WriteLine(@"Hauptmenue:

            Was moechtest du tun?

                                Expandieren:                                                
                                (1) Gruende ein neues Unternehmen       
                                (2) Kauf ein neues Taxi             
                                (3) Besuche die Duebon-Bank

                                Analysieren:
                                (4) Zeige Spieler-Informationen
                                (5) Zeige alle Unternehmen
                                (6) Zeige bestimmtes Unternehmen

                                Tagesgeschaeft
                                (7) Taxi fahren lassen

                                BWL
                                (8) bla

                                Feierabend & Backup
                                (9) Spielstand Speichern
                                (10) Ende des Spiels");
            Regex menüMöglichkeiten = new Regex("^[1-9]$");
            string ersterSchritt = Console.ReadLine();

            //todo 10 muss noch in den Regex eingebunden werden/ in der Schleife überprüfen
            if (menüMöglichkeiten.IsMatch(ersterSchritt.ToString()))
            {
                switch (Convert.ToInt32(ersterSchritt))
                {
                    case 1:
                        Benutzer.player.TaxiUnternehmen.Add(Unternehmen.unternehmenGruenden(Benutzer.player));
                        Console.ReadKey();
                        //LoadScreen();
                        break;
                    case 2:
                        int entscheidungTaxi = 0;
                        if (Benutzer.player.TaxiUnternehmen.Count != 0)
                        {
                            int tmpUnternehmen = Benutzer.player.einkaufUnternehmen();
                            int tmpHersteller = Hersteller.HerstellerWahl();
                            int tmpTaxi = Taxi.TaxinamenAusgabe(tmpUnternehmen, tmpHersteller);
                            Taxi.EinTaxiAnzeigen(tmpHersteller, tmpTaxi);
                            entscheidungTaxi = Taxi.TaxiWahl();
                            switch (entscheidungTaxi)
                            {
                                case 1:
                                    Taxi.TaxiKauf(tmpUnternehmen, tmpHersteller, tmpTaxi);
                                    Console.ReadKey();
                                    LoadScreen();
                                    break;
                                case 2:
                                    LoadScreen();
                                    break;
                            }
                        }
                        if (Convert.ToInt32(entscheidungTaxi) == 1 || Convert.ToInt32(entscheidungTaxi) == 2)
                        {
                            //LoadScreen();
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nSie koennen noch kein Taxi kaufen!\nBitte erst ein Unternehmen gruenden.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;

                    case 3:
                        Bank.BesuchBank();
                        break;
                    case 4:
                        Console.WriteLine(Benutzer.player);
                        Console.ReadKey();
                        //LoadScreen();
                        break;
                    case 5:
                        Benutzer.player.zeigeUnternehmen(Benutzer.player);
                        Console.ReadKey();
                        //LoadScreen();
                        break;
                    case 6:
                        Benutzer.player.zeigeUnternehmen(Benutzer.player);
                        if (Benutzer.player.TaxiUnternehmen.Count != 0)
                        {
                            Console.Write("\n\nWelches Unternehmen wollen sie sehen?\t");
                            string unternehmen = Console.ReadLine();
                            Regex zahl = new Regex("^[0-9]+$");

                            while (true)
                            {
                                if (zahl.IsMatch(unternehmen) && Convert.ToInt32(unternehmen) <= Benutzer.player.TaxiUnternehmen.Count)
                                {
                                    Thread.Sleep(100);
                                    Console.Clear();
                                    Console.WriteLine(Benutzer.player.TaxiUnternehmen[Convert.ToInt32(unternehmen) - 1]);
                                    Console.ReadKey();
                                    LoadScreen();
                                    return;
                                }
                                Console.ForegroundColor = ConsoleColor.DarkRed; Console.ResetColor();
                                Console.Write("\n\nUngueltige Eingabe!\nBitte neuen Wert eingeben:\t");
                                Console.ResetColor();
                                unternehmen = Console.ReadLine();
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 7:
                        if (Benutzer.player.TaxiUnternehmen.Count != 0)
                        {
                            int AnzahlMonate = Intervall.IntervallLaenge();
                            for (int i = 0; i < AnzahlMonate; ++i)
                            {
                                Intervall.EinnahmenAusTaxiFahrten();
                                //hack spritkosten
                                //hackversicherungskosten?
                                Bank.EinzugVerbindlichkeiten();
                                //hack Personalkosten
                                //hack Gebäudekosten
                                Console.WriteLine();
                                string ausgabe = "Taxi faehrt...";
                                for (int j = 0; j < 3; j++)
                                {
                                    foreach (var a in ausgabe)
                                    {
                                        Console.Write(a);
                                        Thread.Sleep(50);
                                    }
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nSie koennen noch kein Umsatz generieren!\nBitte kaufen sie ein Taxi.");
                            Console.ReadKey();
                            Console.ResetColor();
                        }
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Waehlen Sie eine Option aus:\n");

                        Regex möglichkeiten = new Regex("^[1-7]$");
                        int n = 0;
                        foreach (var a in BWL.alleBla)
                        {
                            n++;
                            Console.WriteLine(n + ". " + a);
                        }
                        Console.Write("\n\nBitte waehlen Sie eine Funktion:\t");
                        string funktion = Console.ReadLine();
                        while (!möglichkeiten.IsMatch(funktion))
                        {
                            Console.Write("\nUngueltige Eingabe! Bitte erneut waehlen:\t");
                            funktion = Console.ReadLine();
                        }

                        try
                        {
                            double wert = Convert.ToInt32(funktion);
                        }
                        catch
                        {
                            Console.WriteLine("\n\nNicht moeglich!");
                            Thread.Sleep(200);
                            return;
                        }

                        switch (Convert.ToInt32(funktion))
                        {
                            //todo fehlerbehandlungen bei allen
                            case 1:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Zinssatz:\t\t");
                                double zinssatz = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Laufzeit:\t\t");
                                int laufzeit = Convert.ToInt32(Console.ReadLine());

                                double rbf = BWL.RBF(zinssatz, laufzeit);
                                Console.WriteLine("\n\nRBF:\t\t" + Program.ZahlenAnzeigen(rbf));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 2:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Investition:\t\t");
                                double investition = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Zinssatz:\t\t");
                                zinssatz = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Konstante Einnahmen:\t");
                                double c = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Laufzeit:\t\t");
                                laufzeit = Convert.ToInt32(Console.ReadLine());

                                double kw = BWL.Kapitalwert(investition, zinssatz, c, laufzeit);
                                Console.WriteLine("\n\nKapitalwert:\t\t" + Program.ZahlenAnzeigen(kw));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 3:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Konstante Einnahmen:\t");
                                c = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Zinssatz:\t\t");
                                zinssatz = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Laufzeit:\t\t");
                                laufzeit = Convert.ToInt32(Console.ReadLine());

                                double sb = BWL.Sparbetrag(c, zinssatz, laufzeit);
                                Console.WriteLine("\n\nKapitalwert:\t\t" + Program.ZahlenAnzeigen(sb));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 4:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Konstante Einnahmen:\t");
                                c = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Zinssatz:\t\t");
                                zinssatz = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Laufzeit:\t\t");
                                laufzeit = Convert.ToInt32(Console.ReadLine());

                                double js = BWL.JaehrlicheSparsumme(c, zinssatz, laufzeit);
                                Console.WriteLine("\n\nKapitalwert:\t\t" + Program.ZahlenAnzeigen(js));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 5:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Konstante Einnahmen:\t");
                                c = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Ausgabe:\t");
                                double ausgabe = Convert.ToDouble(Console.ReadLine());

                                double kwf1 = BWL.KWF1(c, ausgabe);
                                Console.WriteLine("\n\nKWF1:\t\t" + Program.ZahlenAnzeigen(kwf1));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 6:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Zinssatz:\t\t");
                                zinssatz = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Laufzeit:\t\t");
                                laufzeit = Convert.ToInt32(Console.ReadLine());

                                double kwf2 = BWL.KWF2(zinssatz, laufzeit);
                                Console.WriteLine("\n\nKWF2:\t\t" + Program.ZahlenAnzeigen(kwf2));
                                Console.ReadKey();
                                LoadScreen();
                                break;

                            case 7:
                                Console.Write("\nSie haben ");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write(BWL.alleBla[Convert.ToInt32(funktion) - 1]);
                                Console.ResetColor();
                                Console.WriteLine(" gewaehlt!");

                                Console.WriteLine("\nBitte geben Sie ein.\n");
                                Console.Write("Konstante Einnahmen:\t");
                                c = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Ausgabe:\t");
                                ausgabe = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Zinssatz:\t\t");
                                zinssatz = Convert.ToDouble(Console.ReadLine());

                                double ad = BWL.Amortisationsdauer(c, ausgabe, zinssatz);
                                Console.WriteLine("\n\nAmortisationsdauer:\t\t" + Program.ZahlenAnzeigen(ad));
                                Console.ReadKey();
                                LoadScreen();
                                break;
                        }
                        break;
                    case 9:
                        SuL.speicherDaten();
                        break;
                    case 10:
                        EndGame();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nUngueltige Eingabe! Bitte nochmal ausfuehren.");
                Console.ResetColor();
                Thread.Sleep(1000);
                Hauptmenü();
            }

        }


        public static void Main()
        {
            Hersteller.FuhrparkGenerieren();
            Intro();

            //hack SuL.ALLHASHINT(); Just for the Hashnumbers
            while (true)
            {
                Hauptmenü();
            }
        }
    }
}