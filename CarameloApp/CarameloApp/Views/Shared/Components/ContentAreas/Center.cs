using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Área para conteúdo principal
	/// </summary>
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