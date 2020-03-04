using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TaxiProjekt
{
    public class Antriebsart
    {
        public string Motorisierung;
        public string Getriebe;
        public int PS;
        public string Antrieb;

        public static Antriebsart Audi_35TFSI_Stronic = new Antriebsart("Benzin", "Automatik", 150, "2WD");
        public static Antriebsart Audi_40TDI_Stronic = new Antriebsart("Diesel", "Automatik", 190, "2WD");
        public static Antriebsart Audi_50TDI_Tiptronic = new Antriebsart("Diesel", "Automatik", 286, "2WD");
        public static Antriebsart Audi_45TDI_quattro_Tiptronic = new Antriebsart("Diesel", "Automatik", 231, "4WD");
        public static Antriebsart Audi_60TFSI_quattro_Tiptronic = new Antriebsart("Benzin", "Automatik", 460, "4WD");

        public static Antriebsart BMW_320i = new Antriebsart("Benzin", "Automatik", 184, "2WD");
        public static Antriebsart BMW_320d = new Antriebsart("Diesel", "Automatik", 190, "2WD");
        public static Antriebsart BMW_xDrive_40i = new Antriebsart("Benzin", "Automatik", 340, "4WD");
        public static Antriebsart BMW750d_xDrive = new Antriebsart("Diesel", "Automatik", 400, "4WD");
        public static Antriebsart BMW_xDrive_30d = new Antriebsart("Diesel", "Automatik", 265, "4WD");
        public static Antriebsart BM520d = new Antriebsart("Diesel", "Automatik", 190, "2WD");

        public static Antriebsart MercedesBenz_200 = new Antriebsart("Benzin", "Automatik", 198, "2WD");
        public static Antriebsart MercedesBenz_220d = new Antriebsart("Diesel", "Automatik", 194, "2WD");
        public static Antriebsart MercedesBenz_300de = new Antriebsart("Hybrid", "Automatik", 316, "2WD");
        public static Antriebsart MercedesBenz_200d = new Antriebsart("Diesel", "Automatik", 160, "2WD");
        public static Antriebsart MercedesBenz_450_4MATIC = new Antriebsart("Benzin", "Automatik", 389, "4WD");


        public Antriebsart(string motorisierung, string getriebe, int ps, string antrieb)
        {
            this.Motorisierung = motorisierung;
            this.Getriebe = getriebe;
            this.PS = ps;
            this.Antrieb = antrieb;
        }


        public override string ToString()
        {
            return this.Motorisierung + "\nGetriebe:\t" + this.Getriebe + "\nAntrieb:\t" + this.Antrieb + "\nLeistung:\t" + this.PS + " PS";
        }
    }
}