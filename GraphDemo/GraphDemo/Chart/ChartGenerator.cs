using GraphDemo.Chart.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphDemo.Chart
{
    public class ChartGenerator
    {
        //public ChartGenerator(string chartType,object chartData)
        //{
        //    this.ChartConfiguration = new ChartConfiguration(chartType, chartData);
        //    this.ApplyConfiguration();
        //}
        public ChartGenerator(string chartType, ChartData chartData)
        {
            this.ChartConfiguration = new ChartConfiguration(chartType, chartData);
            this.ApplyConfiguration();
        }

        private ChartConfiguration ChartConfiguration { set; get; }
        private string chartConfigurationScript { set; get; }

        public string GenerateChart()
        {
            var inlineStyle = "style=\"width:100%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfigurationScript}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
                              <canvas id=""chart"" />
                                </div>";
            var document = $@"<html style=""width:97%;height:100%;"">
                              <head>{chartJsScript}</head>
                              <body {inlineStyle}>
                                {chartContent}
                                {chartConfigJsScript}
                              </body>
                              </html>";
            return document;
        }
        private void ApplyConfiguration()
        {
            //var chartConfig = this.ChartConfiguration.GetChartConfiguration();
            var chartConfig = this.ChartConfiguration.GenerateChartConfiguration();
            chartConfigurationScript = $@"var config = {chartConfig};
                        window.onload = function() {{
                          var canvasContext = document.getElementById(""chart"").getContext(""2d"");
                          new Chart(canvasContext, config);
                        }};";            
        }

        private static object GetChartData()
        {
            var colors = GetDefaultColors();
            var labels = new[] { "Groceries", "Car", "Flat", "Electronics", "Entertainment", "Insurance" };
            var randomGen = new Random();
            var dataPoints1 = Enumerable.Range(0, labels.Length)
                .Select(i => randomGen.Next(5, 25))
                .ToList();
            var dataPoints2 = Enumerable.Range(0, labels.Length)
                .Select(i => randomGen.Next(5, 25))
                .ToList();
            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                        label = "Spending",
                        data = dataPoints1,
                        backgroundColor = dataPoints1.Select((d, i) =>
                        {
                            var color = colors[i % colors.Count];
                            return $"rgb({color.Item1},{color.Item2},{color.Item3})";
                        })
                    },
                    new
                    {
                        label = "My Spending",
                        data = dataPoints2,
                        backgroundColor = dataPoints2.Select((d, i) =>
                        {
                            var color = colors[i % colors.Count];
                            return $"rgb({color.Item1},{color.Item2},{color.Item3})";
                        })
                    }
                },
                labels
            };
            return data;
        }

        private static List<Tuple<int, int, int>> GetDefaultColors()
        {
            return new List<Tuple<int, int, int>>
            {
                new Tuple<int, int, int>(255, 99, 132),
                new Tuple<int, int, int>(255, 159, 64),
                new Tuple<int, int, int>(255, 205, 86),
                new Tuple<int, int, int>(75, 192, 192),
                new Tuple<int, int, int>(54, 162, 235),
                new Tuple<int, int, int>(153, 102, 255),
                new Tuple<int, int, int>(201, 203, 207)
            };
        }
    }
}
