using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class SuL // speichern und Laden
    {
        public static void speicherDaten()
        {
            StreamWriter speicherSpielerDaten = null; // safety first :D

            // not sure if this is correct
            try
            {
                // erstelle & öffne SpielerDatei
                speicherSpielerDaten = new StreamWriter(Benutzer.player.Name + ".txt"); 

                //Schreibe Zeile für Zeile SpielerDaten
                speicherSpielerDaten.WriteLine(Benutzer.player.Kapital);
                speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen.Count);

                // schleife für alle möglichen Unternehmen & deren eigene Fuhrpärke
                for (int i = 0; i < Benutzer.player.TaxiUnternehmen.Count; ++i)
                {
                    // schreibe Zeile für Zeile UnternehmensDaten
                    speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].Name);
                    speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].Kapital);
                    speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].Verbindlichkeiten);
                    speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].montlKredit);
                    speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].Fuhrpark.Count);

                    // schreibe Zeile für Zeile UnternehmensFuhrparkDaten
                    for (int p = 0; p < Benutzer.player.TaxiUnternehmen[i].Fuhrpark.Count; ++p)
                    {
                        speicherSpielerDaten.WriteLine(Benutzer.player.TaxiUnternehmen[i].Fuhrpark[p].ToString());
                    }

                }

                speicherSpielerDaten.Close();
            }
            catch
            {
                Console.WriteLine("Es ist ein fehler aufgetreten!");
            }
            finally
            {
                if (speicherSpielerDaten != null)
                {
                    speicherSpielerDaten.Close();
                }

            }
            //speicherSpielerDaten.Close(); // safety first //MGF Ist doch nicht notwendig wegen der if-Schleife, ist auch nicht gut dann trotzdem zu schließen

            Console.WriteLine("\nErfolgreich gesichert!\n");
            MainClass.LoadScreen();
        }


        public static void ladeDaten()
        {
            StreamReader LadeSpielerDaten = null;

            Console.Write("Bitte tippe deinen Namen ein:\t");
            string BenutzerName = Console.ReadLine();
            Benutzer.player = new Benutzer(BenutzerName);
            int Exception = 0;
            try
            {
                LadeSpielerDaten = new StreamReader(Benutzer.player.Name + ".txt");

                Benutzer.player.Kapital = Convert.ToInt32(LadeSpielerDaten.ReadLine());
                int AnzahlUnternehmen = Convert.ToInt32(LadeSpielerDaten.ReadLine());
                Exception = 1;
                for (int i = 0; i < AnzahlUnternehmen; ++i)
                {
                    // müssen wir Mal genau durchdenken, ob dass so Sinn ergibt...
                    Unternehmen neuesUnternehmen = new Unternehmen("", Benutzer.player, 0);
                    Benutzer.player.TaxiUnternehmen.Add(neuesUnternehmen);

                    // Lade Zeile für Zeile UnternehmensDaten
                    Benutzer.player.TaxiUnternehmen[i].Name = LadeSpielerDaten.ReadLine();
                    Benutzer.player.TaxiUnternehmen[i].Kapital = Convert.ToInt32(LadeSpielerDaten.ReadLine());
                    Benutzer.player.TaxiUnternehmen[i].Verbindlichkeiten = Convert.ToInt32(LadeSpielerDaten.ReadLine());
                    Benutzer.player.TaxiUnternehmen[i].montlKredit = Convert.ToInt32(LadeSpielerDaten.ReadLine());
                    int AnzahlTaxis = Convert.ToInt32(LadeSpielerDaten.ReadLine());

                    for (int p = 0; p < AnzahlTaxis; ++p)
                    {
                        string tmp = LadeSpielerDaten.ReadLine();
                        ImportHashSetTaxi(tmp,i,p);
                    }
                }
                LadeSpielerDaten.Close();
            }
            catch
            {   if (Exception == 0)
                {
                    Console.WriteLine("Es ist ein fehler aufgetreten! \nDie Datei die sie suchen existiert nicht.\n\n DAS SPIEL WIRD BEENDET");
                    Console.ReadKey();
                    MainClass.LoadScreen();
                    MainClass.EndGame();
                }else if(Exception== 1)
                Console.WriteLine("\nEs ist ein fehler aufgetreten!\n\nIhre Datei ist beschaedigt.\n\n\nDas Spiel wird beendet!");
                MainClass.LoadScreen();
                MainClass.EndGame();
            }
            finally
            {
                if (LadeSpielerDaten != null)
                {
                    LadeSpielerDaten.Close();
                }
            }
            //LadeSpielerDaten.Close(); // safetyfirst;
        }

        // Import String, Hash Number, Set Taxi
        public static void ImportHashSetTaxi(string Taxi, int IndexTaxiUnternehmensListe, int IndexTaxiUnternehmensFuhrparkliste)
        {
            Taxi[] TaxiHASHTABELLE = new Taxi[3400];
            // problem bei
            //Auto: Mercedes Benz C 220 = 2165
            //Auto: Mercedes Benz E 200d = 2165


            TaxiHASHTABELLE[1063] = Hersteller.BMW.Fuhrpark[0];
            TaxiHASHTABELLE[1058] = TaxiProjekt.Taxi.BMW_3er_2;
            TaxiHASHTABELLE[1813] = Hersteller.BMW.Fuhrpark[2];
            TaxiHASHTABELLE[1065] = Hersteller.BMW.Fuhrpark[3];
            TaxiHASHTABELLE[1809] = Hersteller.BMW.Fuhrpark[4];
            TaxiHASHTABELLE[1060] = Hersteller.BMW.Fuhrpark[5];
            TaxiHASHTABELLE[2063] = Hersteller.MercedesBenz.Fuhrpark[0];
            TaxiHASHTABELLE[2165] = Hersteller.MercedesBenz.Fuhrpark[1];
            TaxiHASHTABELLE[2267] = Hersteller.MercedesBenz.Fuhrpark[2];
            TaxiHASHTABELLE[2669] = Hersteller.MercedesBenz.Fuhrpark[4];
            TaxiHASHTABELLE[2536] = Hersteller.MercedesBenz.Fuhrpark[5];
            TaxiHASHTABELLE[2363] = Hersteller.Audi.Fuhrpark[0];
            TaxiHASHTABELLE[2274] = Hersteller.Audi.Fuhrpark[1];
            TaxiHASHTABELLE[2495] = Hersteller.Audi.Fuhrpark[2];
            TaxiHASHTABELLE[3332] = Hersteller.Audi.Fuhrpark[3];
            TaxiHASHTABELLE[3399] = Hersteller.Audi.Fuhrpark[4];

            
            char Hash = '0';

            for (int k =0; k< Taxi.Length;++k)
            {
                char tmp = Taxi[k];
                Hash += tmp;
            }
            
            int HashtableNumber = Hash;
           // Console.WriteLine(TaxiHASHTABELLE[HashtableNumber]);
            //Console.WriteLine(Hersteller.BMW.Fuhrpark[0]); GIBT FEHLER AUS

            // behindert uns hier der ToString wert?
            Benutzer.player.TaxiUnternehmen[IndexTaxiUnternehmensListe].Fuhrpark.Add(TaxiHASHTABELLE[HashtableNumber]);
        }

        // just for HashTable
        public static void ALLHASHINT()
        {
            List<Taxi> ALLTAXIS = new List<Taxi>();
            List<int> AllTaxiHashint = new List<int>();

            foreach (var a in Hersteller.Audi.Fuhrpark)
            {
                ALLTAXIS.Add(a);
            }

            foreach (var b in Hersteller.BMW.Fuhrpark)
            {
                ALLTAXIS.Add(b);
            }

            foreach (var c in Hersteller.MercedesBenz.Fuhrpark)
            {
                ALLTAXIS.Add(c);
            }

            char Hash = '0';

            foreach (var t in ALLTAXIS)
            {
                string Taxi = t.ToString();
                for (int i = 0; i < Taxi.Length; ++i)
                {
                    char tmp = Taxi[i];
                    Hash += tmp;
                }
                int HashTableInt = Hash;
                Console.WriteLine(Taxi + ", zugehoerige int = " + HashTableInt);
                AllTaxiHashint.Add(HashTableInt);
                Hash = '0';
            }

            AllTaxiHashint.Sort();

            Console.WriteLine("Nun sortiert?!");
            foreach (int a in AllTaxiHashint)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }

    }
}
