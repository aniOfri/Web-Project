using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VR_Web_Project
{
    public class User
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private bool IsManager { get; set; }

        public User(int id, string username, string password, bool isManager)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.IsManager = isManager;
        }
    }
}