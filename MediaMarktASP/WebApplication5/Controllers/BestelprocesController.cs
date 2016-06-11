using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaMarkt.Models;

namespace MediaMarkt.Controllers
{
    public class BestelprocesController : Controller
    {
        Models.Webshop webshop = new Models.Webshop();
        // GET: BestelProces
        public ActionResult Betalen()
        {
            //Totaalbedrag winkelmandje in betaalpagina laden.
            Models.Winkelmandje winkelmand = Session["chart"] as Models.Winkelmandje;
            ViewData["Totaalprijs"] = winkelmand.producten.Sum(x => x.prijs * x.hoeveelheid);
            return View();
        }

        [HttpPost]
        public ActionResult Betalen(string optionsRadios)
        {
            //Betaalmethode geselecteerd.
            Models.Winkelmandje winkelmand = Session["chart"] as Models.Winkelmandje;
            foreach(Models.Product product in winkelmand.producten)
            {
                product.voorraad = product.voorraad - product.hoeveelheid;
            }

            //Uitvoeren bestelling
            winkelmand.BestelWinkelmandje(optionsRadios, (Session["Gebruiker"] as Models.Gebruiker));

            //Winkelmandje legen en terug naar gebruiker
            Session["chart"] = null;
            return RedirectToAction("Account", "Gebruiker", new { area = "" });
        }
    }
}