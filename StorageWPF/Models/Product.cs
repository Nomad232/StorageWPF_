using System.ComponentModel;

namespace StorageWPF.Models
{
    public class Product : Model
    {
        private string _name;
        private double _price;
        private int _count;
        private Units_Of_Measurement _um;
        private DateTime Date = DateTime.Now;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public double Price
        {
            get => _price;
            set
            {
                Set(ref _price, value);
                OnPropertyChanged("Sum");
            }
        }
        public int Count
        {
            get { return _count; }
            set
            {
                Set(ref _count, value);
                OnPropertyChanged("Sum");
            }
        }
        public Units_Of_Measurement UM
        {
            get => _um;
            set => Set(ref _um, value);
        }
        public DateTime Dt
        {
            get => Date;
            set => Set(ref Date, value);
        }
        public double Sum => _price * _count;
    }
}
