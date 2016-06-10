using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Winkelmandje
    {
        public List<Product> producten { get; private set; } = new List<Product>();
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
    }
}