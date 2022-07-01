using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	// botão personalizado do projeto
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

			HorizontalOptions = LayoutOptions.CenterAndExpand;
			VerticalOptions = LayoutOptions.EndAndExpand;
		}
	}
}
