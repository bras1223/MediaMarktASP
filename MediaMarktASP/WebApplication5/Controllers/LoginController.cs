using MediaMarkt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaMarkt.Controllers
{
    public class LoginController : Controller
    {
        Webshop webshop = new Webshop();
        // GET: Login
        public ActionResult Index(string gebruikersnaam, string wachtwoord)
        {
            ViewBag.foutmelding = "";
            if (string.IsNullOrEmpty(gebruikersnaam) && string.IsNullOrEmpty(wachtwoord))
            {
                ViewBag.foutmelding = "";
            }
            else if (webshop.database.GebruikerLogin(gebruikersnaam, wachtwoord) == null)
            {
                ViewBag.foutmelding = "Gebruikersnaam of Wachtwoord is incorrect.";
            }
            else if(webshop.database.GebruikerLogin(gebruikersnaam, wachtwoord) != null)
            {
                Session["Gebruiker"] = webshop.database.GebruikerLogin(gebruikersnaam, wachtwoord);
                Session["test"] = webshop.database.GebruikerLogin(gebruikersnaam, wachtwoord);
                return RedirectToAction("Account", "Gebruiker", new { area = "" });
            }
            return View();
        }

        // GET: Registreren
        public ActionResult Registreren()
        {
            ViewBag.foutmelding = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registreren(string Voornaam, string Tussenvoegsel, string Achternaam, string Straatnaam, int? Huisnummer, string Postcode, string Stad, string Email, string Wachtwoord)
        {
            ViewBag.foutmelding = "";
            if (!string.IsNullOrEmpty(Voornaam))
            { 
                bool success = webshop.database.AddKlant(Voornaam, Tussenvoegsel, Achternaam, Straatnaam, Huisnummer, Postcode, Stad, Email, Wachtwoord);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.foutmelding = "Vul alle velden in!";
                    return View();
                }
            } else
            {
                ViewBag.foutmelding = "Vul alle velden in!";
                return View();
            }
        }
        
        public ActionResult Uitloggen()
        {
            Session["Gebruiker"] = null;
            return RedirectToAction("Index");
        }
    }
}
