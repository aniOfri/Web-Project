using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace VR_Web_Project
{
    public class DAL
    {
        private string ConnectionString { get; set; }

        public DAL()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        // GetTable
        public DataTable GetTable()
        // SetItem
    }
}