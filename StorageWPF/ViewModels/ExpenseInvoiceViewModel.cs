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
        public ExpenseInvoiceViewModel()
        {
            _products = new ObservableCollection<Product>();
            NewProducts = new ObservableCollection<Product>();
            CopyProducts = CreateNewCopyOfProducts();
        }

        public ExpenseInvoiceViewModel(ObservableCollection<Product> products)
        {
            _products = products;
            NewProducts = new ObservableCollection<Product>();
            CopyProducts = CreateNewCopyOfProducts();
        }

        
        public ObservableCollection<Product> CreateNewCopyOfProducts()
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

        public Product SelectedProductForRemove
        {
            get => _selectedProductForRemove;
            set
            {

                Set(ref _selectedProductForRemove, value);
                OnPropertyChanged(nameof(NewCount));
            }
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

        //Обрана кількість
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

        //Кількість одиниць товару
        public int CurrentCount     
        {
            get
            {
                if (_selectedProduct == null) return 0;
                return _selectedProduct.Count;
            }
            set
            {
                _selectedProduct.Count = value;
                OnPropertyChanged(nameof(CurrentCount));
            }
        }

        private bool CheckFields()
        {
            var errors = new List<string>();
            if (_selectedProduct == null)
                errors.Add("Name cannot be empty.");
            else if(CurrentCount == 0)
                errors.Add($"The {SelectedProduct.Name} are out of stock.");
            else if (_newCount <= 0)
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
                                queue.Count += _newCount;
                                CurrentCount -= _newCount;
                                NewCount = 0;

                                OnPropertyChanged(nameof(FinalCost));
                            }
                            else
                            {

                                NewProducts.Add(new Product()
                                {
                                    Count = _newCount,
                                    Dt = _selectedProduct.Dt,
                                    Name = _selectedProduct.Name,
                                    Price = _selectedProduct.Price,
                                    UM = _selectedProduct.UM,
                                });
                                CurrentCount -= _newCount;
                                NewCount = 0;

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
                        if (_selectedProductForRemove != null)
                        {
                            var queue = CopyProducts.First(x => x.Name == _selectedProductForRemove.Name && x.UM == _selectedProductForRemove.UM);
                            queue.Count += _selectedProductForRemove.Count;
                            NewProducts.Remove(_selectedProductForRemove);

                            OnPropertyChanged(nameof(CurrentCount));
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
                        if (NewProducts.Count > 0)
                        {                          
                            var filteredProducts = _products
                                .Where(p => !NewProducts.Any(np => np.Count == p.Count))
                                .ToList();

                            
                            var mergedProducts = filteredProducts
                                .Union(NewProducts, new ProductComparerRemove())
                                .ToList();

                            _products.Clear();
                            foreach (var product in mergedProducts)
                            {
                                _products.Add(product);
                            }


                            MessageBox.Show("The products have been sent successfully");
                            NewProducts.Clear();
                            
                            

                            JsonUtils.ToJsonFile(_products, typeof(Product));
                            OnPropertyChanged(nameof(CopyProducts));
                            OnPropertyChanged(nameof(FinalCost));
                        }
                    }));
            }
        }
    }
}
