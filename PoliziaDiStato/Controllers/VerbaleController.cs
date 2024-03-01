using PoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PoliziaDiStato.Controllers
{
    public class VerbaleController : Controller
    {
        // GET: Verbale

        // con l'Action Create() (GET) si restituisce la vista Create.cshtml in cui si inserisce un nuovo verbale
        // Mi serve una lista di persone e una lista di violazioni per poterle inserire nel form
        // le passo tramite ViewBag e le prendo alla dropdownlist nel form
        public ActionResult Create()
        {
            SqlConnection conn = Connection.GetConnection();
            List<Anagrafica> listaAnagrafica = new List<Anagrafica>();
            List<Tipo_Violazione> listaViolazioni = new List<Tipo_Violazione>();
            try
            {
                conn.Open();
                string queryAnagrafica = "SELECT idAnagrafica, Nome, Cognome, Codice_Fiscale FROM Anagrafica";
                SqlCommand cmdAnagrafica = new SqlCommand(queryAnagrafica, conn);
                SqlDataReader readerAnagrafica = cmdAnagrafica.ExecuteReader();
                while (readerAnagrafica.Read())
                {
                    Anagrafica persona = new Anagrafica();
                    persona.IdAnagrafica = Convert.ToInt32(readerAnagrafica["idAnagrafica"]);
                    persona.Nome = readerAnagrafica["Nome"].ToString();
                    persona.Cognome = readerAnagrafica["Cognome"].ToString();
                    persona.CodiceFiscale = readerAnagrafica["Codice_Fiscale"].ToString();
                    listaAnagrafica.Add(persona);
                }
                readerAnagrafica.Close();
                string queryViolazioni = "SELECT * FROM Tipo_Violazione";
                SqlCommand cmdViolazioni = new SqlCommand(queryViolazioni, conn);
                SqlDataReader readerViolazioni = cmdViolazioni.ExecuteReader();
                while (readerViolazioni.Read())
                {
                    Tipo_Violazione violazione = new Tipo_Violazione();
                    violazione.IdViolazione = Convert.ToInt32(readerViolazioni["IdViolazione"]);
                    violazione.Descrizione = readerViolazioni["Descrizione"].ToString();
                    listaViolazioni.Add(violazione);
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
            ViewBag.Anagrafica = listaAnagrafica;
            ViewBag.Violazioni = listaViolazioni;

            return View();
        }



        // con l'Action Create() (POST) si inserisce un nuovo verbale nel Database
        [HttpPost]
        public ActionResult Create(Verbale verbale)
        {

            if (ModelState.IsValid)
            {


                SqlConnection conn = Connection.GetConnection();

                try
                {
                    conn.Open();

                    string query = "INSERT INTO Verbale (Data_Violazione, Indirizzo_Violazione, Nominativo_Agente, Data_Trascrizione_Verbale, Importo, Decurtamento_Punti, Fk_IdAnagrafica, Fk_IdViolazione) VALUES (@DataViolazione, @IndirizzoViolazione, @NomeAgente, @DataTrascrizione, @Importo, @PuntiDecurtati, @Fk_IdAnagrafica, @Fk_IdViolazione)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    cmd.Parameters.AddWithValue("@NomeAgente", verbale.NomeAgente);
                    cmd.Parameters.AddWithValue("@DataTrascrizione", verbale.DataTrascrizione);
                    cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                    cmd.Parameters.AddWithValue("@PuntiDecurtati", verbale.PuntiDecurtati);
                    cmd.Parameters.AddWithValue("@Fk_IdAnagrafica", verbale.FK_IdAnagrafica);
                    cmd.Parameters.AddWithValue("@Fk_IdViolazione", verbale.FK_IdViolazione);
                    cmd.ExecuteNonQuery();
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




            }
            return RedirectToAction("Index", "Home");
        }


    }
}