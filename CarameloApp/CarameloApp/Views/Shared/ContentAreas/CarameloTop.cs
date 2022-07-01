using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout ("div") para cabeçalho
	public class CarameloTop : StackLayout
	{
		public CarameloTop(TopType topType, params View[] children)
		{
			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.Center;
			Margin = new Thickness(0, 15, 10, 0);

			if (topType == TopType.Filter)
				HorizontalOptions = LayoutOptions.End;

			if (children != null && children.Any())
			{
				foreach (var child in children)
					Children.Add(child);
			}
		}
	}

	public enum TopType
	{
		Default,
		Filter
	}
}