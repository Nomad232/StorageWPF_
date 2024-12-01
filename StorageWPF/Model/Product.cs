using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string name;
        private double price;
        private int count;
        private Units_Of_Measurement um;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                NotifyPropertyChanged("Name");
            }
        }
        public double Price
        {
            get { return price; }
            set 
            { 
                price = value; 
                NotifyPropertyChanged("Price");
                NotifyPropertyChanged("Sum");
            }
        }
        public int Count
        {
            get { return count; }
            set 
            { 
                count = value; 
                NotifyPropertyChanged("Count");
                NotifyPropertyChanged("Sum");
            }
        }
        public Units_Of_Measurement UM
        {
            get { return um; }
            set
            {
                um = value;
                NotifyPropertyChanged("UM");

            }
        }

        public double Sum
            { get { return price * count; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
