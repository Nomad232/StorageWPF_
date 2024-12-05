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
        public string[] CombinedLabels => Labels.Zip(Units, (label, unit) => $"{label} ({unit})").ToArray();  //комбінує значення з двох масивів
        public double[] ValuesOfSum => Products?.OrderBy(x => x.Count).Select(e => e.Sum).ToArray();
        public string[] Labels => Products?.OrderBy(x=> x.Count).Select(e => e.Name).ToArray();
        public double[] ValuesOfCount => Products?.OrderBy(x => x.Count).Select(e => (double)e.Count).ToArray();
        public Units_Of_Measurement[] Units => Products?.OrderBy(x => x.Count).Select(e => e.UM).ToArray();
        public ObservableCollection<Product> Products { get; set; }
        public GeneralViewModel(ObservableCollection<Product> products)
        {
            Products = products;

            PlotModelSum = new PlotModel { Title = "Total Cost Graph" };
            PlotModelCount = new PlotModel { Title = "Quantity Graph" };

            StartAsyncUpdate();
        }

        private PlotModel _plotModelCount;
        private PlotModel _plotModelSum;

        public PlotModel PlotModelCount
        {
            get => _plotModelCount;
            set
            {
                _plotModelCount = value;
                OnPropertyChanged(nameof(PlotModelCount));
            }
        }

        public PlotModel PlotModelSum
        {
            get => _plotModelSum;
            set
            {
                _plotModelSum = value;
                OnPropertyChanged(nameof(PlotModelSum));
            }
        }

        private async Task StartAsyncUpdate()
        {
            while (true)
            {
                UpdatePlotSum();
                UpdatePlotCount();
                await Task.Delay(5000);
            }
        }

        private void UpdatePlotCount()
        {
            PlotModelCount.Series.Clear();
            PlotModelCount.Axes.Clear();

            var barSeries = new BarSeries //стовпчата діаграма
            {
                FontSize = 15,
                ItemsSource = ValuesOfCount.Select(value => new BarItem { Value = value }).ToList(), //значення стовпців
                LabelPlacement = LabelPlacement.Inside,                                              //підпис в середині стовпця
                LabelFormatString = "{0}",
            };

            PlotModelCount.Series.Add(barSeries);

            PlotModelCount.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                ItemsSource = CombinedLabels
            });

            PlotModelCount.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.1,
                MaximumPadding = 0.1,
                Title = "Quantity"
            });

            PlotModelCount.InvalidatePlot(true);   //динамічне оновлення інформації
        }

        private void UpdatePlotSum()
        {
            PlotModelSum.Series.Clear();
            PlotModelSum.Axes.Clear();

            var barSeries = new BarSeries
            {
                FontSize = 15,
                ItemsSource = ValuesOfSum.Select(value => new BarItem { Value = value }).ToList(), 
                LabelPlacement = LabelPlacement.Inside,                                            
                LabelFormatString = "{0:0.00}",
            };

            PlotModelSum.Series.Add(barSeries);

            PlotModelSum.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                ItemsSource = CombinedLabels
            });

            PlotModelSum.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.1, 
                MaximumPadding = 0.1, 
                Title = "Quantity"      
            });

            PlotModelSum.InvalidatePlot(true);
        }

    }
}