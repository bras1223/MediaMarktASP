using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Categorie
    {
        public List<Product> producten { get; private set; } = new List<Product>();
        public string naam
        {
            get; private set;
        }
        public string afkorting
        {
            get; private set;
        }
        Database database = new Database();

        public Categorie(string naam, string afkorting)
        {
            this.naam = naam;
            this.afkorting = afkorting;
            this.producten = database.Producten(this.afkorting);
        }
    }
}