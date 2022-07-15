using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Componente para agrupamento de radio buttons
	/// </summary>
	public class RadioGroup : CustomStackLayout
	{
		public RadioGroup(StackOrientation orientation, params View[] children) : base(children)
		{
			Orientation = orientation;
			HorizontalOptions = LayoutOptions.Start;
			FlowDirection = FlowDirection.LeftToRight;
		}
	}
}
