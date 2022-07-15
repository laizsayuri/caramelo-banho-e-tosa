using CarameloApp.Resources;
using Microcharts;
using System.Collections.Generic;

namespace CarameloApp.Views.Shared.Components.Charts
{
	/// <summary>
	/// DonutChart personalizado
	/// </summary>
	public class CustomDonutChart : DonutChart
	{
		public CustomDonutChart(List<CustomChartEntry> entries)
		{
			Entries = entries;
			LabelMode = LabelMode.RightOnly;
			BackgroundColor = CarameloColors.SecondBackgroundColor.ToSKColor();
			LabelTextSize = 40;
		}
	}
}
