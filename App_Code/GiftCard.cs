using System;
using System.Data;
using System.Data.SqlClient;
using DALLib;

namespace VR_Web_Project
{
    public class GiftCard
    {
        public int Id { get; set; }
        public int Worth { get; set; }
        public string Code { get; set; }
        public bool IsExpired { get; set; }

        public GiftCard(string code)
        {
            Worth = FindWorthByCode(code);
            Code = code;
            IsExpired = Worth==0 ? true : false;
        }

        private int FindWorthByCode(string code)
        {
            string sql = "SELECT Worth FROM GiftCard WHERE Code ='" + code + "'";

            try
            {
                var readerAndConnection = DAL.GetReader(sql);
                SqlDataReader reader = readerAndConnection.Item1;

                int worth = 0;
                if (reader.Read())
                    worth = (int)reader["Worth"];

                reader.Close();
                readerAndConnection.Item2.Close();

                return worth;
            }
            catch
            {
                return 0;
            }

        }
    }
}