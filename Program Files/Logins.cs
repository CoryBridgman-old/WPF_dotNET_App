using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm___Cory_Bridgman_991199354
{
    public class Logins
    {
        private int _id;
        private string _username;
        private string _password;
        private int _superUser;

        public Logins(int id, string username, string password, int isSuper)
        {
            _id = id;
            _username = username;
            _password = password;
            _superUser = isSuper;
        }

        public int ID
        {
            get { return _id; }
        }
        public string Username
        {
            get { return _username; }
        }
        public string Password
        {
            get { return _password; }
        }
        public int Super
        {
            get { return _superUser; }
        }
    }
}
