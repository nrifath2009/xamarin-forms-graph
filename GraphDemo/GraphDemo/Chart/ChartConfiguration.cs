using GraphDemo.Chart.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo.Chart
{
    public class ChartConfiguration
    {
        private string chartType = "bar";
        private object chartData = null;
        private ChartData _chartData;
        //public ChartConfiguration(string chartType, object chartData)
        //{
        //    this.chartType = chartType;
        //    this.chartData = chartData;
        //}
        public ChartConfiguration(string chartType, ChartData chartData)
        {
            this.chartType = chartType;
            this._chartData = chartData;
        }
        public string GetChartConfiguration()
        {
            var config = new
            {
                type = chartType,
                data = chartData,
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
        private string GenerateChartOptions()
        {
            var chartOptions = $@"{{
            responsive: true,
            maintainAspectRatio: false,
            scales: {{
                xAxes: [{{
                    id: 'x-axis-1',
                    ticks:
            {{
                fontSize: 15,
                        fontStyle: 'bold'

                    }},
                    stacked: {_chartData.IsStackedBarChart},
                    gridLines:
            {{
                zeroLineWidth: 1,
                        zeroLineColor: '#000',

                    }},
                    scaleLabel:
            {{
                display: true,
                        labelString: {_chartData.XLabel},
                        fontSize: 15,
                        fontStyle: 'bold'

                    }}

        }}],
                yAxes: [{{
                    id: 'y-axis-1',
                    display: true,
                    ticks: {{
                        fontSize: 15,
                        fontStyle: 'bold',
                        beginAtZero: true                            
                    }},
                    stacked: {_chartData.IsStackedBarChart},
                    gridLines: {{
                        zeroLineWidth: 1,
                        zeroLineColor: '#000',
                        drawTicks: true,
                        tickMarkLength: 15,

                    }},
                    scaleLabel: {{
                        display: true,
                        labelString: {_chartData.YLabel},
                        fontSize: 15,
                        fontStyle: 'bold'

                    }}

                }},
                {{
                    id: 'y-axis-2',
                    display: {_chartData.ShowRightYAxis},
                    position: 'right',
                    ticks: {{
                        fontSize: 15,
                        fontStyle: 'bold',
                        beginAtZero: true
                    }},
                    gridLines: {{
                        zeroLineWidth: 1,
                        zeroLineColor: '#000',
                        drawTicks: true,
                        tickMarkLength: 15,
                        color: '#000',
                        display: false

                    }},
                    scaleLabel: {{
                        display: true,
                        labelString: {_chartData.RightYLabel},
                        fontSize: 15,
                        fontStyle: 'bold'

                    }}

                }}]

            }},
            title: {{
                display: true,
                text: {_chartData.ChartName},
                fontSize: 20,
                fontFamily: 'Raleway',
            }},
            backgroundRules: {_chartData.BackgroundRules},
            legend: {{
                labels: {{
                    usePointStyle: true,
                    pointStyle: 'round',
                    fontSize: 15,
                    padding: 40,
                    fontFamily: 'Raleway',

                }}

            }},
            tooltips: {{
                backgroundColor: '#F7BB29',
                bodyFontColor: '#000',
                titleFontColor: '#000',
                caretPadding: 10,
                xPadding: 10,
                yPadding: 10,
                mode: 'nearest',
                position: 'nearest',
                titleFontSize: 15,
                bodyFontSize: 15,
                titleFontFamily: 'Raleway',
                bodyFontFamily: 'Raleway',
                titleSpacing: 8,
                bodySpacing: 8,
                displayColors: false,

                callbacks: {{

                    title: function(tooltipItem, data)
{{
    title = data.datasets[tooltipItem[0].datasetIndex].label + ' : ' + tooltipItem[0].yLabel + ' (' + tooltipItem[0].xLabel + ' )';
    return title;
}},
                    label: function(tooltipItem, data)
{{

    var dataArray = [];
    if (data.tooltipLabels == undefined || data.tooltipLabels == null)
        return dataArray;

    var tooltipLabels = data.tooltipLabels;
    var tooltipData = data.tooltipDataSet[tooltipItem.datasetIndex].DataList[tooltipItem.index];
    var size = tooltipLabels != undefined ? tooltipLabels.length : 0;

    for (var i = 0; i < size; i++)
    {{
        var item = '\u27A4 ' + tooltipLabels[i] + ' : ' + tooltipData[i] + ' ';
        dataArray.push(item);
    }}

    //return toolTipItem;
    return dataArray;

}},

                }}

            }},
            animation: {{
                duration: 1000,
                onComplete: function()
{{
    if ({_chartData.IsPatientComparativeData})
    {{
        var chartInstance = this.chart;
        var ctx = chartInstance.ctx;
        ctx.textAlign = 'left';
        ctx.font = '14px Open Sans';
        ctx.fillStyle = '#fff';

        Chart.helpers.each(this.data.datasets.forEach(function(dataset, i) {{
            var meta = chartInstance.controller.getDatasetMeta(i);
            Chart.helpers.each(meta.data.forEach(function(bar, index) {{

                if ({_chartData.IsPatientComparativeWithProfessionalData})
                {{

                    if (i === 0 && $.inArray(bar._model.label, {_chartData.PatientData.Labels}) > -1) {{

                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 10);
                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 25);
                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 40);
                    }}
                }}
                else
                {{

                    if (bar._model.datasetLabel === {_chartData.PatientData.LabelNames}[index] &&
                        bar._model.label === {_chartData.PatientData.Labels}[index])
                    {{

                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 10);
                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 25);
                        ctx.fillText('\u25CF', bar._model.x - 5, bar._model.y + 40);
                    }}
                }}

            }}),
                                this);
        }}),
                            this);
    }}
}}

            }},
            hover: {{
                animationDuration: 0

            }}

        }};

        if ({_chartData.IsPatientProgressGraph} != undefined && {_chartData.IsPatientProgressGraph})
        {{
            if ({_chartData.SurveyQuestionMaxScore} != undefined && {_chartData.SurveyQuestionMaxScore} > 0)
            {{
                options.scales.yAxes[0].ticks.max = {_chartData.SurveyQuestionMaxScore};
            }}            
            options.scales.xAxes[0].gridLines.display = false;
        }}";

            return chartOptions;
        }
        private string GenerateChartPlugins()
        {
            string plugins = $@"[{{
                beforeDraw: function (chart) {{
                    if ({_chartData.HasBackgroundRules} != undefined && {_chartData.HasBackgroundRules}) {{
                        var ctx = chart.chart.ctx;
                        var ruleIndex = 0;
                        var rules = chart.chart.options.backgroundRules;
                        var yaxis = chart.chart.scales['y-axis-1'];
                        var xaxis = chart.chart.scales['x-axis-1'];

            for (var i = 0; i < rules.length; i++)
            {{
                var yRangeBeginPixel = yaxis.getPixelForValue(rules[i].YaxisSegementStart);
                var yRangeEndPixel = yaxis.getPixelForValue(rules[i].YaxisSegementEnd);

                var width = chart.chart.width,
                height = chart.chart.height,
                ctx = chart.chart.ctx;

                ctx.restore();
                var fontSize = 16;
                ctx.font = 'bold ' + fontSize + 'px sans-serif';
                ctx.textBaseline = 'middle';

                ctx.beginPath();
                var xPos = xaxis.left;
                var yPos = Math.min(yRangeBeginPixel, yRangeEndPixel);
                var recWidth = xaxis.right - xaxis.left;
                var recHeight = Math.max(yRangeBeginPixel, yRangeEndPixel) - Math.min(yRangeBeginPixel, yRangeEndPixel);
                ctx.rect(xPos, yPos, recWidth, recHeight);
                ctx.fillStyle = rules[i].BackgroundColor;
                ctx.fill();
                ctx.fillStyle = '#585551';
                ctx.fillText(rules[i].LabelText, xPos + (recWidth / 2), yPos + (recHeight / 2));
                ctx.save();
            }}
        }}

    }}
}}]";
            return plugins;
        }
        public string GenerateChartConfiguration()
        {
            var config = new
            {
                type = chartType,
                data = new
                {
                    labels = _chartData.Labels,
                    labelsValue = _chartData.LabelsValue,
                    datasets = _chartData.Series,
                    tooltipLabels = _chartData.TooltipLabels,
                    tooltipDataSet = _chartData.TooltipSeries
                },
                options = GenerateChartOptions(),
                plugins = GenerateChartPlugins()
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }
    }
}
