using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.ViewModels
{
    internal class GeneralViewModel: ViewModel
    {
        public ObservableCollection<Product> Products { get; }
        public GeneralViewModel(ObservableCollection<Product> Products)
        {
            this.Products = Products;
        }
    }
}
