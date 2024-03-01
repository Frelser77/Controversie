using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace Controversie.Models
{
    public class DataAccess
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ComuneDiOria"].ConnectionString;

        // ###################### DATI ANAGRAFICA ######################
        public List<Anagrafica> GetAnagrafica()
        {
            List<Anagrafica> list = new List<Anagrafica>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM Anagrafica";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Anagrafica()
                            {
                                IdAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]),
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Citta = reader["Citta"].ToString(),
                                CAP = reader["CAP"].ToString(),
                                Cod_Fisc = reader["Cod_Fisc"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }

        public void AddAnagrafica(Anagrafica anagrafica)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, Cod_Fisc) VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Aggiunta dei parametri in base ai campi
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAnagrafica(Anagrafica anagrafica)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Anagrafica SET Cognome = @Cognome, Nome = @Nome, Indirizzo = @Indirizzo, Citta = @Citta, CAP = @CAP, Cod_Fisc = @Cod_Fisc WHERE IdAnagrafica = @IdAnagrafica";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Aggiunta dei parametri in base ai campi
                command.Parameters.AddWithValue("@IdAnagrafica", anagrafica.IdAnagrafica);
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAnagrafica(int idAnagrafica)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Anagrafica WHERE IdAnagrafica = @IdAnagrafica";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@IdAnagrafica", idAnagrafica);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // DATI VERBALE 
        public List<Verbale> GetVerbali()
        {
            List<Verbale> list = new List<Verbale>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Verbale ORDER BY DataViolazione DESC";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Verbale()
                        {
                            IdVerbale = Convert.ToInt32(reader["IdVerbale"]),
                            DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                            IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                            NominativoAgente = reader["NominativoAgente"].ToString(),
                            DataTrascrizione = Convert.ToDateTime(reader["DataTrascrizione"]),
                            Importo = Convert.ToDecimal(reader["Importo"]),
                            DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]),
                            Fk_IdAnagrafica = Convert.ToInt32(reader["fk_IdAnagrafica"]),
                            Fk_IdViolazione = Convert.ToInt32(reader["fk_IdViolazione"]),
                            Pagata = Convert.ToBoolean(reader["Pagata"])
                        });
                    }
                }
            }

            return list;
        }

        public Verbale GetVerbaleById(int idVerbale)
        {
            Verbale verbale = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Verbale WHERE IdVerbale = @IdVerbale";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@IdVerbale", idVerbale);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        verbale = new Verbale
                        {
                            IdVerbale = Convert.ToInt32(reader["IdVerbale"]),
                            DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                            IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                            NominativoAgente = reader["NominativoAgente"].ToString(),
                            DataTrascrizione = Convert.ToDateTime(reader["DataTrascrizione"]),
                            Importo = Convert.ToDecimal(reader["Importo"]),
                            DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]),
                            Fk_IdAnagrafica = Convert.ToInt32(reader["fk_IdAnagrafica"]),
                            Fk_IdViolazione = Convert.ToInt32(reader["fk_IdViolazione"]),
                            Pagata = Convert.ToBoolean(reader["Pagata"])
                        };
                    }
                }
            }

            return verbale;
        }

        public void AddVerbale(Verbale verbale)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"INSERT INTO Verbale ( DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizione, Importo, DecurtamentoPunti, fk_IdAnagrafica, fk_IdViolazione, Pagata) 
                                    VALUES ( @DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizione, @Importo, @DecurtamentoPunti, @Fk_IdAnagrafica, @Fk_IdViolazione, @Pagata)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                command.Parameters.AddWithValue("@DataTrascrizione", verbale.DataTrascrizione);
                command.Parameters.AddWithValue("@Importo", verbale.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                command.Parameters.AddWithValue("@Fk_IdAnagrafica", verbale.Fk_IdAnagrafica);
                command.Parameters.AddWithValue("@Fk_IdViolazione", verbale.Fk_IdViolazione);
                command.Parameters.AddWithValue("@Pagata", verbale.Pagata ? 1 : 0);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateVerbale(Verbale verbale)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Verbale SET DataViolazione = @DataViolazione, IndirizzoViolazione = @IndirizzoViolazione, NominativoAgente = @NominativoAgente, DataTrascrizione = @DataTrascrizione, Importo = @Importo, DecurtamentoPunti = @DecurtamentoPunti, fk_IdAnagrafica = @Fk_IdAnagrafica, fk_IdViolazione = @Fk_IdViolazione, Pagata = @Pagata WHERE IdVerbale = @IdVerbale";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                command.Parameters.AddWithValue("@DataTrascrizione", verbale.DataTrascrizione);
                command.Parameters.AddWithValue("@Importo", verbale.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                command.Parameters.AddWithValue("@Fk_IdAnagrafica", verbale.Fk_IdAnagrafica);
                command.Parameters.AddWithValue("@Fk_IdViolazione", verbale.Fk_IdViolazione);
                command.Parameters.AddWithValue("@Pagata", verbale.Pagata ? 1 : 0);
                command.Parameters.AddWithValue("@IdVerbale", verbale.IdVerbale);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteVerbale(int idVerbale)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Verbale WHERE IdVerbale = @IdVerbale";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@IdVerbale", idVerbale);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void VerbaleAsPaid(int idVerbale)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Verbale SET Pagata = 1 WHERE IdVerbale = @IdVerbale";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@IdVerbale", idVerbale);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // ###################### METODI PER LE VIOLAZIONI 

        public IEnumerable<Violazione> GetViolazioniContestabili()
        {
            List<Violazione> violazioni = new List<Violazione>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT IdViolazione, Descrizione, Contestabile, DecurtamentoPunti FROM TipoViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        violazioni.Add(new Violazione
                        {
                            IdViolazione = reader.GetInt32(reader.GetOrdinal("IdViolazione")),
                            Descrizione = reader.GetString(reader.GetOrdinal("Descrizione")),
                            Contestabile = reader.GetBoolean(reader.GetOrdinal("Contestabile")),
                            PuntiDecurtati = reader.GetInt32(reader.GetOrdinal("DecurtamentoPunti"))
                        });
                    }
                }
            }
            return violazioni;
        }


        public void AddViolazione(Violazione violazione)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                INSERT INTO TipoViolazione (Descrizione, Contestabile, DecurtamentoPunti)
                VALUES (@Descrizione, @Contestabile, @PuntiDecurtati)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
                command.Parameters.AddWithValue("@Contestabile", violazione.Contestabile);
                command.Parameters.AddWithValue("@PuntiDecurtati", violazione.PuntiDecurtati);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public Violazione GetViolazioneById(int id)
        {
            Violazione violazione = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM TipoViolazione WHERE IdViolazione = @IdViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@IdViolazione", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        violazione = new Violazione
                        {
                            IdViolazione = Convert.ToInt32(reader["IdViolazione"]),
                            Descrizione = reader["Descrizione"].ToString(),
                            Contestabile = Convert.ToBoolean(reader["Contestabile"])
                        };
                    }
                }
            }
            return violazione;
        }

        public void UpdateViolazione(Violazione violazione)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                UPDATE TipoViolazione
                SET Descrizione = @Descrizione, Contestabile = @Contestabile, DecurtamentoPunti = @PuntiDecurtati
                WHERE IdViolazione = @IdViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
                command.Parameters.AddWithValue("@Contestabile", violazione.Contestabile);
                command.Parameters.AddWithValue("@IdViolazione", violazione.IdViolazione);
                command.Parameters.AddWithValue("@PuntiDecurtati", violazione.PuntiDecurtati);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Violazione> GetTipiViolazione()
        {
            List<Violazione> tipiViolazione = new List<Violazione>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "SELECT idviolazione, Descrizione, DecurtamentoPunti FROM TipoViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idViolazione = int.Parse(reader["idviolazione"].ToString());
                        string descrizione = reader["Descrizione"].ToString();
                        int decurtamentoPunti = int.Parse(reader["DecurtamentoPunti"].ToString());

                        Violazione violazione = new Violazione(idViolazione, descrizione, decurtamentoPunti);
                        tipiViolazione.Add(violazione);
                    }
                }
            }
            return tipiViolazione;
        }


        public void DeleteViolazione(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM TipoViolazione WHERE idviolazione = @IdViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@IdViolazione", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void ToggleContestabile(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                UPDATE TipoViolazione
                SET Contestabile = ~Contestabile
                WHERE idviolazione = @IdViolazione";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@IdViolazione", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // ###################### METODI PER CONTROLLO DATI ######################

        public List<TrasgressoreReport> GetTotaleVerbaliPerTrasgressore()
        {
            List<TrasgressoreReport> risultati = new List<TrasgressoreReport>();
            string query = @"SELECT a.Cognome, a.Nome, COUNT(v.IdVerbale) AS TotaleVerbali
                      FROM Anagrafica a
                      JOIN Verbale v ON a.IdAnagrafica = v.fk_IdAnagrafica
                      GROUP BY a.Cognome, a.Nome";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        risultati.Add(new TrasgressoreReport
                        {
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Totale = (int)reader["TotaleVerbali"]
                        });
                    }
                    return risultati;
                }
            }
        }

        public List<TrasgressoreReport> GetTotalePuntiDecurtatiPerTrasgressore()
        {
            List<TrasgressoreReport> risultati = new List<TrasgressoreReport>();
            string query = @"SELECT a.Cognome, a.Nome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati
                  FROM Anagrafica a
                  JOIN Verbale v ON a.IdAnagrafica = v.fk_IdAnagrafica
                  GROUP BY a.Cognome, a.Nome";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        risultati.Add(new TrasgressoreReport
                        {
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Totale = (int)reader["TotalePuntiDecurtati"] // Qui usiamo Totale per rappresentare TotalePuntiDecurtati
                        });
                    }
                }
            }
            return risultati;
        }
        public List<ViolazioneReport> GetViolazioniOltre10Punti()
        {
            List<ViolazioneReport> risultati = new List<ViolazioneReport>();
            string query = @"SELECT a.Cognome, a.Nome, v.DataViolazione, v.Importo, v.DecurtamentoPunti
                  FROM Verbale v
                  JOIN Anagrafica a ON v.fk_IdAnagrafica = a.IdAnagrafica
                  WHERE v.DecurtamentoPunti > 10";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        risultati.Add(new ViolazioneReport
                        {
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            DataViolazione = (DateTime)reader["DataViolazione"],
                            Importo = (decimal)reader["Importo"],
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                        });
                    }
                }
            }
            return risultati;
        }
        public List<ViolazioneReport> GetViolazioniImportoMaggiore400()
        {
            List<ViolazioneReport> risultati = new List<ViolazioneReport>();
            string query = @"SELECT a.Cognome, a.Nome, v.DataViolazione, v.Importo, v.DecurtamentoPunti
                  FROM Verbale v
                  JOIN Anagrafica a ON v.fk_IdAnagrafica = a.IdAnagrafica
                  WHERE v.Importo > 400";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        risultati.Add(new ViolazioneReport
                        {
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            DataViolazione = (DateTime)reader["DataViolazione"],
                            Importo = (decimal)reader["Importo"],
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                        });
                    }
                }
            }
            return risultati;
        }


    }
}

