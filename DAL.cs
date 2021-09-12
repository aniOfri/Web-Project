using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class DAL
    {
        private string ConnectionString { get; set; }

        public DAL()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        // GetConnection
        public SqlConnection ConnectToDb()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;

        }


        // ExecNonQuery

        public void ExecNonQuery(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            com.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }
        
        // GetReader
        public SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            //conn.Close();
            //conn.Dispose();
            return reader;
        }
    }
}