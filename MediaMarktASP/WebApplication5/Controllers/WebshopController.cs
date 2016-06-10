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
        public ActionResult Index()
        {
            ViewBag.Data = webshop;
            
            return View();
        }

        public ActionResult Categoriën(string id)
        {
            Categorie selectedCategorie = null;

            foreach (Categorie categorie in webshop.categorien)
            {
                if(categorie.afkorting == id)
                {
                    selectedCategorie = categorie;
                    break;
                }
            }
            ViewBag.Data = webshop;

            if(selectedCategorie != null)
            {

                List<Categorie> subcategorie = webshop.database.SubCategorien(selectedCategorie.afkorting);
                ViewData["categorie"] = selectedCategorie;
                ViewData["Subcategorien"] = subcategorie;
                ViewData["producten"] = selectedCategorie.producten;
                return View("CategorieView");
            }
            else
            {
                return View();
            }
           
        }

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

            foreach (Product product in selectedCategorie.producten)
            {
                if (product.artikelnummer == artikelnummer)
                {
                    selectedProduct = product;
                }
            }

            Winkelmandje winkelmandje = new Winkelmandje();

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
        public ActionResult WinkelmandjeRemove(string id, int artikelnummer)
        {
            Winkelmandje winkelmandje = Session["chart"] as Winkelmandje;
            Product selectedProduct = null;
            foreach (Product product in winkelmandje.producten)
            {
                if (product.artikelnummer == artikelnummer)
                {
                    selectedProduct = product;
                    break;
                }
            }

            winkelmandje.DeleteProduct(selectedProduct);
            Session["chart"] = winkelmandje;

            return RedirectToAction("Winkelmandje", "Webshop", new { area = "" });
        }
        public ActionResult Winkelmandje()
        {
            Winkelmandje winkelmandje = new Winkelmandje();

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
        public ActionResult Product(string id, int artikelnummer)
        {
            Categorie selectedCategorie = null;
            Product selectedProduct = null;
            foreach (Categorie categorie in webshop.categorien)
            {
                if(categorie.afkorting == id)
                {
                    selectedCategorie = categorie;
                    break;
                }
            }

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