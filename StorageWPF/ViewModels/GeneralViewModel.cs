using OxyPlot;
using OxyPlot.Series;
using StorageWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StorageWPF.ViewModels
{
    internal class GeneralViewModel: ViewModel
    {
        public string[] Labels => Products?.Select(e => e.Name).ToArray();
        public double[] Values => Products?.Select(e => (double)e.Count).ToArray();
        public ObservableCollection<Product> Products { get; set; }
        public GeneralViewModel(ObservableCollection<Product> products)
        {
            Products = products;
            OnPropertyChanged("Lables");
            OnPropertyChanged("Values");

            PlotModel = new PlotModel { Title = "Динамический график" };
            UpdatePlot();
        }

        private PlotModel _plotModel;

        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }

        private void UpdatePlot()
        {
            // Обновляем график
            PlotModel.Series.Clear();

            var barSeries = new BarSeries
            {
                ItemsSource = Values,
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}"
            };

            PlotModel.Series.Add(barSeries);

            PlotModel.Axes.Clear();
            PlotModel.Axes.Add(new OxyPlot.Axes.CategoryAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                ItemsSource = Labels
            });

            // Сообщаем, что график обновился
            PlotModel.InvalidatePlot(true);
        }
    }
}
