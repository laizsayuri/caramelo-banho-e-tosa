using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.Forms
{
	/// <summary>
	/// Picker personalizado
	/// </summary>
	public class CustomPicker : Picker
	{
		public CustomPicker()
		{
			TitleColor = CarameloColors.TextColor;
			TextColor = CarameloColors.TextColor;
		}
	}
}
