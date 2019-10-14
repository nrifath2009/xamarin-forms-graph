using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphDemo
{
    public class ChartReportPageViewModel
    {
        public string ReportHtml { set; get; }

        public ChartReportPageViewModel(string chartType)
        {
            BuildReportHtml(chartType);
        }

        private void BuildReportHtml(string chartType)
        {
            var chartConfigScript = GetChartScript(chartType);
            var html = GetHtmlWithChartConfig(chartConfigScript);
            ReportHtml = html;
        }

        private string GetHtmlWithChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:100%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
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

        private string GetChartScript(string chartType)
        {
            var chartConfig = GetSpendingChartConfig(chartType);
            var script = $@"var config = {chartConfig};
                        window.onload = function() {{
                          var canvasContext = document.getElementById(""chart"").getContext(""2d"");
                          new Chart(canvasContext, config);
                        }};";
            return script;
        }

        private string GetSpendingChartConfig(string chartType)
        {
            var config = new
            {
                type = chartType,
                data = GetChartData(),
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top"
                    },
                    animation = new
                    {
                        animateScale = true
                    }
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetChartData()
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

        private List<Tuple<int, int, int>> GetDefaultColors()
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
