using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace StorageWPF.ViewModels
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
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
