using GraphDemo.Chart;
using GraphDemo.Chart.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphDemo
{
    public class ChartReportPageViewModel
    {
        public string ReportHtml { set; get; }
        private HttpClient _client;
        
        public ChartReportPageViewModel(string chartType,string url)
        {
            _client = new HttpClient();
            PopulateChart(chartType,url);
        }
        private async void PopulateChart(string chartType,string url)
        {
            var chartData = await GetChartDataFromUrl(url);
            ChartGenerator chartGenerator = new ChartGenerator(chartType, chartData);
            ReportHtml = chartGenerator.GenerateChart();
        }

        private async Task<ChartData> GetChartDataFromUrl(string url)
        {
            try
            {
                ChartData chartData = null;
                var uri = new Uri(string.Format(url, string.Empty));
  
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    chartData = JsonConvert.DeserializeObject<ChartData> (content);
                    if (chartData.PatientData == null)
                    {
                        chartData.PatientData = new PatientData();
                    }
                }
                return chartData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
