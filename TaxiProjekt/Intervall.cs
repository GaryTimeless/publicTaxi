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
        // Choose option intervall length -> 1 Monat, 3 Monate, 6 Monate
        // Check anzahl Taxis -> choose one to calculate / decide activity
        // GO: jedes Taxi hat den gleichen €/km Wert 
        //
        //evtl entscheiden verschiedene Werte über Anzahl Taxifahrt:
        //                          können Taxis Leveln?
        // welche kosten fallen an?
        // sprit kosten, Reinigungskosten, Reperaturkosten/Wartungskosten







        public static int IntervallLaenge()
        {
            Console.WriteLine("Bitte wähle, aus wie lange das Intervall sein soll:");
            Console.WriteLine("1.) 1 Monat\n2.) 3 Monate\n3.) 6 Monate");
            Console.Write("Option: ");

            int AnzahlMonate = -1;
            while (!EinsbisDrei.IsMatch(AnzahlMonate.ToString()))
            {
                try
                {

                    AnzahlMonate = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    Console.WriteLine("Bitte eine vorgegebene Zahl eingeben");

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
                    int MaxFahrten = 80; // 5 am Tag, 5 Tage die Woche, 4 Wochen im Monat
                    double Preiszone1EuroProKm = 0.80;
                    double Preiszone2EuroProKm = 0.50;
                    double Preiszone3EuroProKm = 0.30;

                    //Anzahl Fahrten in versch. Preiszonen
                    int FahrtenInPreisZone1 = rand.Next(1, MaxFahrten); // PreisZone 1 max KM radius -> 1-10KM
                    MaxFahrten -= FahrtenInPreisZone1;
                    int FahrtenInPreisZone2 = rand.Next(1, MaxFahrten);// PreisZone 2 max KM radius -> 10-30KM
                    MaxFahrten -= FahrtenInPreisZone2;
                    int FahrtenInPreisZone3 = rand.Next(1, MaxFahrten);//PreisZone 3 max KM radius -> 30-100KM

                    // Umsatz der Fahrten in PZX und insgesamt
                    double UmsatzPZ1 = 0;
                    double UmsatzPZ2 = 0;
                    double UmsatzPZ3 = 0;

                    

                    // Gefahrene KM pro PZ, insgesamt
                    double gesamtKm = 0;
                    double gesamtKmPZ1 = 0;

                    List<int> gefahreneKminPZ1 = new List<int>(); // ist in so fern praktisch, da man mit Count und index aus der Liste viel herauslesen kann. Ist aber auch doppelt gemoppelt
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


                    //Ausgabe der Anzahl Fahrten in versch. Preiszonen
                    Console.WriteLine("aktuelles Taxi: "+TaxiAuto.Modell);
                    Console.WriteLine("Anzahl Fahrten PZ1 :"+FahrtenInPreisZone1.ToString());
                    Console.WriteLine("Anzahl Fahrten PZ2 :"+FahrtenInPreisZone2.ToString());
                    Console.WriteLine("Anzahl Fahrten PZ3 :"+FahrtenInPreisZone3.ToString());
                    Console.WriteLine("Umsatz PZ 1: "+UmsatzPZ1.ToString());
                    Console.WriteLine("Umsatz PZ 2: "+UmsatzPZ2.ToString());
                    Console.WriteLine("Umsatz PZ 3: " + UmsatzPZ3.ToString());
                    Console.WriteLine("\ngesamt Umsatz: " + gesamtUmsatzProMonat.ToString());

                    Console.WriteLine("gefahrene KM PZ 1: " + gesamtKmPZ1.ToString());
                    Console.WriteLine("gefahrene KM PZ 2: " + gesamtKmPZ2.ToString());
                    Console.WriteLine("gefahrene KM PZ 3: " + gesamtKmPZ3.ToString());
                    Console.WriteLine("gefahrene KM insg. "+ gesamtKm.ToString());
                    Console.WriteLine("gefahrene KM Ueberpruefen: " + (gesamtKmPZ1 + gesamtKmPZ2 + gesamtKmPZ3));


                    Console.ReadKey();


                    TaxiUnternehmen.Kapital += gesamtUmsatzProMonat;
                    TaxiAuto.Kilometerstand += gesamtKm;


                    // alle Daten müssen nun sauber irgendwo gespeichert werden. Auto X hat im Monat MM.JJ : -> UmsazuPZ1
                    //                                                                                       -> UmsatzPZ2
                    //                                                                                       -> UmsatzPZ3
                    //                                                                                       -> GesamtUmsatz
                    //                                                                                       -> gefahreneKM







                }

            }


        }


        




        




    }





}
