using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Card para listagem em ListView personalizado
	/// </summary>
	public class Card : StackLayout
	{
		public Card()
		{
			Margin = new Thickness(15, 5);
			BackgroundColor = CarameloColors.SecondBackgroundColor;
			Orientation = StackOrientation.Horizontal;
		}

	}

	public class CardCenterContent : CustomStackLayout
	{
		public CardCenterContent(params View[] children) : base(children)
		{
			Orientation = StackOrientation.Vertical;
			VerticalOptions = LayoutOptions.Center;
		}
	}

	public class CardEndContent : CustomStackLayout
	{
		public CardEndContent(params View[] children) : base(children)
		{
			Orientation = StackOrientation.Vertical;
			HorizontalOptions = LayoutOptions.EndAndExpand;
			VerticalOptions = LayoutOptions.Center;
			Margin = new Thickness(15);
		}
	}
}
