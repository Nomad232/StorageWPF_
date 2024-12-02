using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.ViewModels
{
    internal class InventoryViewModel : ViewModel
    {
        private string search;
        private ObservableCollection<Product> List { get; set; }

        public ObservableCollection<Product> Products { get; }
        public InventoryViewModel(ObservableCollection<Product> Products) 
        {
            this.Products = Products;
            List = Products;
        }


        public string Search
        {
            get => search;
            set
            {
                Set(ref search, value);
                UpdateList();
                // filtr
            }
        }

        public void UpdateList()
        {
            if (string.IsNullOrWhiteSpace(Search))
                List = Products;
            //+ фільтр
            else
            {
                List = new ObservableCollection<Product>(Products.Where(el => el.Name.ToLower().Contains(search.ToLower())));
            }

        }

    }
}
