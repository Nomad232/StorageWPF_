using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StorageWPF.ViewModels
{
    internal class GeneralViewModel: ViewModel
    {
        public string[] Lables => Products?.Select(e => e.Name).ToArray();
        public double[] Values => Products?.Select(e => (double)e.Count).ToArray();
        public ObservableCollection<Product> Products { get; set; }
        public GeneralViewModel(ObservableCollection<Product> products)
        {
            Products = products;
            OnPropertyChanged("Lables");
            OnPropertyChanged("Values");
        }
    }
}
