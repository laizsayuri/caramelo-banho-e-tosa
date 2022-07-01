using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	// label para listagens vazias
	public class EmptyLabel : CustomLabel
	{
		public EmptyLabel(string text)
		{
			Margin = new Thickness(25);
			Text = text;			
			VerticalOptions = LayoutOptions.CenterAndExpand;
			HorizontalOptions = LayoutOptions.CenterAndExpand;
		}
	}
}
