using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Entity;

namespace DesktopRepository
{
    public static class MysqlDbContext
    {
        private static string ConnectionString { get; set; }
        private static MySqlConnection connection { get; set; }

        static MysqlDbContext()
        {
            //ConnectionString = "Server=54.36.121.209;Database=banksystemdb;UID=dotcomuser;Password=dotcomuser123;SslMode=none;Convert Zero Datetime=True;Allow Zero Datetime=True";
            //GetConnection();
        }

        public static void Initialize()
        {
            ConnectionString = "Server=54.36.121.209;Database=banksystemdb;UID=dotcomuser;Password=dotcomuser123;SslMode=none;Convert Zero Datetime=True;Allow Zero Datetime=True";
            GetConnection();
        }
        
        private static void GetConnection()
        {
            connection = new MySqlConnection(ConnectionString);
        }

        public static MySqlConnection ShareConnetction()
        {
            return connection;
        }

    }
}
