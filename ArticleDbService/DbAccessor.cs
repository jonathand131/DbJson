using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DbJsonServer
{
    class DbAccessor
    {
        private static readonly string connectionString = "SERVER=127.0.0.1; DATABASE=jdsystem_pciflux; UID=csharp; PASSWORD=testC#";
        private static readonly string[] validTables = { "users" };

        private MySqlConnection mysqlDb;

        public DbAccessor()
        {
            mysqlDb = new MySqlConnection(connectionString);
        }

        public List<Article> GetArticlesById(Int64 id)
        {
            // Open database connection
            mysqlDb.Open();

            // Prepare query
            MySqlCommand rqt = mysqlDb.CreateCommand();
            rqt.CommandText = "SELECT id, title, sub_title, author, date, content FROM pci_articles WHERE id = @id ORDER BY date DESC LIMIT 100";
            rqt.Parameters.AddWithValue("id", id);

            // Execute query
            List<Article> results = GetArticlesFromCommand(rqt);

            // Close database connection
            mysqlDb.Close();

            // Return results
            return results;
        }

        public List<Article> GetArticlesByDateRange(DateTime start, DateTime end)
        {
            // Open database connection
            mysqlDb.Open();

            // Prepare query
            MySqlCommand rqt = mysqlDb.CreateCommand();
            rqt.CommandText = "SELECT id, title, sub_title, author, date, content FROM pci_articles WHERE date >= @date_start AND date <= @date_end ORDER BY date DESC LIMIT 100";
            rqt.Parameters.AddWithValue("date_start", start);
            rqt.Parameters.AddWithValue("date_end", end);

            // Execute query
            List<Article> results = GetArticlesFromCommand(rqt);

            // Close database connection
            mysqlDb.Close();

            // Return results
            return results;
        }

        public List<Article> GetLastArticlesList()
        {
            // Open database connection
            mysqlDb.Open();

            // Prepare query
            MySqlCommand rqt = mysqlDb.CreateCommand();
            rqt.CommandText = "SELECT id, title, sub_title, author, date FROM pci_articles ORDER BY date DESC LIMIT 100";

            // Execute query
            List<Article> results = GetArticlesFromCommand(rqt);

            // Close database connection
            mysqlDb.Close();

            // Return results
            return results;
        }

        protected List<Article> GetArticlesFromCommand(MySqlCommand command)
        {
            // Execute query
            MySqlDataReader dbReader = command.ExecuteReader();

            // Prepare results
            List<Article> results = new List<Article>();
            while (dbReader.Read())
            {
                results.Add(new Article(dbReader));
            }

            // Close query result reader
            dbReader.Close();

            return results;
        }
    }
}
