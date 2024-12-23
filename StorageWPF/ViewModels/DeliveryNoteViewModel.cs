﻿using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageWPF.ViewModels
{
    internal class DeliveryNoteViewModel : ViewModel
    {
        private Product _newProduct = new Product();
        private Product _selectedProduct;
        public ObservableCollection<Product> DeliveryProducts {  get; set; }
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

        public Product NewProduct
        {
            get => _newProduct;
            set => Set(ref _newProduct, value);
        }

        public string CurrentName
        {
            get => _newProduct.Name;
            set
            {
                _newProduct.Name = value;
                OnPropertyChanged(nameof(CurrentName));
            }
        }
        public double CurrentPrice
        {
            get => _newProduct.Price;
            set
            {
                _newProduct.Price = value;
                OnPropertyChanged(nameof(CurrentPrice));
            }
        }
        public int CurrentCount
        {
            get => _newProduct.Count;
            set
            {
                _newProduct.Count = value;
                OnPropertyChanged(nameof(CurrentCount));
            }
        }

        public Units_Of_Measurement CurrentUnit
        {
            get => _newProduct.UM;
            set
            {
                _newProduct.UM = value;
                OnPropertyChanged(nameof(CurrentUnit));
            }
        }
        public DateTime CurrentDate
        {
            get => _newProduct.Dt;
            set
            {
                _newProduct.Dt = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public string FinalCost => DeliveryProducts.Sum(x => x.Sum).ToString("f2");

        private bool CheckFields()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(CurrentName))
                errors.Add("Name cannot be empty.");
            if (CurrentPrice <= 0)
                errors.Add("Price must be greater than 0.");
            if (CurrentCount <= 0)
                errors.Add("Count must be greater than 0.");
            if (CurrentDate < new DateTime(2000, 1, 1) || CurrentDate > DateTime.Now)
                errors.Add("Date must be between January 1, 2000, and now.");

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors));
                return false;
            }

            return true;
        }

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
                            if(DeliveryProducts.Any(x=> x.Name == _newProduct.Name && x.UM == _newProduct.UM))
                            {
                                var queue = DeliveryProducts.First(x => x.Name == _newProduct.Name && x.UM == _newProduct.UM);
                                queue.Count += _newProduct.Count;
                                queue.Price += _newProduct.Count;

                                _newProduct = new Product();
                                CurrentCount = 0;
                                CurrentName = "";
                                CurrentPrice = 0;

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
                            //додавання продуктів
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
