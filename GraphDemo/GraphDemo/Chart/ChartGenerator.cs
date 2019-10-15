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
        public ChartGenerator(string chartType, ChartData chartData)
        {
            this._chartConfiguration = new ChartConfiguration(chartType, chartData);
            this.ApplyConfiguration();
        }

        private ChartConfiguration _chartConfiguration;
        private string _chartConfigurationScript;

        public string GenerateChart()
        {
            var inlineStyle = "style=\"width:100%;height:100%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{_chartConfigurationScript}</script>";
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
            var chartConfig = this._chartConfiguration.GenerateChartConfiguration();
            _chartConfigurationScript = $@"var config = {chartConfig};
                        window.onload = function() {{
                          var canvasContext = document.getElementById(""chart"").getContext(""2d"");
                          new Chart(canvasContext, config);
                        }};";            
        }        
    }
}
