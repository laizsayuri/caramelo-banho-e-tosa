using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout ("div") para agrupamento de radio buttons
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
