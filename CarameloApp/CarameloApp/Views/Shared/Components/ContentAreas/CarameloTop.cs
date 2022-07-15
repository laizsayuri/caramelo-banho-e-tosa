using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Área de cabeçalho
	/// </summary>
	public class CarameloTop : StackLayout
	{
		public CarameloTop(TopType topType, params View[] children)
		{
			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.Center;

			if (topType == TopType.Filter)
			{
				HorizontalOptions = LayoutOptions.End;
				Margin = new Thickness(0, 15, 10, 0);
			}
			else
			{
				HorizontalOptions = LayoutOptions.Start;
				Margin = new Thickness(15, 0);
			}

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