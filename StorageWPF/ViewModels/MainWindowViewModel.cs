using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using StorageWPF.Views;

namespace StorageWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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
                case "General":
                    //CurrentPage =
                        break;
                case "Inventory":
                    CurrentPage = new InventoryListPage();
                    break;
                case "Delivery":
                    CurrentPage = new DeliveryNotePage();
                    break;
                case "Expense":
                    //CurrentPage = new 
                    break;
            }
        }
    }
}
