using PoliziaDiStato.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PoliziaDiStato.Controllers
{
    public class HomeController : Controller
    {
        // L'Action Index() restituisce la vista principale dell'applicazione in cui ci sono i link alle altre alle view delle altre action
        public ActionResult Index()
        {
            return View();
        }

        // L'Action TotaleVerbaliAggressore() restituisce la vista TotaleVerbaliAggressore.cshtml che mostra il totale dei verbali emessi per ogni aggressore
        public ActionResult TotaleVerbaliAggressore()
        {
            SqlConnection conn = Connection.GetConnection();
            List<Anagrafica> lista = new List<Anagrafica>();
            try
            {
                conn.Open();
                string query = "SELECT Cognome, Nome, COUNT(*) AS NumeroVerbali, SUM(Importo) AS TotaleImporti, SUM(Decurtamento_Punti) AS TotalePunti FROM Anagrafica INNER JOIN Verbale ON Anagrafica.IdAnagrafica = Verbale.Fk_IdAnagrafica GROUP BY Cognome, Nome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Anagrafica record = new Anagrafica();
                    record.Cognome = reader["Cognome"].ToString();
                    record.Nome = reader["Nome"].ToString();
                    record.TotaleNumeroVerbali = Convert.ToInt32(reader["NumeroVerbali"]);
                    record.TotaleImporti = Convert.ToDouble(reader["TotaleImporti"]);
                    record.TotalePuntiDecurtati = Convert.ToInt32(reader["TotalePunti"]);
                    lista.Add(record);
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
            return View(lista);
        }
        // L'Action TotalePuntiDecurtatiAggressore() restituisce la vista TotalePuntiDecurtatiAggressore.cshtml che mostra il totale dei punti decurtati per ogni aggressore    
        public ActionResult TotalePuntiDecurtatiAggressore()
        {
            SqlConnection conn = Connection.GetConnection();
            List<Anagrafica> lista = new List<Anagrafica>();
            try
            {
                conn.Open();
                string query = "SELECT Cognome, Nome, SUM(Decurtamento_Punti) AS TotalePunti FROM Anagrafica INNER JOIN Verbale ON Anagrafica.IdAnagrafica = Verbale.Fk_IdAnagrafica GROUP BY Cognome, Nome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Anagrafica record = new Anagrafica();
                    record.Cognome = reader["Cognome"].ToString();
                    record.Nome = reader["Nome"].ToString();
                    record.TotalePuntiDecurtati = Convert.ToInt32(reader["TotalePunti"]);
                    lista.Add(record);
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
            return View(lista);
        }

        // L'Action VerbaliSupQuattrocento() restituisce la vista VerbaliSupQuattrocento.cshtml che mostra i verbali con importo superiore a 400 euro qui uso il modello SharedAnagraficaVerbale che contiene i campi di Anagrafica, Verbale e Tipo_Violazione
        public ActionResult VerbaliSupQuattrocento()
        {
            SqlConnection conn = Connection.GetConnection();
            List<SharedAnagraficaVerbale> lista = new List<SharedAnagraficaVerbale>();
            try
            {
                conn.Open();
                string query = "SELECT A.Nome, A.Cognome, B.Importo, C.Descrizione FROM Anagrafica AS A INNER JOIN Verbale B ON B.Fk_IdAnagrafica = A.idAnagrafica INNER JOIN Tipo_Violazione AS C ON B.Fk_IdViolazione = C.idViolazione WHERE B.Importo > 400";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SharedAnagraficaVerbale record = new SharedAnagraficaVerbale
                    {
                        Anagrafica = new Anagrafica(),
                        Verbale = new Verbale(),
                        Tipo_Violazione = new Tipo_Violazione()
                    };

                    record.Anagrafica.Nome = reader["Nome"].ToString();
                    record.Anagrafica.Cognome = reader["Cognome"].ToString();
                    record.Verbale.Importo = Convert.ToDouble(reader["Importo"]);
                    record.Tipo_Violazione.Descrizione = reader["Descrizione"].ToString();
                    lista.Add(record);
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

            return View(lista);
        }

        // L'Action PuntiMaggDieci() restituisce la vista PuntiMaggDieci.cshtml che mostra i verbali con decurtamento punti maggiore o uguale a 10 anche qui uso il modello SharedAnagraficaVerbale
        public ActionResult PuntiMaggDieci()
        {
            SqlConnection conn = Connection.GetConnection();
            List<SharedAnagraficaVerbale> lista = new List<SharedAnagraficaVerbale>();
            try
            {
                conn.Open();
                string query = "SELECT A.Nome, A.Cognome, B.Importo, B.Data_Violazione, B.Decurtamento_Punti FROM Anagrafica AS A INNER JOIN Verbale B ON B.Fk_IdAnagrafica = A.idAnagrafica WHERE Decurtamento_Punti >= 10";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SharedAnagraficaVerbale record = new SharedAnagraficaVerbale
                    {
                        Anagrafica = new Anagrafica(),
                        Verbale = new Verbale()
                    };
                    record.Anagrafica.Nome = reader["Nome"].ToString();
                    record.Anagrafica.Cognome = reader["Cognome"].ToString();
                    record.Verbale.Importo = Convert.ToDouble(reader["Importo"]);
                    record.Verbale.DataViolazione = Convert.ToDateTime(reader["Data_Violazione"]);
                    record.Verbale.PuntiDecurtati = Convert.ToInt32(reader["Decurtamento_Punti"]);
                    lista.Add(record);
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
            return View(lista);
        }

    }
}