using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using StorageWPF.Views;
using StorageWPF.Models;
using System.Collections.ObjectModel;

namespace StorageWPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private bool _isGuest;
        public string Name { get; }
        public ObservableCollection<Product> Products { get; set; }

        private Dictionary<string, Page> _pageCache = new Dictionary<string, Page>();

        public MainWindowViewModel()
        {
            Products = JsonUtils.FromJsonFile<ObservableCollection<Product>>(typeof(Product));
            if(Products == null) Products = new ObservableCollection<Product>();
        }

        public MainWindowViewModel(bool isGuest, string name)
        {
            _isGuest = isGuest;
            Name = name;

            Products = JsonUtils.FromJsonFile<ObservableCollection<Product>>(typeof(Product));
            if (Products == null) Products = new ObservableCollection<Product>();
            PageSelect("General info");
        }

        public Visibility IsGuest
        {
            get
            {
                if (_isGuest)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        //знач за замовч General
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }


        private RelayCommand pageCommand;
        public RelayCommand PageCommand
        {
            get
            {
                return pageCommand ??
                    (pageCommand = new RelayCommand(obj =>
                    {
                        PageSelect(obj.ToString());
                    }));
            }
        }

        private void PageSelect(string page)
        {
            if (!_pageCache.ContainsKey(page))
            {
                switch (page)
                {
                    case "General info":
                        _pageCache[page] = new GeneralInfoPage()
                        {
                            DataContext = new GeneralViewModel(Products)
                        };
                        break;
                    case "Inventory list":
                        _pageCache[page] = new InventoryListPage()
                        {
                            DataContext = new InventoryViewModel(Products)
                        };
                        break;
                    case "Delivery note":
                        _pageCache[page] = new DeliveryNotePage()
                        {
                            DataContext = new DeliveryNoteViewModel(Products)
                        };
                        break;
                    case "Expense invoice":
                        _pageCache[page] = new ExpenseInvoicePage()
                        {
                            DataContext = new ExpenseInvoiceViewModel(Products)
                        };
                        break;
                }
            }

            CurrentPage = _pageCache[page];
        }
    }
}
