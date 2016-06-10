using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Bestelling
    {
        public int Bestelnummer
        {
            get; private set;
        }
        public DateTime Besteldatum
        {
            get; private set;
        }
        public Int32 Totaalprijs
        {
            get; private set;
        }
        public List<Product>  Producten { get; private set; } = new List<Product>();
        public String Status
        {
            get; private set;
        }
        public String Betaalmethode
        {
            get; private set;
        }
        Database database = new Database();
        public Bestelling(int bestelnummer, DateTime besteldatum, Int32 totaalprijs, string status, string betaalmethode)
        {
            this.Bestelnummer = bestelnummer;
            this.Besteldatum = besteldatum;
            this.Totaalprijs = totaalprijs;
            this.Status = status;
            this.Betaalmethode = betaalmethode;
            this.Producten = database.ProductenBestelling(bestelnummer);
        }
 }
    
}