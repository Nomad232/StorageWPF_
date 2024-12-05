using OxyPlot;
using OxyPlot.Axes;
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
    internal class GeneralViewModel : ViewModel
    {
        public string[] CombinedLabels => Labels.Zip(Units, (label, unit) => $"{label} ({unit})").ToArray();


        public string[] Labels => Products?.OrderBy(x=> x.Count).Select(e => e.Name).ToArray();
        public double[] Values => Products?.OrderBy(x => x.Count).Select(e => (double)e.Count).ToArray();
        public Units_Of_Measurement[] Units => Products?.OrderBy(x => x.Count).Select(e => e.UM).ToArray();
        public ObservableCollection<Product> Products { get; set; }
        public GeneralViewModel(ObservableCollection<Product> products)
        {
            Products = products;
            OnPropertyChanged("Lables");
            OnPropertyChanged("Values");

            PlotModel = new PlotModel { Title = "Quantity graph" };

            StartAsyncUpdate();
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

        private async Task StartAsyncUpdate()
        {
            while (true)
            {
                UpdatePlot();
                await Task.Delay(5000);
            }
        }

        private void UpdatePlot()
        {
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();

            var barSeries = new BarSeries
            {
                FontSize = 15,
                ItemsSource = Values.Select(value => new BarItem { Value = value }).ToList(),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                TrackerFormatString = "{0}",
            };

            PlotModel.Series.Add(barSeries);

            PlotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                ItemsSource = CombinedLabels
            });

            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.1, 
                MaximumPadding = 0.1, 
                Title = "Quantity"      
            });

            PlotModel.InvalidatePlot(true);
        }

    }
}