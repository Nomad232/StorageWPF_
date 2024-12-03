using StorageWPF.Models;
using StorageWPF.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StorageWPF.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private string _username;
        private string _password;
        private ObservableCollection<User> _users;

        public LoginViewModel()
        {
            _users = JsonUtils.FromJsonFile<ObservableCollection<User>>(typeof(User));
            if (_users == null)
            {
                MessageBox.Show("Відсутній файл Users.json");
                Application.Current.Shutdown();
            }
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

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(obj =>
                  {
                      if (obj is PasswordBox passwordBox)
                      {
                          User user = new User(_username, passwordBox.Password);
                          if (_users.Contains(user))
                          {
                              MainWindow mainWindow = new MainWindow();
                              mainWindow.DataContext = new MainWindowViewModel(true, user.Username);
                              mainWindow.Show();
                          }
                          else
                              MessageBox.Show("No such user exists");
                      }                      
                  }));
            }
        }
        private RelayCommand guestCommand;
        public RelayCommand GuestCommand
        {
            get
            {
                return guestCommand ??
                    (guestCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.DataContext = new MainWindowViewModel(false, "Guest");
                        mainWindow.Show();
                    }));
            }
        }
        
    }
}