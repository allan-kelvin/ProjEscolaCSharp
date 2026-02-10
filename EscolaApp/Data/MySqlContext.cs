using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaApp.Data
{
    class MySqlContext
    {
        private readonly string _connectionString =
           "Server=localhost;Database=escola_db;User=root;Password=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
