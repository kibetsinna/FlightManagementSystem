using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem
{
    public static class SQLConnection
    {
        public static int transferTime = -3;
        public static int threadTime = 86400000;
        public static string conStr= @"Data Source=DESKTOP-7CULLHA\SQLEXPRESS;Initial Catalog=DBFlightManagementSystem;Integrated Security=True";
        public static void SQLOpen(SqlConnection conSQL)
        {
            if (conSQL.State != System.Data.ConnectionState.Open)
                conSQL.Open();
   
        }
        public static void SQLClose(SqlConnection conSQL)
        {
            if (conSQL.State != System.Data.ConnectionState.Closed)
                conSQL.Close();
        }

    
    }
}
