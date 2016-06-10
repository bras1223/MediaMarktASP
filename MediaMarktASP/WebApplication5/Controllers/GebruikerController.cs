using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcRazorToPdf;

namespace MediaMarkt.Controllers
{
    public class GebruikerController : Controller
    {
        Models.Webshop webshop = new Models.Webshop();
        // GET: Gebruiker
        public ActionResult Account()
        {
            if(Session["Gebruiker"] != null)
            {
                MediaMarkt.Models.Gebruiker g = Session["Gebruiker"] as MediaMarkt.Models.Gebruiker;
                g.UpdateBestellingen();
                Session["Gebruiker"] = g;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
        }

        public ActionResult Factuur(int bestelnummer)
        {
            Models.Bestelling bestelling = null;
            MediaMarkt.Models.Gebruiker g = Session["Gebruiker"] as MediaMarkt.Models.Gebruiker;
            g.UpdateBestellingen();
            foreach (MediaMarkt.Models.Bestelling b in g.bestellingen)
            {
                if (b.Bestelnummer == bestelnummer)
                {
                    bestelling = b;
                    break;
                }
            }
            ViewData["bestelling"] = bestelling;
            return View();
        }
    }
}