using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Winkelmandje
    {
        //Autoproperties
        public List<Product> producten { get; private set; } = new List<Product>();
        Database database = new Database();

        //Product toevoegen aan winkelmandje
        public void AddProduct(Product product)
        {
            bool toegevoegd = false;
            foreach (Product p in producten)
            {
                if(p.artikelnummer == product.artikelnummer)
                {
                    p.hoeveelheid += 1;
                    toegevoegd = true;
                    break;
                }
            }
            if (!toegevoegd)
            {
                producten.Add(product);
            }
        }

        //product verwijderen uit winkelmandje
        public void DeleteProduct(Product product)
        {
            bool verwijderd = false;
            foreach (Product p in producten)
            {
                if (p.artikelnummer == product.artikelnummer)
                {
                    if(p.hoeveelheid > 1)
                    {
                        p.hoeveelheid -= 1;
                        verwijderd = true;
                        break;
                    }
                }
            }
            if (!verwijderd)
            {
                producten.Remove(product);
            }
        }

        //Winkelmandje in database invoeren als bestelling
        public void BestelWinkelmandje(string betaalwijze, Gebruiker gebruiker)
        {
            database.AddOrder(betaalwijze, this.producten, gebruiker);
        }
    }
}