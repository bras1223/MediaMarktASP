using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Gebruiker
    {
       public int KlantID
        {
            get; private set;
        }
       public string Email
        {
            get; private set;
        }
        public string Voornaam
        {
            get; private set;
        }
        public string Tussenvoegsel
        {
            get; private set;
        }
        public string Achternaam
        {
            get; private set;
        }
        public string Straatnaam
        {
            get; private set;
        }
        public int Huisnummer
        {
            get; private set;
        }
        public string Postcode
        {
            get; private set;
        }
        public string Stad
        {
            get; private set;
        }

        Database database = new Database();
        public List<Bestelling> bestellingen { get; private set; } = new List<Bestelling>();

        public Gebruiker(int klantID, string mail, string voornaam, string tussenvoegsel, string achternaam, string straatnaam, int huisnummer, string postcode, string stad)
        {
            this.KlantID = klantID;
            this.Email = mail;
            this.Voornaam = voornaam;
            this.Tussenvoegsel = tussenvoegsel;
            this.Achternaam = achternaam;
            this.Straatnaam = straatnaam;
            this.Huisnummer = huisnummer;
            this.Postcode = postcode;
            this.Stad = stad;
        }
        public void UpdateBestellingen()
        {
            this.bestellingen = database.GetOrders(this.KlantID);
        }

    }
}