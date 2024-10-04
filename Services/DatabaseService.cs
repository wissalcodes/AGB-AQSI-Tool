using AGB_AQSI_ExcelTool.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AGB_AQSI_ExcelTool.Services
{
    public class DatabaseService
    {
        private string connectionString = "Server=WISSAL\\SQLEXPRESS;Database=ExcelTool;Trusted_Connection=True;";

        // retourne le nom d'utilisateur pour une demande donnee
        public Testeur GetTesteurNomByDemandeId(int idDemande)
        {
            Testeur testeur = null;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("dbo.SpFetchTesteurByDemande", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_demande", idDemande);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));
                            string nomTesteur = reader.IsDBNull(reader.GetOrdinal("username")) ? string.Empty : reader.GetString(reader.GetOrdinal("username"));

                            testeur = new Testeur(id, nomTesteur);
                        }
                    }
                }
            }
            return testeur;
        }
        // retourne la liste des nom d'utilisateurs des testeurs
        public ObservableCollection<string> FetchTesteurs()
        {
            ObservableCollection<string> testeurs = new ObservableCollection<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SpFetchUsernames", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("username"));
                        testeurs.Add( name);
                    }
                }
            }

            return testeurs;
        }

        // affecte une demande a un testeur
        public void AffecterDemande( int IdDemande, string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SpCreateAffectation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DemandeId", IdDemande);
                command.Parameters.AddWithValue("@Username", username);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // cherche la liste des testeurs 
        public ObservableCollection<Testeur> FetchTesteursInfo()
        {
            ObservableCollection<Testeur> testeurs = new ObservableCollection<Testeur>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SpFetchTesteurs", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                        DateTime dateCreation = reader.GetDateTime(reader.GetOrdinal("date_creation"));
                        string username = reader.GetString(reader.GetOrdinal("username"));
                        testeurs.Add(new Testeur(nom, prenom, dateCreation, username));
                    }
                }
            }

            return testeurs;
        }

        // cree un nouveau testeur
        public void CreateTesteur(string username, string nom, string prenom)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SpAddUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Nom", nom);
                command.Parameters.AddWithValue("@Prenom", prenom);

                var resultMessageParam = new SqlParameter("@ResultMessage", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(resultMessageParam);

                command.ExecuteNonQuery();
                string resultMessage = resultMessageParam.Value.ToString();

                MessageBox.Show(resultMessage);

                connection.Close();
            }
        }

        // supprime un testeur
        public void SupprimerTesteur(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SpDeleteTesteur", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Username", username));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        
        // supprime plusieurs testeurs
        public void SupprimerPlusieursTesteurs(string usernames)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SpDeletePlusieursTesteurs", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Usernames", usernames));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public ObservableCollection<Affectation> FetchAffectations()
        {
            ObservableCollection<Affectation> affectations = new ObservableCollection<Affectation>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SpFetchHistory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idDemande = reader.GetInt32(reader.GetOrdinal("id_demande"));
                        string username = reader.GetString(reader.GetOrdinal("username"));
                        DateTime dateCreation = reader.GetDateTime(reader.GetOrdinal("date_creation"));
                        affectations.Add(new Affectation(idDemande, username, dateCreation));
                    }
                }
            }

            return affectations;
        }
    }
}
