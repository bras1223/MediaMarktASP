using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaMarkt.Models
{
    public class Specificaties
    {
        //Autoproperties
        public string naam
        {
            get; private set;
        }
        public string uitwerking
        {
            get; private set;
        }
        
        public Specificaties(string naam, string uitwerking)
        {
            this.naam = naam;
            this.uitwerking = uitwerking;
        }
    }
}