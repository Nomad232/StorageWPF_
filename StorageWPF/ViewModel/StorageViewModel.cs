using StorageWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StorageWPF.ViewModel
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
