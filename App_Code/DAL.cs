using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class DAL
    {
        // GetConnection
        private static SqlConnection ConnectToDb()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }

        // ExecNonQuery
        public static void ExecNonQuery(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            com.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        // IsExist

        public static bool IsExist(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataReader data = com.ExecuteReader();
            bool found;
            found = (bool)data.Read();
            conn.Close();
            return found;
        }

        // DataTable
        public static DataTable ExecuteDataTable(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            return dt;
        }

        // ExecuteScalar
        public static int ExecuteScalar(string sql)
        {
            SqlConnection conn = ConnectToDb();
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            int scalar = (int)com.ExecuteScalar();
            return scalar;
        }


        // GetReader
        public static SqlDataReader GetReader(string sql)
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