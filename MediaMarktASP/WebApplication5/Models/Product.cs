using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Product
    {
        Database database = new Database();
        //Autoproperties
        public int artikelnummer
        {
            get; private set;
        }
        public string naam
        {
            get; private set;
        }
        public string categorie
        {
            get; private set;
        }
        public Int32 prijs
        {
            get; private set;
        }
        public string omschrijving
        {
            get; private set;
        }
        public int voorraad
        {
            get; set;
        }
        public List<Specificaties> specificaties
        {
            get; private set;
        }
        public int hoeveelheid
        {
            get; set;
        }
        public Product(int artikelnummer, string naam, string categorie, int prijs, string omschrijving, int voorraad, int hoeveelheid)
        {
            this.artikelnummer = artikelnummer;
            this.naam = naam;
            this.categorie = categorie;
            this.prijs = prijs;
            this.omschrijving = omschrijving;
            this.voorraad = voorraad;
            this.hoeveelheid = hoeveelheid;
        }

        //Specificaties uit database ophalen
        public void UpdateSpecificaties()
        {
            this.specificaties = database.GetSpecs(this.artikelnummer);
        }
    }
}