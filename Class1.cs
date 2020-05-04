using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem
{
    public class Class1
    {
        public string Test1(int status)
        {
            string result = "";
            //SqlConnection testCon = new SqlConnection(@"Data Source=DESKTOP-7CULLHA\SQLEXPRESS;Initial Catalog=DBFlightManagementSystem;Integrated Security=True");
            SqlConnection testCon = new SqlConnection(SQLConnection.conStr);
            testCon.Open();
            string str=$"SELECT FLIGHT_STATUS FROM FlightStatus WHERE ID={status}";
            using (SqlCommand cmd = new SqlCommand(str, testCon))
            {
               using(SqlDataReader dr=cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        dr.Read();
                        result = (string)dr["FLIGHT_STATUS"];
                    }
                }
            }
            testCon.Close();
            return result;
        }
    }
}
