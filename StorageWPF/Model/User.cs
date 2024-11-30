using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.Model
{
    internal class User
    {
        private string username;
        private string password;

        public string Username { get { return username; } }
        public string Password { get { return password; } }        
    }
}