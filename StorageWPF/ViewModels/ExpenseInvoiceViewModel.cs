using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageWPF.ViewModels
{
    internal class ExpenseInvoiceViewModel : ViewModel
    {
        private Product _selectedProductForRemove;
        private Product _selectedProduct;
        private int _newCount = 0;
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> CopyProducts { get; set; }
        public ObservableCollection<Product> NewProducts { get; set; }

        public ExpenseInvoiceViewModel(ObservableCollection<Product> products)
        {
            _products = products;
            NewProducts = new ObservableCollection<Product>();
            CopyProducts = CreateNewCopyOfProducts();
        }

        public ExpenseInvoiceViewModel()
        {
            _products = new ObservableCollection<Product>();
            NewProducts = new ObservableCollection<Product>();
            CopyProducts = CreateNewCopyOfProducts();
        }

        private ObservableCollection<Product> CreateNewCopyOfProducts()
        {
            return new ObservableCollection<Product>(
            _products.Select(product => new Product
            {
                Name = product.Name,
                Price = product.Price,
                Dt = product.Dt,
                Count = product.Count,
                UM = product.UM,
            }));
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {

                Set(ref _selectedProduct, value);
                OnPropertyChanged(nameof(CurrentCount));
                _newCount = 0;
                OnPropertyChanged(nameof(NewCount));
            }
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

        public int NewCount
        {
            get => _newCount;
            set
            {
                if(value < 0)
                {
                    Set(ref _newCount,0);
                }
                 else if (value > CurrentCount)
                {
                    Set(ref _newCount, CurrentCount);
                }
                else
                {
                    Set(ref _newCount, value);
                }
                
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

        private bool CheckFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(CurrentName))
                errors.Add("Name cannot be empty.");
            if (CurrentCount <= 0)
                errors.Add("Count must be greater than 0.");

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors));
                return false;
            }

            return true;
        }

        public string FinalCost => NewProducts.Sum(x => x.Sum).ToString("f2");

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(obj =>
                    {
                        if (CheckFields())
                        {
                            if (NewProducts.Any(x => x.Name == _selectedProduct.Name && x.UM == _selectedProduct.UM))
                            {
                                var queue = NewProducts.First(x => x.Name == _selectedProduct.Name && x.UM == _selectedProduct.UM);
                                queue.Count += _selectedProduct.Count;
                                _selectedProduct.Count -= _newCount;

                                _newCount = 0;

                                //CurrentCount = 0;
                                //CurrentName = "";
                                //SelectedProduct.Price = 0;
                                //_selectedProduct = new Product();

                                OnPropertyChanged(nameof(FinalCost));
                            }
                            else
                            {
                                DeliveryProducts.Add(_newProduct);
                                _newProduct = new Product();
                                CurrentCount = 0;
                                CurrentName = "";
                                CurrentPrice = 0;

                                OnPropertyChanged(nameof(FinalCost));
                            }
                        }
                    }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(obj =>
                    {
                        if (_selectedProduct != null)
                        {
                            DeliveryProducts.Remove(_selectedProduct);
                            OnPropertyChanged(nameof(FinalCost));
                        }
                    }));
            }
        }

        private RelayCommand _confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get
            {
                return _confirmCommand ??
                    (_confirmCommand = new RelayCommand(obj =>
                    {
                        if (DeliveryProducts.Count > 0)
                        {
                            var mergedProducts = _products
                                                .Union(DeliveryProducts, new ProductComparer())
                                                .ToList();

                            _products.Clear();
                            foreach (var product in mergedProducts)
                            {
                                _products.Add(product);
                            }

                            MessageBox.Show("Products have been successfully added");
                            DeliveryProducts.Clear();

                            JsonUtils.ToJsonFile(_products, typeof(Product));
                            OnPropertyChanged(nameof(FinalCost));
                        }
                    }));
            }
        }
    }
}
