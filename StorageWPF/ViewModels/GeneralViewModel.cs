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
        public string[] Labels => Products?.OrderBy(x=> x.Count).Select(e => e.Name).ToArray();
        public double[] Values => Products?.OrderBy(x => x.Count).Select(e => (double)e.Count).ToArray();
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
            // Очистка предыдущего графика
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();

            // Создание серии данных
            var barSeries = new BarSeries
            {
                ItemsSource = Values.Select(value => new BarItem { Value = value }).ToList(),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}"
            };

            // Добавление серии на график
            PlotModel.Series.Add(barSeries);

            // Настройка оси Y (категорийная ось с метками)
            PlotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                ItemsSource = Labels
            });

            // Настройка оси X (числовая ось для значений)
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0.1, // Дополнительное пространство слева
                MaximumPadding = 0.1, // Дополнительное пространство справа
                Title = "Quantity"      // Подпись оси X
            });

            // Обновление графика
            PlotModel.InvalidatePlot(true);
        }

    }
}