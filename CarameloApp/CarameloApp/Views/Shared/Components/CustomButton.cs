using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	/// <summary>
	/// Button personalizado
	/// </summary>
	public class CustomButton : Button
	{
		public CustomButton(string text)
		{
			Text = text;

			BackgroundColor = CarameloColors.BackgroundColor;
			TextColor = CarameloColors.TextColor;

			BorderColor = CarameloColors.TextColor;
			BorderWidth = 1;
			CornerRadius = 17;
			
			Margin = new Thickness(50, 0);

			HorizontalOptions = LayoutOptions.FillAndExpand;
			VerticalOptions = LayoutOptions.EndAndExpand;
		}
	}
}
