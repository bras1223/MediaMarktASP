using MediaMarkt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MediaMarkt.Controllers
{
    public class WebshopController : Controller
    {
        Webshop webshop = new Webshop();
        // GET: Webshop
        
        //Index ophalen
        public ActionResult Index()
        {
            ViewBag.Data = webshop;
            
            return View();
        }

        //Categoriën pagina tonen
        public ActionResult Categoriën(string id)
        {
            Categorie selectedCategorie = null;
            //Geselecteerde categorie achterhalen
            foreach (Categorie categorie in webshop.categorien)
            {
                if(categorie.afkorting == id)
                {
                    selectedCategorie = categorie;
                    break;
                }
            }
            ViewBag.Data = webshop;
            
            //Producten-lijst van categorie tonen
            if(selectedCategorie != null)
            {
                ViewData["categorie"] = selectedCategorie;
                ViewData["Subcategorien"] = selectedCategorie.subcategorie;
                ViewData["producten"] = selectedCategorie.producten;
                return View("CategorieView");
            }
            else
            {
                return View();
            }
           
        }

        //Product toevoegen aan winkelmandje
        public ActionResult WinkelmandjeAdd(string id, int artikelnummer)
        {

            Categorie selectedCategorie = null;
            Product selectedProduct = null;
            foreach (Categorie categorie in webshop.categorien)
            {
                if (categorie.afkorting == id)
                {
                    selectedCategorie = categorie;
                    break;
                }
            }
            //Product achterhalen
            foreach (Product product in selectedCategorie.producten)
            {
                if (product.artikelnummer == artikelnummer)
                {
                    selectedProduct = product;
                }
            }

            Winkelmandje winkelmandje = new Winkelmandje();
            //Winkelmandje sessie aanmaken waar nodig. Product toevoegen.
            if (Session["chart"] == null)
            {
                winkelmandje.AddProduct(selectedProduct);
                Session["chart"] = winkelmandje;
            }
            else
            {
                winkelmandje = Session["chart"] as Winkelmandje;
                winkelmandje.AddProduct(selectedProduct);
                Session["chart"] = winkelmandje;
            }

            return RedirectToAction("Winkelmandje", "Webshop", new { area = "" });
        }

        //Product verwijderen uit winkelmandje
        public ActionResult WinkelmandjeRemove(string id, int artikelnummer)
        {
            Winkelmandje winkelmandje = Session["chart"] as Winkelmandje;
            Product selectedProduct = null;

            //Product achterhalen
            foreach (Product product in winkelmandje.producten)
            {
                if (product.artikelnummer == artikelnummer)
                {
                    selectedProduct = product;
                    break;
                }
            }

            //Product verwijderen
            winkelmandje.DeleteProduct(selectedProduct);
            Session["chart"] = winkelmandje;

            return RedirectToAction("Winkelmandje", "Webshop", new { area = "" });
        }

        //Winkelmandje tonen
        public ActionResult Winkelmandje()
        {
            Winkelmandje winkelmandje = new Winkelmandje();

            //Winkelmandje sessie aanmaken, waar nodig.
            if (Session["chart"] == null)
            {
                Session["chart"] = winkelmandje;
            }
            else
            {
                winkelmandje = Session["chart"] as Winkelmandje;
            }
            ViewData["winkelmand"] = winkelmandje.producten;
            ViewData["Totaalprijs"] = winkelmandje.producten.Sum(x => x.prijs * x.hoeveelheid);
            return View();
        }

        //Product tonen
        public ActionResult Product(string id, int artikelnummer)
        {
            Categorie selectedCategorie = null;
            Product selectedProduct = null;

            //Categorie achterhalen
            foreach (Categorie categorie in webshop.categorien)
            {
                if(categorie.afkorting == id)
                {
                    selectedCategorie = categorie;
                    break;
                }
            }

            //Product achterhalen
            foreach (Product product in selectedCategorie.producten)
            {
                if(product.artikelnummer == artikelnummer)
                {
                    selectedProduct = product;
                }
            }
            
            ViewData["categorie"] = selectedCategorie;
            selectedProduct.UpdateSpecificaties();
            ViewData["product"] = selectedProduct;
            return View();
        }
    }
}