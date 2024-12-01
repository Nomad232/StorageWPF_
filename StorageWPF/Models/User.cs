using System.ComponentModel;

namespace StorageWPF.Models
{
    public class User : Model
    {
        private string _username;
        private string _password;

        public User() { }
        public User(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }
        public string Password
        {
            get => _password; 
            set => Set(ref _password, value);
        }
    }
}