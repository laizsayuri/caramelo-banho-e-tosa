using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Área de rodapé
	/// </summary>
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