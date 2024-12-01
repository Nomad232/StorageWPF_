using StorageWPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows;
using System.Xml.Serialization;

namespace StorageWPF.ViewModels
{
    internal class StorageViewModel : ViewModel
    {
        public bool User { get; }
        public ObservableCollection<Product> Products { get; set; }

        public StorageViewModel(bool user)
        {
            this.User = user;
            using (FileStream f = new FileStream("Products.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Product>));
                Products = (ObservableCollection<Product>)xmlSerializer.Deserialize(f);
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

        //RelayCommand addCommand;
        //public RelayCommand AddCommand

        //RelayCommand removeCommand;
        //public RelayCommand RemoveCommand

    }
}
