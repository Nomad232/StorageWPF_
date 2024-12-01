using StorageWPF.Models;
using StorageWPF.Views;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace StorageWPF.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private string _username;
        private string _password;
        private ObservableCollection<User> _users;

        public LoginViewModel()
        {
            // Тут проблема с путем
            _users = JsonUtils.FromJsonFile<ObservableCollection<User>>("../../../Data/Users.json");
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
                      User user = new User(_username, _password);
                      if (_users.Contains(user))
                      {
                          MainWindow mainWindow = new MainWindow();
                          mainWindow.DataContext = new StorageViewModel(true);
                          mainWindow.Show();
                      }
                      else
                          MessageBox.Show("Такого користувача не існує");
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
                        mainWindow.DataContext = new StorageViewModel(false);
                        mainWindow.Show();
                    }));
            }
        }
        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new RelayCommand(obj =>
                    {
                        if (obj is Window window)
                        {
                            window.Close();
                        }
                    }));
            }
        }
        private RelayCommand minimizeCommand;
        public RelayCommand MinimizeCommand
        {
            get
            {
                return minimizeCommand ??
                    (minimizeCommand = new RelayCommand(obj =>
                    {
                        if (obj is Window window)
                        {
                            window.WindowState = WindowState.Minimized;
                        }
                    }));
            }
        }
        private RelayCommand dragCommand;
        public RelayCommand DragCommand
        {
            get
            {
                return dragCommand ??
                    (dragCommand = new RelayCommand(obj =>
                    {
                        if (obj is Window window)
                        {
                            if (Mouse.LeftButton == MouseButtonState.Pressed)
                            {
                                window.DragMove();
                            }
                        }
                    }));
            }
        }
    }
}