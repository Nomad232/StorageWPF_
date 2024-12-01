using System.ComponentModel;

namespace StorageWPF.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private double _price;
        private int _count;
        private Units_Of_Measurement _um;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged("Price");
                NotifyPropertyChanged("Sum");
            }
        }
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyPropertyChanged("Count");
                NotifyPropertyChanged("Sum");
            }
        }
        public Units_Of_Measurement UM
        {
            get { return _um; }
            set
            {
                _um = value;
                NotifyPropertyChanged("UM");

            }
        }

        public double Sum => _price * _count;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
