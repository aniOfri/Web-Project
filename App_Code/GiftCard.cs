using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DALLib;

namespace VR_Web_Project
{
    public class GiftCard : Order
    {
        public int Id { get; set; }
        public int ReceiptID { get; set; }
        public int Worth { get; set; }
        public string Code { get; set; }
        public bool IsExpired { get; set; }

        public GiftCard(int price2) : base(price2)
        {
            Id = CountForId();
        }
        public GiftCard(string code, int price = 0) : base(price)
        {
            this.Worth = FindWorthByCode(code);
            this.Code = code;
            this.IsExpired = Worth==0 ? true : false;
            this.ReceiptID = FindReceiptIdByCode(code);
        }

        private int FindReceiptIdByCode(string code)
        {
            string sql = "SELECT ReceiptID FROM GiftCard WHERE Code ='" + code + "'";

            try
            {
                var readerAndConnection = DAL.GetReader(sql);
                SqlDataReader reader = readerAndConnection.Item1;

                int id = 0;
                if (reader.Read())
                    id = (int)reader["ReceiptID"];

                reader.Close();
                readerAndConnection.Item2.Close();

                return id;
            }
            catch
            {
                return -1;
            }
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

        // A private function which returns the next Id
        // INPUT: none
        // OUTPUT: int as the Id
        public int CountForId()
        {
            string sql = "SELECT COUNT(*) FROM GiftCard";
            return DAL.ExecuteCounting(sql);
        }
        // A private function which returns a unique Gift Card code
        // INPUT: none
        // OUTPUT: string as code
        private string GenCode()
        {
            Random rnd = new Random();
            SHA1Managed sha1 = new SHA1Managed();
            string toHash;
            toHash = rnd.Next(11111111, 99999999).ToString();

            byte[] raw = Encoding.Default.GetBytes(toHash);
            var hash = sha1.ComputeHash(raw);
            
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            string Code = sb.ToString().Substring(0, 7);
            return Code;
        }
        public bool Insert()
        {
            Code = GenCode();

            // BUILD STRING AS AN SQL COMMAND
            string sql = "INSERT INTO GiftCard (Id, Worth, Code, IsExpired, ReceiptID) VALUES (\'";
            sql += CountForId() + "\', \'" + Worth + "\', \'" + Code + "\', \'" + false + "\', \'" + ReceiptID + "\')";

            // EXECUTE COMMAND AND RETURN TRUE IF SUCESS AND FAIL OTHERWISE
            try
            {
                DAL.ExecNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}