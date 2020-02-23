using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TaxiProjekt
{
    public class MainClass
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
            foreach(var a in ausgabe)
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
            //Console.WriteLine("\n\nIhr Benutzer wurde erstellt!");
            Console.WriteLine(Benutzer.player);
            Console.ReadKey();
        }


        public static string ZahlenAnzeigen(double zahl)
        {
            StringBuilder zahlen = new StringBuilder();
            List<char> zahlen2 = new List<char>();
            int komma = 0;
            foreach(var a in zahl.ToString())
            {
                zahlen.Append((char)a);
                zahlen2.Add((char)a);
            }

            if(zahlen2.Contains((char)','))
            {
                komma = zahlen2.Count - zahlen2.IndexOf(',');
            }

            for(int i = zahlen.Length-3-komma; i > 0; i-=3)
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
            //            Console.WriteLine(@"Hauptmenue:

            //Was moechtest du tun?

            //Expandieren:                         Analysieren:                       Tagesgeschäft
            //(1) Gruende ein neues Unternehmen   (4) Zeige Spieler-Informationen     (7) Taxi fahren lassen
            //(2) Kauf ein neues Taxi             (5) Zeige alle Unternehmen
            //(3) Besuche die Duebon-Bank         (6) Zeige bestimmtes Unternehme

            //Feierabend & Backup
            //(8) Spielstand Speichern
            //(9) Ende des Spiels");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"Hauptmenue:

        Was moechtest du tun?

                            Expandieren:                                                
                            (1) Gruende ein neues Unternehmen       
                            (2) Kauf ein neues Taxi             
                            (3) Besuche die Duebon-Bank");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
                            Analysieren:
                            (4) Zeige Spieler-Informationen
                            (5) Zeige alle Unternehmen
                            (6) Zeige bestimmtes Unternehmen");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
                            Tagesgeschäft
                            (7) Taxi fahren lassen");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(@"

                            Feierabend & Backup
                            (8) Spielstand Speichern
                            (9) Ende des Spiels");
            Regex menüMöglichkeiten = new Regex("^[1-9]$"); // hier stand zuvor [12345678]
            string ersterSchritt = Console.ReadLine();

            if (menüMöglichkeiten.IsMatch(ersterSchritt.ToString()))
            {
                switch (Convert.ToInt32(ersterSchritt))
                {
                    case 1:
                        Benutzer.player.TaxiUnternehmen.Add(Unternehmen.unternehmenGruenden(Benutzer.player));
                        Console.ReadKey();
                        LoadScreen();
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
                                    break;
                                case 2:
                                    break;
                            }
                        }
                        if (Convert.ToInt32(entscheidungTaxi) == 1 || Convert.ToInt32(entscheidungTaxi) == 2)
                        {
                            LoadScreen();
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nSie koennen noch kein Taxi kaufen!\nBitte erst ein Unternehmen gruenden.");
                        Console.ResetColor();
                        Console.ReadKey();
                        LoadScreen();
                        break;

                    case 3:
                        Bank.BesuchBank();
                        break;
                    case 4:
                        Console.WriteLine(Benutzer.player);
                        Console.ReadKey();
                        LoadScreen();
                        break;
                    case 5:
                        Benutzer.player.zeigeUnternehmen(Benutzer.player);
                        Console.ReadKey();
                        LoadScreen();
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
                                if(zahl.IsMatch(unternehmen) && Convert.ToInt32(unternehmen) <= Benutzer.player.TaxiUnternehmen.Count)
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
                                //spritkosten
                                //versicherungskosten?
                                Bank.EinzugVerbindlichkeiten();
                                //Personalkosten
                                //Gebäudekosten

                            }
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nSie koennen noch kein Umsatz generieren!\nBitte kaufen sie ein Taxi.");
                            Console.ResetColor();
                        }
                        break;
                    case 8:
                        SuL.speicherDaten();
                        break;
                    case 9:
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
           
            //SuL.ALLHASHINT(); Just for the Hashnumbers
            while (true)
            {
                Hauptmenü();
            }
        }
    }
}
