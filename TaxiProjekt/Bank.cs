using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Bank
    {
        static Regex EinsZwei = new Regex("^[12]$");
        static Regex zahlen = new Regex("^[0-9]+$");

        //Konstruktor - if needed
        public Bank()
        {

        }

        
        public static void BesuchBank()
        {
            int AnzahlUnternehmen = Benutzer.player.TaxiUnternehmen.Count;
            if (AnzahlUnternehmen == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Da Sie noch kein Unternehmen haben, kann ich Ihnen noch kein Kredit für ein Taxi geben.");
                Console.ResetColor();
                Console.ReadKey();
                MainClass.LoadScreen();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(@"
              __           __                   ____  ___    _   ____ __
         ____/ /_  _____  / /_  ____  ____     / __ )/   |  / | / / //_/
        / __  / / / / _ \/ __ \/ __ \/ __ \   / __  / /| | /  |/ / ,<   
       / /_/ / /_/ /  __/ /_/ / /_/ / / / /  / /_/ / ___ |/ /|  / /| |  
       \__,_/\__,_/\___/_.___/\____/_/ /_/  /_____/_/  |_/_/ |_/_/ |_|  ");
                Console.ResetColor();
                Console.WriteLine("\n\n");
                Console.WriteLine(@"
Herzlich Willkommen bei der Duebon-Bank, mein Name ist Karl Duebon und ich bin Ihr persoenlicher Berater.

Womit kann ich Ihnen behilflich sein?

                                            (1) Kredit fuer den Kauf eines Taxis
                                            (2) Kreit fuer die Gruendung eines Unternehmens");
                Console.Write("\n"+"Bitte waehlen: ");


                // Not sure if this do what i want it to do :D
                int Kreditauswahl = 0;
                while (!EinsZwei.IsMatch(Kreditauswahl.ToString()))
                {
                    try
                    {
                        
                        Kreditauswahl = Convert.ToInt32(Console.ReadLine());

                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Bitte eine vorgegebene Zahl eingeben:\t");
                        Console.ResetColor();
                    }
                }


                if (Kreditauswahl == 1)
                {
                    Bank.Taxikredit();

                }
                if (Kreditauswahl == 2)
                {
                    Console.WriteLine("Hier beginnt die Methode zum Unternehmensgruendungsmethode");
                    Console.ReadKey();
                }
            }

        }

        public static void Taxikredit()
        {
            Console.Clear();
            // Mögliche Kriterien: Aktuelles Einkommen, zu erwartendes Einkommen, Eigenkapital, Monatliche Ausgaben, anderweitige Verbindlichkeiten, Schufaeintrag, ausstehende Forderungen, Grundstücke, Umlaufvermögen ... BILANZ ??
            Console.WriteLine("Herr Duebon:\tEs freut mich, dass sie sich fuer unsere Bank entschieden haben.");
            Console.ReadKey();
            Console.WriteLine("Herr Duebon:\tICH BIN BEGEISTERT");
            Console.WriteLine("Herr Duebon:\tWir werden Sie bestmoeglich beraten und unetrstuetzen wo es Sinn ergibt.");
            Console.ReadKey();
            Console.WriteLine("Herr Duebon:\tZunaechst interessiert mich, was sie sich vorgestellt haben, wie hoch der Kredit sein soll.");
            Console.Write("Kreditsumme:\t");
             
            int Kredithöhe = -1;
            while (!zahlen.IsMatch(Kredithöhe.ToString()))
            {
                try
                {
                    
                    Kredithöhe = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Bitte eine vorgegebene Zahl eingeben:\t");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Herr Duebon:\tHerrvoragend. Aus ihren Unterlagen kann ich sehen, dass sie: ");
            Console.WriteLine(Benutzer.player.TaxiUnternehmen.Count + " Unternehmen besitzen \n" + "");

            Console.ReadKey();
            Console.WriteLine("Herr Duebon:\tMit Welchem Unternehmen moechten sie einen Kredit aufnehmen?");
            
            Benutzer.player.zeigeUnternehmen(Benutzer.player);
            Console.Write("Unternehmen Nr.:\t");
            int UnternehmensKapital = Convert.ToInt32(Console.ReadLine());

            int AnzahlTaxis = 0;
            bool Verbindlichkeiten = false;
            for (int i = 0; i < Benutzer.player.TaxiUnternehmen.Count; ++i)
            {
                for (int p = 0; p < Benutzer.player.TaxiUnternehmen[i].Fuhrpark.Count; ++p)
                {
                    if (Benutzer.player.TaxiUnternehmen[i].Fuhrpark.Count == 0)
                    {
                        // es gibt kein Auto - > schleife Nutzlos
                    }
                    else
                    {
                        Console.WriteLine(p + 1 + ")" + Benutzer.player.TaxiUnternehmen[i].Fuhrpark[p].Modell);
                    }

                    // check Verbindlichkeiten
                    if (Benutzer.player.TaxiUnternehmen[i].Verbindlichkeiten > 0)
                    {
                        Verbindlichkeiten = true;
                    }
                }
            }
            Console.WriteLine("Herr Duebon:\tDes Weiteren lese ich, dass sie " + AnzahlTaxis + " Fahzeuge besitzen\n");
            Console.ReadKey();


            bool montlEinkommen = false;
            if (AnzahlTaxis > 0)
            {
                montlEinkommen = true;
                Console.WriteLine("Somit ist es entschieden, wieviel einnahmen sie monatlich generien.");
                // hier muss dann Benutzer.Player.Taxiunternehmen[X]. Bilanz xor montl Einahme xor jährliche Einnahmen...
            }



            Console.WriteLine("Herr Duebon:\tZu guter Letzt moechte ich noch wissen, wieviel Geld sie bereit sind selbst zu bezahlen.");
            Console.Write("Anzahlung: ");



            int Anzahlung = -1;
            while (!zahlen.IsMatch(Anzahlung.ToString()))
            {
                try
                {

                     Anzahlung = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Bitte eine vorgegebene Zahl eingeben:\t");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n"+"Herr Duebon:\tBestens. Nun haben wir alle relevanten Daten. Ich vergleiche diese nun mit unserer Datenbank um ihren Zins auszurechnen...");
            Console.ReadKey();

            KredizZins(Kredithöhe, Benutzer.player.TaxiUnternehmen.Count, AnzahlTaxis, Anzahlung, montlEinkommen, Verbindlichkeiten, UnternehmensKapital - 1);
            //jetzt kommt Methode zur Bestimmung des Zinssatzes so dass der User auch versch.Zeiten bestimmen kann.

        }

        public static void KredizZins(int kredithöhe, int unternehmensanzahl, int fahrzeuganzahl, int eigenkapital, bool montlEinkommen, bool verbindlichkeiten, int TaxiUnternehmenAuswahl)
        {
            bool HoherZins = true; // false => niedriger Zins
            double Zins = 0.05;

            // hoher Zins |1 ==  unternehmensanzahl > 2| niedriger Zins
            // hoher Zins |2 > FahrzeugAnzahl = 0| niedriger Zins
            // hoher Zins | Eigenkapital < Kreditsumme/2 < Eigenkapital| niedriger Zins
            // hoher Zins | nein -  montlEinkommen  - ja| niedriger Zins
            // hoher Zins |nein -  Verbindlichkeiten  - ja| niedriger Zins

            if (unternehmensanzahl > 1)
            { HoherZins = false; }
            else { HoherZins = true; }

            if (fahrzeuganzahl == 0)
            { HoherZins = false; }
            else { HoherZins = true; }

            if (kredithöhe / 2 > eigenkapital)
            { HoherZins = false; }
            else { HoherZins = true; }

            if (montlEinkommen == true)
            { HoherZins = false; }
            else { HoherZins = true; }

            if (verbindlichkeiten == true)
            { HoherZins = false; }
            else { HoherZins = true; }

            Console.WriteLine("Ihre Daten werden berechnet...");
            MainClass.LoadScreen();

            if (HoherZins == false)
            {
                Zins = 0.02;

            }

            Console.WriteLine("Ihre Kreditwuerdigkeit wurde bestaetigt. Sie erhalten ihre Wunschsumme zu folgenden Konditionen:");
            Console.WriteLine("NettoKreditbetrag:\t" + (kredithöhe-eigenkapital) + "\n");
            Console.WriteLine("Zins:\t" + Zins + "\n");


            Console.WriteLine("Kreditsumme:\t" + (kredithöhe-eigenkapital)*Math.Pow(1+Zins,3) + "\n");
            Console.WriteLine("Laufzeit:\t36 Monate \n");
            Console.WriteLine("montl. Aufwand:\t" + (kredithöhe - eigenkapital) * Math.Pow(1 + Zins, 3) / 36 + "\n");

            Console.WriteLine(@"
Herr Duebon:    Sind Sie damit einverstanden?
                                                (1) Ja
                                                (2) Nein");
            int Annahme = Convert.ToInt32(Console.ReadLine());

            if (Annahme == 1)
            {
                Console.WriteLine("Herr Duebon:\tAusgezeichnet. Ich bereite alles vor, in wenigen Sekunden verfuegen Sie ueber das Geld");
                MainClass.LoadScreen();
                Benutzer.player.TaxiUnternehmen[TaxiUnternehmenAuswahl].Kapital += kredithöhe;
                Benutzer.player.TaxiUnternehmen[TaxiUnternehmenAuswahl].Verbindlichkeiten += (kredithöhe - eigenkapital) * Math.Pow(1 + Zins, 3);
                Benutzer.player.TaxiUnternehmen[TaxiUnternehmenAuswahl].montlKredit += (kredithöhe - eigenkapital) * Math.Pow(1 + Zins, 3) / 36;

                // TODO montl. Aufwand verbuchens
            }
            else
            {
                Console.WriteLine("Herr Duebon:\tWie sie moechten. Beehren Sie uns bald wieder. Bid bald.");
                MainClass.LoadScreen();
            }
        }

        public static void EinzugVerbindlichkeiten()
        {
            foreach (var TaxiUnternehmen in Benutzer.player.TaxiUnternehmen)
            {
                TaxiUnternehmen.Kapital -= TaxiUnternehmen.montlKredit;
            }
        }
    }
}
