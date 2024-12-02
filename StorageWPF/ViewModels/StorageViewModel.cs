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

        

        //RelayCommand addCommand;
        //public RelayCommand AddCommand

        //RelayCommand removeCommand;
        //public RelayCommand RemoveCommand

    }
}
