using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout ("div") para centro da página
	public class Center : CustomStackLayout
	{
		public Center(params View[] children) : base(children)
		{
			Margin = new Thickness(25);
			Spacing = 15;
			VerticalOptions = LayoutOptions.StartAndExpand;
		}
	}
}