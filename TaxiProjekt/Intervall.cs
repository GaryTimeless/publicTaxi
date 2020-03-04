using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    
    public class Intervall
    {
        static Regex EinsbisDrei = new Regex("^[123]$");
        static Random rand = new Random();
         
        
        //hack evtl entscheiden verschiedene Werte über Anzahl Taxifahrt:
        //                          können Taxis Leveln?
        //hack welche kosten fallen an?
        //hack sprit kosten, Reinigungskosten, Reperaturkosten/Wartungskosten

        public static int IntervallLaenge()
        {
            Console.WriteLine("Bitte waehlen Sie aus, wie lange das Intervall sein soll:");
            Console.WriteLine(@"
                                (1) 1 Monat
                                (2) 3 Monate
                                (3) 6 Monate");
            Console.Write("Option: ");

            int AnzahlMonate = -1;
            while (!EinsbisDrei.IsMatch(AnzahlMonate.ToString()))
            {
                try
                {
                    AnzahlMonate = Convert.ToInt32(Console.ReadLine());
                    if(AnzahlMonate == 2 || AnzahlMonate == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nLeider noch nicht verfuegbar :(");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\nBitte eine vorgegebene Zahl eingeben:\t");
                    Console.ResetColor();
                }
            }

            return AnzahlMonate;
        }


        public static void EinnahmenAusTaxiFahrten()
        {
            foreach (var TaxiUnternehmen in Benutzer.player.TaxiUnternehmen)
            {
                foreach (var TaxiAuto in TaxiUnternehmen.Fuhrpark)
                {
                    int MaxFahrten = 80; //hack 5 am Tag, 5 Tage die Woche, 4 Wochen im Monat
                    double Preiszone1EuroProKm = 0.80;
                    double Preiszone2EuroProKm = 0.50;
                    double Preiszone3EuroProKm = 0.30;

                    //hack Anzahl Fahrten in versch. Preiszonen
                    int FahrtenInPreisZone1 = rand.Next(1, MaxFahrten); //hack PreisZone 1 max KM radius -> 1-10KM
                    MaxFahrten -= FahrtenInPreisZone1;
                    int FahrtenInPreisZone2 = rand.Next(1, MaxFahrten);//hack PreisZone 2 max KM radius -> 10-30KM
                    MaxFahrten -= FahrtenInPreisZone2;
                    int FahrtenInPreisZone3 = rand.Next(1, MaxFahrten);//hack PreisZone 3 max KM radius -> 30-100KM

                    //hack Umsatz der Fahrten in PZX und insgesamt
                    double UmsatzPZ1 = 0;
                    double UmsatzPZ2 = 0;
                    double UmsatzPZ3 = 0;



                    //hack Gefahrene KM pro PZ, insgesamt
                    double gesamtKm = 0;
                    double gesamtKmPZ1 = 0;

                    List<int> gefahreneKminPZ1 = new List<int>(); //hack ist in so fern praktisch, da man mit Count und index aus der Liste viel herauslesen kann. Ist aber auch doppelt gemoppelt
                    for (int i = 0; i < FahrtenInPreisZone1; ++i)
                    {
                        int tmp = rand.Next(1, 10);
                        gefahreneKminPZ1.Add(tmp);
                        gesamtKm += tmp;
                        gesamtKmPZ1 += tmp;
                        UmsatzPZ1 += (tmp * Preiszone1EuroProKm);
                    }

                    double gesamtKmPZ2 = 0;
                    List<int> gefahreneKminPZ2 = new List<int>();
                    for (int i = 0; i < FahrtenInPreisZone1; ++i)
                    {
                        int tmp = rand.Next(10, 30);
                        gefahreneKminPZ2.Add(tmp);
                        gesamtKm += tmp;
                        gesamtKmPZ2 += tmp;
                        UmsatzPZ2 += tmp * Preiszone2EuroProKm;

                    }

                    double gesamtKmPZ3 = 0;
                    List<int> gefahreneKminPZ3 = new List<int>();
                    for (int i = 0; i < FahrtenInPreisZone1; ++i)
                    {
                        int tmp = rand.Next(30, 100);
                        gefahreneKminPZ2.Add(tmp);
                        gesamtKm += tmp;
                        gesamtKmPZ3 += tmp;
                        UmsatzPZ3 += tmp * Preiszone3EuroProKm;
                    }
                    double gesamtUmsatzProMonat = (UmsatzPZ1 + UmsatzPZ2 + UmsatzPZ3);


                    //hack Ausgabe der Anzahl Fahrten in versch. Preiszonen vorerst für codeZwecke

                    //Console.WriteLine("aktuelles Taxi: "+TaxiAuto.Modell);
                    //Console.WriteLine("Anzahl Fahrten PZ1 :"+FahrtenInPreisZone1.ToString());
                    //Console.WriteLine("Anzahl Fahrten PZ2 :"+FahrtenInPreisZone2.ToString());
                    //Console.WriteLine("Anzahl Fahrten PZ3 :"+FahrtenInPreisZone3.ToString());
                    //Console.WriteLine("Umsatz PZ 1: "+UmsatzPZ1.ToString());
                    //Console.WriteLine("Umsatz PZ 2: "+UmsatzPZ2.ToString());
                    //Console.WriteLine("Umsatz PZ 3: " + UmsatzPZ3.ToString());
                    //Console.WriteLine("\ngesamt Umsatz: " + gesamtUmsatzProMonat.ToString());

                    //Console.WriteLine("gefahrene KM PZ 1: " + gesamtKmPZ1.ToString());
                    //Console.WriteLine("gefahrene KM PZ 2: " + gesamtKmPZ2.ToString());
                    //Console.WriteLine("gefahrene KM PZ 3: " + gesamtKmPZ3.ToString());
                    //Console.WriteLine("gefahrene KM insg. "+ gesamtKm.ToString());
                    //Console.WriteLine("gefahrene KM Ueberpruefen: " + (gesamtKmPZ1 + gesamtKmPZ2 + gesamtKmPZ3));


                    Console.ReadKey();


                    TaxiUnternehmen.Kapital += gesamtUmsatzProMonat;
                    TaxiAuto.Kilometerstand += gesamtKm;


                    //hack alle Daten müssen nun sauber irgendwo gespeichert werden. Auto X hat im Monat MM.JJ : -> UmsazuPZ1
                    //                                                                                       -> UmsatzPZ2
                    //                                                                                       -> UmsatzPZ3
                    //                                                                                       -> GesamtUmsatz
                    //                                                                                       -> gefahreneKM


                    // hack Versuch einen Lifeticker zu basteln

                    //Idee: Liveticker geht 20 sec. und zeigt teilweise Fahrten an. Alternativ müssten es 80 sein... denke das wäre zu viel und sinnlos
                    //      es geht vorerst ja nur um Unterhaltung (emotion)
                    // dennoch sollen die angegebenen Daten echt sein.
                    // folgendes TExt layout



                    //TaxiUnternehmen: NAME
                    //Taxi: Modell
                    //Taxifahrer? - wenn Personal kommt
                    //Fahrt innerhalb PZ(X)
                    // Strecke: (XX) KM
                    // Einnahmen: (XX.xx) €


                    // oder :

                    // Taxifahrer(x) fährt mit seiem Taxi(x) ein/e Geschlecht(X,Y,Z) (XX) KM und rechnet (XX,xx)€ ab.
                    
                    



                }
            }
        }
        //public static string RandomGeschlecht()
        //{
        //    int i = rand.Next(1, 2);
        //    string geschlecht = "";
        //    switch (i)
        //    {
        //        case 1:
        //            geschlecht = "einen Mann";
        //            break;
        //        case 2:
        //            geschlecht = "eine Frau";
        //            break;
        //    }
        //    return geschlecht;
        //}
        //public static void AusgabeLiveticker(int AnzahlFahrten, Taxi aktuellesTaxi, int einzelneFahrtKm, int )
        //{

        //    int ausgabeLiveticker = rand.Next(1, 3);



        //    switch (ausgabeLiveticker)
        //    {
        //        case 1:
        //            int tmp1 = FahrtenInPreisZone1;
        //            if (tmp1 > 0)
        //            {
        //                Console.WriteLine("James fährt mit seinem" + TaxiAuto + RandomGeschlecht() + KM und rechnet(XX, xx)€ ab.");

        //                tmp1 -= 1;
        //            }


        //            Console.WriteLine("hello");

        //            break;


        //    }
        //}
    }
}