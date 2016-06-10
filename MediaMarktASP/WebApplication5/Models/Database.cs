using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;
using System.Linq;
using System.Data;
namespace MediaMarkt.Models
{
    public class Database
    {
        private OracleConnection con;
        private OracleCommand command;
        private OracleDataReader reader;

        public Database()
        {

            string constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))"
                          + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));"
                          + "User ID=mediamarkt; PASSWORD=mediamarkt;";

            con = new OracleConnection(constr);
        }

        public List<Product> Producten(string categorie)
        {
            List<Product> producten = new List<Product>();

            con.Open();
          
            command = new OracleCommand("SELECT Artikelnummer, Categorie, Naam, Prijs, Omschrijving, Voorraad FROM Artikel WHERE Categorie =:categorie", con);
            command.Parameters.Add(new OracleParameter(":categorie", OracleDbType.Varchar2)).Value = categorie;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product(Convert.ToInt32(reader["Artikelnummer"]), reader["Naam"].ToString(), reader["Categorie"].ToString(), Convert.ToInt32(reader["Prijs"]), reader["Omschrijving"].ToString(), Convert.ToInt32(reader["Voorraad"]), 1);
                producten.Add(product);
            }
            con.Close();

            return producten;
        }
        public List<Product> ProductenBestelling(int bestelnummer)
        {
            List<Product> producten = new List<Product>();

            con.Open();

            command = new OracleCommand("SELECT a.Artikelnummer, a.Categorie, a.Naam, b.Prijs, a.Omschrijving, a.Voorraad, b.Hoeveelheid FROM Artikel a, ArtikelBestelling b WHERE a.Artikelnummer = b.Artikelnummer AND b.Bestelnummer=:bestelnummer", con);
            command.Parameters.Add(new OracleParameter(":bestelnummer", OracleDbType.Varchar2)).Value = bestelnummer;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product(Convert.ToInt32(reader["Artikelnummer"]), reader["Naam"].ToString(), reader["Categorie"].ToString(), Convert.ToInt32(reader["Prijs"]), reader["Omschrijving"].ToString(), Convert.ToInt32(reader["Voorraad"]), Convert.ToInt32(reader["Hoeveelheid"]));
                producten.Add(product);
            }
            con.Close();

            return producten;
        }
        public List<Product> GetProducten()
        {
            List<Product> producten = new List<Product>();

            con.Open();

            command = new OracleCommand("SELECT Artikelnummer, Categorie, Naam, Prijs, Omschrijving, Voorraad FROM Artikel", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product(Convert.ToInt32(reader["Artikelnummer"]), reader["Naam"].ToString(), reader["Categorie"].ToString(), Convert.ToInt32(reader["Prijs"]), reader["Omschrijving"].ToString(), Convert.ToInt32(reader["Voorraad"]), 1);
                producten.Add(product);
            }
            con.Close();

            return producten;
        }
        public List<Specificaties> GetSpecs(Int32 artikelnummer)
        {
            List<Specificaties> specificaties = new List<Specificaties>();

            con.Open();

            //Eigen hulpvragen weergeven waarop een nieuwe reactie is gegeven.
            command = new OracleCommand("SELECT s.Omschrijving, i.Uitwerking FROM Specparts s, Specificaties i WHERE i.Specnummer = s.Specnummer AND i.Artikelnummer =:Artikelnummer", con);
            command.Parameters.Add(new OracleParameter(":Artikelnummer", OracleDbType.Int32)).Value = artikelnummer;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Specificaties specificatie = new Specificaties(reader["Omschrijving"].ToString(), reader["Uitwerking"].ToString());
                specificaties.Add(specificatie);
            }
            con.Close();

            return specificaties;
        }

        public List<Bestelling> GetOrders(int KlantID)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            con.Open();

            //Eigen hulpvragen weergeven waarop een nieuwe reactie is gegeven.
            command = new OracleCommand("SELECT b.Bestelnummer, b.Besteldatum, f.Prijs, (SELECT s.NAME_STR FROM STATUS s WHERE s.STATUS_ID = b.STATUS_ID) AS STATUS, (SELECT BESCHRIJVING FROM BETAALWIJZE WHERE ID = f.Betaalwijze) AS BETAALWIJZE FROM Bestelling b, Factuur f WHERE f.Bestelnummer = b.Bestelnummer AND b.KlantID =:KlantID", con);
            command.Parameters.Add(new OracleParameter(":KlantID", OracleDbType.Varchar2)).Value = KlantID;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Bestelling bestelling = new Bestelling(Convert.ToInt32(reader["Bestelnummer"]), Convert.ToDateTime(reader["Besteldatum"]), Convert.ToInt32(reader["Prijs"]), reader["STATUS"].ToString(), reader["BETAALWIJZE"].ToString());
                bestellingen.Add(bestelling);
            }
            con.Close();

            return bestellingen;
        }
        public List<Categorie> Categorien()
        {
            List<Categorie> categorien = new List<Categorie>();
            con.Open();

            command = new OracleCommand("SELECT * FROM Categorie", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Categorie categorie = new Categorie(reader["Naam"].ToString(), reader["CatID"].ToString());
                categorien.Add(categorie);
            }
            con.Close();

            return categorien;
        }
        public List<Categorie> HoofdCategorien()
        {
            List<Categorie> categorien = new List<Categorie>();
            con.Open();

            command = new OracleCommand("SELECT * FROM Categorie WHERE Hoofdcat IS NULL", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Categorie categorie = new Categorie(reader["Naam"].ToString(), reader["CatID"].ToString());
                categorien.Add(categorie);
            }
            con.Close();

            return categorien;
        }
        public List<Categorie> SubCategorien(string Hoofdcat)
        {
            List<Categorie> categorien = new List<Categorie>();
            con.Open();

            command = new OracleCommand("SELECT * FROM Categorie WHERE Hoofdcat =:hoofdcat", con);
            command.Parameters.Add(new OracleParameter(":hoofdcat", OracleDbType.Varchar2)).Value = Hoofdcat;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Categorie categorie = new Categorie(reader["Naam"].ToString(), reader["CatID"].ToString());
                categorien.Add(categorie);
            }
            con.Close();

            return categorien;
        }
        public Gebruiker GebruikerLogin(string email, string wachtwoord)
        {
            Gebruiker gebruiker = null;
            try
            {
                con.Open();
                //Gebruikersnaam zoeken waar gebruikersnaam gelijk is aan de ingevoerde naam + w8woord
                command = new OracleCommand("SELECT * FROM Klant WHERE Email = :email AND wachtwoord = :pw", con);
                command.Parameters.Add(new OracleParameter("email", email));
                command.Parameters.Add(new OracleParameter("pw", EncryptString(wachtwoord)));
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gebruiker = new Gebruiker(Convert.ToInt32(reader["KlantID"]), reader["Email"].ToString(), reader["Voornaam"].ToString(), reader["Tussenvoegsel"].ToString(), reader["Achternaam"].ToString(), reader["Straatnaam"].ToString(), Convert.ToInt32(reader["Huisnummer"]), reader["Postcode"].ToString(), reader["Stad"].ToString());
                    //Properties toekennen aan gebruiken.
                    
                }
                    
            }
            catch
            {

            }
            finally
            {
                
                con.Close();
            }
