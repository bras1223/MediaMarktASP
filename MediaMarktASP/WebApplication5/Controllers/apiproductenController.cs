using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MediaMarkt.Models;

namespace MediaMarkt.Controllers
{
    public class apiproductenController : ApiController
    {
        Webshop webshop = new Webshop();
        // GET api/<controller>

        //Stuur List door, bij een get statement.
        public IEnumerable<Apiproducten> Get()
        {
            List<Models.Product> producten = webshop.database.GetProducten();
            List<Models.Apiproducten> apiproducten = new List<Apiproducten>();
            foreach(Product product in producten)
            {
                //Plaatsen van producten in de door te sturen list.
                apiproducten.Add(new Apiproducten
                {
                    Naam = product.naam,
                    Prijs = product.prijs,
                    Link = $"http://http://192.168.19.214/mediamarkt/Webshop/Product/{product.categorie}?artikelnummer={product.artikelnummer}"

                });
            }
            return apiproducten;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}