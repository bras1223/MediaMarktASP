using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaMarkt.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class WinkelmandjeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Winkelmandje winkelmandje = new Winkelmandje();
            Product product = new Product(1, "S7 EDGE", "SM", 799, "Telefoon", 32, 1);
            // act
            winkelmandje.AddProduct(product);

            // assert
            double actual = winkelmandje.producten.Count;
            Assert.AreEqual(1, actual, 0.001, "Product is toegevoegd!");

            winkelmandje.DeleteProduct(product);

            double actual2 = winkelmandje.producten.Count;
            Assert.AreEqual(0, actual2, 0.001, "Product is verwijderd!");
        }
    }
}
