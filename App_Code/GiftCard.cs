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
            this.Worth = FindWorthByCode(code);
            this.Code = code;
            this.IsExpired = Worth==0 ? true : false;
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

        public static int ApplyGiftCard(int price, string code)
        {
            string sql = "SELECT Worth FROM GiftCard WHERE Code ='" + code + "'";

            // GET READER USING DAL
            var readerAndConnection = DAL.GetReader(sql);
            SqlDataReader reader = readerAndConnection.Item1;

            // ASSIGN DATA TO Balance
            int balance = 0;
            if ((bool)reader.Read())
                balance = (int)reader["Balance"];

            // CLOSE
            reader.Close();
            readerAndConnection.Item2.Close();

            // SET NEWBALANCE AS THE NEW VALUE FOR THE DB
            int newWorth = balance - price;
            int newPrice = price - balance;
            if (newWorth > 0)
            {
                string updateSql = "UPDATE GiftCard SET Worth = '" + newWorth + "'"
                     + " WHERE Code='"+code+"'";

                DAL.ExecNonQuery(updateSql);
                return newPrice;
            }
            return price;

        }

    }
}