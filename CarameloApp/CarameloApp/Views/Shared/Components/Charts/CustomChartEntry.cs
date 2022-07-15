using CarameloApp.Resources;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.Charts
{
	/// <summary>
	/// ChartEntry personalizado
	/// </summary>
	public class CustomChartEntry : ChartEntry
	{
		public CustomChartEntry(float value, Color color, string label, bool blackText = false) : base(value)
		{
			Color = color.ToSKColor();
			ValueLabelColor = color.ToSKColor();			
			TextColor = blackText ? SKColor.Parse("#000000") : color.ToSKColor();			
			Label = label;
			ValueLabel = value.ToString();
		}
	}
}
