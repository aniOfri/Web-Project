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

        public int ApplyGiftCard(int price)
        {
            // SET NEWBALANCE AS THE NEW VALUE FOR THE DB
            int newWorth = Worth - price;
            int newPrice = price < Worth ? 0 : price - Worth;
            if (newWorth > 0)
            {
                string updateSql = "UPDATE GiftCard SET Worth = '" + newWorth + "'"
                     + " WHERE Code='"+Code+"'";

                DAL.ExecNonQuery(updateSql);
                return newPrice;
            }
            else
            {
                string updateSql = "UPDATE GiftCard SET Worth = '" + 0 + "'"
                + " WHERE Code='" + Code + "'";

                DAL.ExecNonQuery(updateSql);

                updateSql = "UPDATE GiftCard SET IsExpired = '" + true + "'"
                    + " WHERE Code='" + Code + "'";

                DAL.ExecNonQuery(updateSql);
                return newPrice;
            }

        }

    }
}