return gebruiker;
        }

        public bool AddKlant(string Voornaam, string Tussenvoegsel, string Achternaam, string Straatnaam, int? Huisnummer, string Postcode, string Stad, string Email, string Wachtwoord)
        {
                con.Open();
            try {
                command = new OracleCommand(@"INSERT INTO KLANT(VOORNAAM, TUSSENVOEGSEL, ACHTERNAAM, STRAATNAAM, HUISNUMMER, POSTCODE, EMAIL, WACHTWOORD, STAD)" +
                                                  "VALUES(:Voornaam, :Tussenvoegsel, :Achternaam, :Straatnaam, :Huisnummer, :Postcode, :Email, :Wachtwoord, :Stad)", con);
                command.Parameters.Add(new OracleParameter(":Voornaam", OracleDbType.Varchar2)).Value = Voornaam;
                command.Parameters.Add(new OracleParameter(":Tussenvoegsel", OracleDbType.Varchar2)).Value = Tussenvoegsel;
                command.Parameters.Add(new OracleParameter(":Achternaam", OracleDbType.Varchar2)).Value = Achternaam;
                command.Parameters.Add(new OracleParameter(":Straatnaam", OracleDbType.Varchar2)).Value = Straatnaam;
                command.Parameters.Add(new OracleParameter(":Huisnummer", OracleDbType.Int32)).Value = Huisnummer;
                command.Parameters.Add(new OracleParameter(":Postcode", OracleDbType.Varchar2)).Value = Postcode;
                command.Parameters.Add(new OracleParameter(":Email", OracleDbType.Varchar2)).Value = Email;
                command.Parameters.Add(new OracleParameter(":Wachtwoord", OracleDbType.Varchar2)).Value = EncryptString(Wachtwoord);
                command.Parameters.Add(new OracleParameter(":Stad", OracleDbType.Varchar2)).Value = Stad;

                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }             
        }
        public string EncryptString(string toEncrypt)
        {
            SHA256Managed crypt = new SHA256Managed();
            System.Text.StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(toEncrypt), 0, Encoding.UTF8.GetByteCount(toEncrypt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public void AddOrder(string betaalwijze, List<Product> producten, Gebruiker gebruiker)
        {
            con.Open();
            Int32 ordernumber = 0;
            command = new OracleCommand("SELECT MAX(BESTELNUMMER) AS bestelnummer FROM BESTELLING", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                    if (reader["bestelnummer"].ToString() != "")
                    {
                    ordernumber = Convert.ToInt32(reader["bestelnummer"]);
                    }   
            }
            ordernumber += 1;
                //Bestelling aanmaken
                command = new OracleCommand(@"INSERT INTO BESTELLING(BESTELNUMMER, KLANTID, BESTELDATUM)" +
                                                  "VALUES(:Bestelnummer, :KlantID, :Besteldatum)", con);
                command.Parameters.Add(new OracleParameter(":Bestelnummer", OracleDbType.Int32)).Value = ordernumber;
                command.Parameters.Add(new OracleParameter(":KlantID", OracleDbType.Int32)).Value = gebruiker.KlantID;
                command.Parameters.Add(new OracleParameter(":Besteldatum", OracleDbType.Date)).Value = DateTime.Now;
                command.ExecuteNonQuery();

                foreach (Product product in producten)
                {
                    command = new OracleCommand(@"INSERT INTO ARTIKELBESTELLING(BESTELNUMMER, ARTIKELNUMMER, HOEVEELHEID, PRIJS)" +
                                      "VALUES(:Bestelnummer, :Artikelnummer, :Hoeveelheid, :Prijs)", con);
                    command.Parameters.Add(new OracleParameter(":Bestelnummer", OracleDbType.Int32)).Value = ordernumber;
                    command.Parameters.Add(new OracleParameter(":Artikelnummer", OracleDbType.Int32)).Value = product.artikelnummer;
                    command.Parameters.Add(new OracleParameter(":Hoeveelheid", OracleDbType.Int32)).Value = product.hoeveelheid;
                    command.Parameters.Add(new OracleParameter(":Prijs", OracleDbType.Int32)).Value = product.prijs;
                    command.ExecuteNonQuery();

                    command = new OracleCommand(@"UPDATE ARTIKEL SET VOORRAAD =:voorraad WHERE ARTIKELNUMMER=:artikelnummer", con);
                    command.Parameters.Add(new OracleParameter(":voorraad", OracleDbType.Int32)).Value = product.voorraad;
                    command.Parameters.Add(new OracleParameter(":artikelnummer", OracleDbType.Int32)).Value = product.artikelnummer;
                    command.ExecuteNonQuery();
            }

                command = new OracleCommand(@"INSERT INTO FACTUUR(FACTUURNUMMER, FACTUURADRES, BETAALWIJZE, BETAALSTATUS, BESTELNUMMER, PRIJS)" +
                                  "VALUES(:Factuurnummer, :Factuuradres, :Betaalwijze, :Betaalstatus, :Bestelnummer, :Prijs)", con);
                command.Parameters.Add(new OracleParameter(":Factuurnummer", OracleDbType.Int32)).Value = ordernumber;
                command.Parameters.Add(new OracleParameter(":Factuuradres", OracleDbType.Varchar2)).Value = (gebruiker.Straatnaam + " "+gebruiker.Huisnummer + " " + gebruiker.Postcode + " " + gebruiker.Stad);
                command.Parameters.Add(new OracleParameter(":Betaalwijze", OracleDbType.Int32)).Value = Convert.ToInt32(betaalwijze);
                command.Parameters.Add(new OracleParameter(":Betaalstatus", OracleDbType.Char)).Value = 'N';
                command.Parameters.Add(new OracleParameter(":Bestelnummer", OracleDbType.Int32)).Value = ordernumber;
                command.Parameters.Add(new OracleParameter(":Prijs", OracleDbType.Int32)).Value = producten.Sum(x => x.prijs * x.hoeveelheid);
            command.ExecuteNonQuery();


                con.Close();
        }
    }
}