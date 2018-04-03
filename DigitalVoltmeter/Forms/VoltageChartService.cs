using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DigitalVoltmeter
{
    public class VoltageChartService
    {

        private Chart chart;
        private double quantStep;
        private String title;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="chart">График</param>
        /// <param name="title">Заголовок графика</param>
        /// <param name="quantStep">Шаг вертикальной сетки</param>
        public VoltageChartService(Chart chart, String title, double quantStep)
        {
            this.chart = chart;
            this.chart.ChartAreas[0].AxisX.Minimum = 0;
            this.chart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            this.title = title;
            this.quantStep = quantStep;
            this.chart.ChartAreas[0].AxisY.Interval = this.quantStep;
            this.chart.Titles[0] = new Title(title);
        }

        /// <summary>
        /// Добавляет ступенчатый график последовательных значений напряжений
        /// </summary>
        /// <param name="voltages">массив напряжений</param>
        /// <param name="color">цвет графика</param>
        /// <param name="width">ширина линии</param>
        public void AddInputVoltageList(string seriesName, double[] voltages, Color color, int width)
        {
            double n = Math.Log(voltages.Length, 2);
            double readQuantStep = (n > 5) ? (quantStep * (n - 4)) : quantStep;
            this.chart.ChartAreas[0].AxisY.Interval = readQuantStep;
            Series voltageSeries = new Series(seriesName);
            voltageSeries.ChartType = SeriesChartType.StepLine;
            voltageSeries.Color = color;
            voltageSeries.BorderWidth = width;
            voltageSeries.Points.AddXY(0, 0);
            for (int x = 0; x < voltages.Length; x++)
                voltageSeries.Points.AddXY(x, voltages[x]);

            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Uвх";
            Series series = chart.Series.FindByName(seriesName);
            if (series != null)
                chart.Series.Remove(series);
            chart.Series.Add(voltageSeries);
        }
    }
}
