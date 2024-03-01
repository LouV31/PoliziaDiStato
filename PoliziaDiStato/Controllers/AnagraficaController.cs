using PoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PoliziaDiStato.Controllers
{
    public class AnagraficaController : Controller
    {
        // GET: Anagrafica
        // l'Action Index() restituisce la vista Index.cshtml che mostra la lista delle persone presenti nel Database
        public ActionResult Index()
        {
            SqlConnection conn = Connection.GetConnection();
            List<Anagrafica> listaAnagrafica = new List<Anagrafica>();
            try
            {
                conn.Open();
                string query = "SELECT idAnagrafica, Nome, Cognome, Indirizzo, Citta, CAP, Codice_Fiscale FROM Anagrafica";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Anagrafica persona = new Anagrafica();
                    persona.IdAnagrafica = Convert.ToInt32(reader["idAnagrafica"]);
                    persona.Nome = reader["Nome"].ToString();
                    persona.Cognome = reader["Cognome"].ToString();
                    persona.Indirizzo = reader["Indirizzo"].ToString();
                    persona.Citta = reader["Citta"].ToString();
                    persona.CAP = reader["CAP"].ToString();
                    persona.CodiceFiscale = reader["Codice_Fiscale"].ToString();
                    listaAnagrafica.Add(persona);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(listaAnagrafica);
        }

        public ActionResult Create()
        {
            return View();
        }

        // con l'Action Create() (POST) si inserisce una nuova persona nel Database c'è un controllo per verificare che il codice fiscale non sia già presente nel Database
        [HttpPost]
        public ActionResult Create(Anagrafica persona)
        {
            SqlConnection conn = Connection.GetConnection();
            try
            {
                conn.Open();
                // controllo se il codice fiscale è già presente nel Database prendendo il numero di record che soddisfano la condizione e se è maggiore di 0 restituisco un messaggio di errore
                string queryCheck = "SELECT COUNT(*) FROM Anagrafica WHERE Codice_Fiscale = @Codice_Fiscale";
                SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                cmdCheck.Parameters.AddWithValue("@Codice_Fiscale", persona.CodiceFiscale);
                int count = (int)cmdCheck.ExecuteScalar();
                if (count > 0)
                {
                    ViewBag.Message = "Il codice Fiscale esiste già nel Database";
                    return View();
                }
                else
                {
                    string query = "INSERT INTO Anagrafica (Nome, Cognome, Indirizzo, Citta, CAP, Codice_Fiscale) VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @CAP, @Codice_Fiscale)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nome", persona.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", persona.Cognome);
                    cmd.Parameters.AddWithValue("@Indirizzo", persona.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", persona.Citta);
                    cmd.Parameters.AddWithValue("@CAP", persona.CAP);
                    cmd.Parameters.AddWithValue("@Codice_Fiscale", persona.CodiceFiscale);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

    }
}