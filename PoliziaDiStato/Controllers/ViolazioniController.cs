using PoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PoliziaDiStato.Controllers
{
    public class ViolazioniController : Controller
    {
        // GET: Violazioni
        // L'Action Index() restituisce la vista Index.cshtml che mostra la lista delle violazioni presenti nel Database
        public ActionResult Index()
        {
            SqlConnection conn = Connection.GetConnection();
            List<Tipo_Violazione> listaViolazioni = new List<Tipo_Violazione>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Tipo_Violazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tipo_Violazione violazione = new Tipo_Violazione();
                    violazione.IdViolazione = Convert.ToInt32(reader["IdViolazione"]);
                    violazione.Descrizione = reader["Descrizione"].ToString();
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
            return View(listaViolazioni);
        }

        public ActionResult Create()
        {
            return View();
        }

        // con l'Action Create() (POST) si inserisce una nuova violazione nel Database
        [HttpPost]
        public ActionResult Create(Tipo_Violazione violazione)
        {
            SqlConnection conn = Connection.GetConnection();
            try
            {
                conn.Open();
                string query = "INSERT INTO Tipo_Violazione (Descrizione) VALUES (@Descrizione)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
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
            return RedirectToAction("Index");
        }
    }
}