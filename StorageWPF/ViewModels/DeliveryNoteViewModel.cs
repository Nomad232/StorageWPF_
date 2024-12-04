using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.ViewModels
{
    internal class DeliveryNoteViewModel : ViewModel
    {
        private Product _selectedProduct = new Product();
        public ObservableCollection<Product> DeliveryProducts;
        private ObservableCollection<Product> _products;

        public Units_Of_Measurement[] AllUnits => (Units_Of_Measurement[])Enum.GetValues(typeof(Units_Of_Measurement));
        public DeliveryNoteViewModel()
        {
            _products = new ObservableCollection<Product>();
            DeliveryProducts = new ObservableCollection<Product>();
        }

        public DeliveryNoteViewModel(ObservableCollection<Product> products)
        {
            _products = products;
            DeliveryProducts = new ObservableCollection<Product>();
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }

        public string CurrentName
        {
            get => _selectedProduct.Name;
            set
            {
                _selectedProduct.Name = value;
                OnPropertyChanged(nameof(CurrentName));
            }
        }
        public double CurrentPrice
        {
            get => _selectedProduct.Price;
            set
            {
                _selectedProduct.Price = value;
                OnPropertyChanged(nameof(CurrentPrice));
            }
        }
        public int CurrentCount
        {
            get => _selectedProduct.Count;
            set
            {
                _selectedProduct.Count = value;
                OnPropertyChanged(nameof(CurrentCount));
            }
        }

        public Units_Of_Measurement CurrentUnit
        {
            get => _selectedProduct.UM;
            set
            {
                _selectedProduct.UM = value;
                OnPropertyChanged(nameof(CurrentUnit));
            }
        }
        public DateTime CurrentDate
        {
            get => _selectedProduct.Dt;
            set
            {
                _selectedProduct.Dt = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
    }
}
