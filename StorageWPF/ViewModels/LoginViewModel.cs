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
        private ObservableCollection<User> _users;

        public LoginViewModel()
        {
            _users = JsonUtils.FromJsonFile<ObservableCollection<User>>(typeof(User));
            if (_users == null) _users = new ObservableCollection<User>();
        }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
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
                          if (_users.Any(x => x.Username == _username && x.Password == passwordBox.Password))
                          {
                              MainWindow mainWindow = new MainWindow();
                              mainWindow.DataContext = new MainWindowViewModel(false, _username);
                              var currentWindow = Application.Current.Windows.OfType<Window>()
                          .FirstOrDefault(w => w.IsActive);
                              currentWindow?.Close();
                              mainWindow.Show();
                          }
                          else
                              MessageBox.Show("Incorrect login or password");
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
                        mainWindow.DataContext = new MainWindowViewModel(true, "Guest");
                        var currentWindow = Application.Current.Windows.OfType<Window>()
                        .FirstOrDefault(w => w.IsActive);
                        currentWindow?.Close();
                        mainWindow.Show();
                    }));
            }
        }

    }
}