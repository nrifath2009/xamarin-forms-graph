using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GraphDemo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GraphWithChartJsPage : ContentPage
	{
		public GraphWithChartJsPage ()
		{
			InitializeComponent ();
            this.BindingContext = new ChartReportPageViewModel("bar");
		}
	}
}