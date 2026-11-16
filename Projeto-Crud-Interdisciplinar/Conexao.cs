using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace TesteCrudDelivery
{
    // 12500445 
    // Leonardo Magno De Oliveira Silva Filho
    // 2D1
    public class Conexao
    {
        public object ExecutarQuery(string querry, object[] valores = default)
        {
            string connectionConfig = "Persist Security Info = false;" +
                            "server = localhost;" +
                            "port = 3306;" +
                            "database = projeto_interdisciplinar;" +
                            "uid = root; " +
                            "pwd = root;" +
                            "TreatTinyAsBoolean=false;";
            using (MySqlConnection connection = new MySqlConnection(connectionConfig))
            {

                try
                {
                    using (MySqlCommand comando = new MySqlCommand(querry, connection))
                    {
                        MatchCollection correspondencias = Regex.Matches(querry, @"@\w+");
                        for (int i = 0; i < correspondencias.Count; i++)
                        {
                            comando.Parameters.AddWithValue(correspondencias[i].Value, valores[i] ?? DBNull.Value);
                        }
                        connection.Open();
                        string queryTexto = querry.Trim().ToUpper();

                        if (queryTexto.StartsWith("SELECT"))
                        {
                            MySqlDataAdapter da = new MySqlDataAdapter(comando);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                        else
                        {
                            comando.ExecuteNonQuery();
                            return comando.LastInsertedId;
                        }
                    }
                }

                catch (MySqlException ex)
                {
                    throw new Exception($"erro: {ex.Message}");
                }

            }
        }
    }
}
