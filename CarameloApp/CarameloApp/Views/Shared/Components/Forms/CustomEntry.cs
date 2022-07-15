using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.Forms
{
	/// <summary>
	/// Entry personalizado
	/// </summary>
	public class CustomEntry : Entry
	{
		public CustomEntry()
		{
			PlaceholderColor = CarameloColors.TextColor;
			TextColor = CarameloColors.TextColor;
		}
	}
}
