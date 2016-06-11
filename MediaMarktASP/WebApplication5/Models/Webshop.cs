using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Webshop
    {
        //Autoproperties
        public List<Categorie> categorien { get; private set; } = new List<Categorie>();
        public List<Categorie> Hoofdcategorien { get; private set; } = new List<Categorie>();
        public Database database = new Database();
        public Webshop()
        {
            this.categorien = database.Categorien();
            this.Hoofdcategorien = database.HoofdCategorien();
        }
    }
}