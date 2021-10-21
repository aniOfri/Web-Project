using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DALLib;

namespace VR_Web_Project.App_Code
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

            var readerAndConnection = DAL.GetReader(sql);
            SqlDataReader reader = readerAndConnection.Item1;

            int worth = 0;
            if ((bool)reader.Read())
                worth = (int)reader["Worth"];

            reader.Close();
            readerAndConnection.Item2.Close();

            return worth;
        }
    }
}