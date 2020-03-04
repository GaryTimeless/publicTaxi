using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TaxiWorld
{
    public class BWL
    {
        //todo Ich weiß es fehlen noch mega viele aber die werden jetzt tricky dann, erstmal diese ausprobieren :)

        public static List<string> alleBla = new List<string> { "RBF", "Kapitalwert", "Sparbetrag", "Jaehrliche Sparsumme", "KWF1", "KWF2", "Amortisationsdauer" };


        public static bool positiveZahl(double input)
        {
            Regex zahl = new Regex("[0-9.,]+");
            if (zahl.IsMatch(input.ToString()) && input > 0)
            {
                return true;
            }
            return false;
        }


        public static double RBF(double zinssatz, int laufzeit)
        {
            if (positiveZahl(zinssatz) == true && positiveZahl(laufzeit) == true)
            {
                return ((Math.Pow((1 + zinssatz), laufzeit) - 1) / ((Math.Pow((1 + zinssatz), laufzeit) * zinssatz)));
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double Kapitalwert(double investition, double zinssatz, double c, int laufzeit)
        {
            if (positiveZahl(investition) == true && positiveZahl(zinssatz) == true && positiveZahl(c) == true && positiveZahl(laufzeit) == true)
            {
                return -investition + c * RBF(zinssatz, laufzeit);
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double Sparbetrag(double c, double zinssatz, int laufzeit)
        {
            if (positiveZahl(c) == true && positiveZahl(zinssatz) == true && positiveZahl(laufzeit) == true)
            {
                return c * RBF(zinssatz, laufzeit);
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double JaehrlicheSparsumme(double c, double zinssatz, int laufzeit)
        {
            if (positiveZahl(c) == true && positiveZahl(zinssatz) == true && positiveZahl(laufzeit) == true)
            {
                return ((Sparbetrag(c, zinssatz, laufzeit) / (Math.Pow((1 + zinssatz), laufzeit))) / RBF(zinssatz, laufzeit));
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double KWF1(double c, double ausgabe)
        {
            if (positiveZahl(c) == true && positiveZahl(ausgabe) == true)
            {
                return c / ausgabe;
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double KWF2(double zinssatz, int laufzeit)
        {
            if (positiveZahl(zinssatz) == true && positiveZahl(laufzeit) == true)
            {
                return (1 / RBF(zinssatz, laufzeit));
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }


        public static double Amortisationsdauer(double c, double ausgabe, double zinssatz)
        {
            if (positiveZahl(c) == true && positiveZahl(ausgabe) == true && positiveZahl(zinssatz) == true)
            {
                double wert1 = KWF1(c, ausgabe);
                int zeit = -1;
                double ergebnis;

                do
                {
                    zeit++;
                    ergebnis = KWF2(zinssatz, zeit);
                } while (ergebnis > wert1);

                return zeit;
            }
            throw new ArgumentException("\n\nKann mit den Daten nicht berechnet werden!");
        }
    }
}