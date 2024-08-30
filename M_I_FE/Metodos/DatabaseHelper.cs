using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace M_I_FE.Metodos
{
    public class DatabaseHelper
    {
        private static readonly string connectionString = @"Data Source=E:\Proyectos\M_I_FE\M_I_FE\Datos\MIFE_DB.db;";

        public static List<Dictionary<string, string>> GetComprobante()
        {
            var combinedResults = new List<Dictionary<string, string>>();
            var ids = GetIDs(); // Obtener IDs

            if (ids.Count == 0)
                return combinedResults;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                foreach (var id in ids)
                {
                    // Ejecutar consultas separadas
                    var comprobanteResult = GetDataFromQuery(connection, $"SELECT * FROM Comprobante WHERE ID = {id}");
                    var comprobanteAResult = GetDataFromQuery(connection, $"SELECT CA.* from Comprobante C join ComprobanteA CA on CA.ID = C.ComprobanteA where C.ID = {id}");
                    var comprobanteBResult = GetDataFromQuery(connection, $"SELECT CB.* from Comprobante C join ComprobanteB CB on CB.ID = C.ComprobanteB where C.ID = {id}");
                    var comprobanteCResult = GetDataFromQuery(connection, $"SELECT CC.* from Comprobante C join ComprobanteC CC on CC.ID = C.ComprobanteC where C.ID = {id}");

                    // Combinar resultados en combinedResult
                    var combinedDict = comprobanteResult.Concat(comprobanteAResult).Concat(comprobanteBResult).Concat(comprobanteCResult)
                                                   .ToDictionary(pair => pair.Key, pair => pair.Value);

                    combinedResults.Add(combinedDict);
                }
            }

            return combinedResults;
        }

        private static List<int> GetIDs()
        {
            const string query = "SELECT ID FROM Comprobante WHERE EstadoEnvio = 0;";
            var ids = new List<int>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(reader.GetInt32(0));
                    }
                }
            }

            return ids;
        }

        private static Dictionary<string, string> GetDataFromQuery(SqliteConnection connection, string query)
        {
            var result = new Dictionary<string, string>();

            using (var command = new SqliteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        var value = reader.GetValue(i);
                        if (columnName != "ID")
                        {
                            result[columnName] = value == DBNull.Value ? null : value.ToString();
                        }
                    }
                }
            }

            return result;
        }
    }
}
