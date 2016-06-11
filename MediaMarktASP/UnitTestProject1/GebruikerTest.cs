using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaMarkt.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class GebruikerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Gebruiker testgebruiker = new Gebruiker(1, "piet@gmail.com", "Piet", "", "Peters", "Rachelsmolen", 1, "6000AE", "Eindhoven");

            // assert
            Assert.AreEqual("piet@gmail.com", testgebruiker.Email, "Klopt!");
            Assert.AreEqual("Piet", testgebruiker.Voornaam, "Klopt!");
            Assert.AreEqual("", testgebruiker.Tussenvoegsel, "Klopt!");
            Assert.AreEqual("Peters", testgebruiker.Achternaam, "Klopt!");
            Assert.AreEqual("Rachelsmolen", testgebruiker.Straatnaam, "Klopt!");
            Assert.AreEqual(1, testgebruiker.Huisnummer, "Klopt!");
            Assert.AreEqual("6000AE", testgebruiker.Postcode, "Klopt!");
            Assert.AreEqual("Eindhoven", testgebruiker.Stad, "Klopt!");
        }
    }
}
