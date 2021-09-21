using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VR_Web_Project
{
    public class DAL
    {
        // A static function which returns an SqlConnection with the connection string for web.config
        // INPUT: none
        // OUTPUT: SqlConnection as a connection to the db
        private static SqlConnection ConnectToDb()
        {
            // GET CONNECTION STRING FROM WEBCONFIG
            string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            
            // DECLARE CONNECTION USING CONNECTION STRING AND RETURN VALUE
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }

        // A static function which execute the command from the input on the database
        // INPUT: string as an sql command
        // OUTPUT: void
        public static void ExecNonQuery(string sql)
        {
            // GET CONNECTION AND OPEN IT
            SqlConnection conn = ConnectToDb();
            conn.Open();

            // DECLARE COMMAND TYPE AND EXECUTE
            SqlCommand com = new SqlCommand(sql, conn);
            com.ExecuteNonQuery();

            // CLOSE AND DISPOSE
            conn.Close();
            conn.Dispose();
        }

        // A static function which checks if a specific data exists in the database
        // INPUT: string as an sql command
        // OUTPUT: bool as an answer
        public static bool IsExist(string sql)
        {
            // GET CONNECTION AND OPEN IT
            SqlConnection conn = ConnectToDb();
            conn.Open();

            // DECLARE COMMAND TYPE FOR THE READER
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataReader data = com.ExecuteReader();

            // DECLARE found VARIABLE FOR THE FINAL VALUE
            bool found;
            found = (bool)data.Read();

            // CLOSE, DISPOSE AND RETURN
            conn.Close();
            conn.Dispose();
            return found;
        }

        // A static function which returns a data table from the database
        // INPUT: string as an sql command
        // OUTPUT: DataTable as a data table
        public static DataTable ExecuteDataTable(string sql)
        {
            // GET CONNECTION AND OPEN IT
            SqlConnection conn = ConnectToDb();
            conn.Open();

            // DECLARE DATAADAPTER TYPE FOR THE DATATABLE
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            // FILL THE ADAPTER
            tableAdapter.Fill(dt);

            // CLOSE, DISPOSE AND RETURN
            conn.Close();
            conn.Dispose();
            return dt;
        }

        // A static function which returns a counting of items from the database
        // INPUT: string as an sql command (COUNT)
        // OUTPUT: int as a count
        public static int ExecuteCounting(string sql)
        {
            // GET CONNECTION AND OPEN IT
            SqlConnection conn = ConnectToDb();
            conn.Open();

            // DECLARE COMMAND TYPE AND EXECUTE
            SqlCommand com = new SqlCommand(sql, conn);
            int scalar = (int)com.ExecuteScalar();

            // CLOSE, DISPOSE AND RETURN
            conn.Close();
            conn.Dispose();
            return scalar;
        }


        // A static function which return a reader of the database
        // INPUT: string as an sql command
        // OUTPUT: SqlDataReader as a reader
        public static SqlDataReader GetReader(string sql)
        {
            // GET CONNECTION AND OPEN IT
            SqlConnection conn = ConnectToDb();
            conn.Open();
            // DECLARE COMMAND TYPE FOR THE READER
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();

            // *for the reader to function properly i mustn't close nor dispose the connection
            // conn.Close();
            // conn.Dispose();

            // RETURN
            return reader;
        }
    }
}