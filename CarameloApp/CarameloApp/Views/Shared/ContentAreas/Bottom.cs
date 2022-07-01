using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout ("div") para fim da página
	public class Bottom : CustomStackLayout
	{
		public Bottom(params View[] children) : base(children)
		{
			Orientation = StackOrientation.Vertical;
			VerticalOptions = LayoutOptions.End;
			Margin = new Thickness(25);
		}
	}
}