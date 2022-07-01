using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Pages
{
	// navigation page customizado do projeto
	public class CustomNavigationPage : NavigationPage
	{
		public CustomNavigationPage(Page root, string iconImageSource, string title) : base(root)
		{
			IconImageSource = iconImageSource;
			Title = title;

			BarBackgroundColor = CarameloColors.BackgroundColor;
			BackgroundColor = CarameloColors.BackgroundColor;
			BarTextColor = CarameloColors.TextColor;
		}
	}
}
