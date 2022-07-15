using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Pages
{
	public class CustomNavigationPage : NavigationPage
	{
		public CustomNavigationPage(Page root, string iconImageSource = null, string title = null, bool hasBar = true) : base(root)
		{
			SetHasNavigationBar(root, hasBar);

			BarBackgroundColor = CarameloColors.BackgroundColor;
			BackgroundColor = CarameloColors.BackgroundColor;
			BarTextColor = CarameloColors.TextColor;

			if (title != null)
				Title = title;

			if (iconImageSource != null)
				IconImageSource = iconImageSource;
		}
	}
}
