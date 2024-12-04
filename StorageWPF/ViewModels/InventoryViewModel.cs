using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace StorageWPF.ViewModels
{
    internal class InventoryViewModel : ViewModel
    {
        private string _search;
        private ObservableCollection<Product> _products;
        public string _selectedFilter = "None";


        public string[] Filter = { "None", "Name", "Price", "Count", "Unit", "Last date" };
        public InventoryViewModel(ObservableCollection<Product> products)
        {
            _products = products;
        }

        private ObservableCollection<Product> CreateNewCopyOfProducts()
        {
            return new ObservableCollection<Product>(
            _products.Select(product => new Product
            {
                Name = product.Name,
                Price = product.Price
            }));
        }

        public string Search
        {
            get => _search;
            set
            {
                Set(ref _search, value);
                OnPropertyChanged(nameof(Products));
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                Set(ref _selectedFilter, value);
                OnPropertyChanged(nameof(Products));
            }
        }

        public List<Product> Products
        {
            get
            {
                if (string.IsNullOrEmpty(_search) || string.IsNullOrWhiteSpace(_search))
                {
                    switch (_selectedFilter)
                    {
                        case "None":
                            return _products.ToList();
                        case "Name":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Name).ToList();
                        case "Price":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Price).ToList();
                        case "Count":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Count).ToList();
                        case "Unit":
                            return CreateNewCopyOfProducts().OrderBy(x => x.UM).ToList();
                        case "Last date":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Dt).ToList();
                        default:
                            return _products.ToList();
                    }
                }
                else
                {
                    switch (_selectedFilter)
                    {
                        case "None":
                            return CreateNewCopyOfProducts().Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        case "Name":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Name).Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        case "Price":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Price).Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        case "Count":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Count).Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        case "Unit":
                            return CreateNewCopyOfProducts().OrderBy(x => x.UM).Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        case "Last date":
                            return CreateNewCopyOfProducts().OrderBy(x => x.Dt).Where(x => x.Name.Contains(_search, StringComparison.OrdinalIgnoreCase)).ToList();
                        default:
                            return _products.ToList();
                    }
                }
            }
        }     
    }
}
