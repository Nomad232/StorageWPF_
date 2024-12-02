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
        public bool IsGuest { get; }
        public string Name { get;}
        public ObservableCollection<Product> Products { get; set; }

        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(bool isGuest, string name)
        {
            this.IsGuest = isGuest;
            Name = name;
            //Products з файлу
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
            switch (page)
            {
                case "General info":
                    //CurrentPage =
                        break;
                case "Inventory list":
                    CurrentPage = new InventoryListPage();
                    break;
                case "Delivery note":
                    CurrentPage = new DeliveryNotePage();
                    break;
                case "Expense invoice":
                    CurrentPage = new ExpenseInvoicePage();
                    break;
            }
        }
    }
}
