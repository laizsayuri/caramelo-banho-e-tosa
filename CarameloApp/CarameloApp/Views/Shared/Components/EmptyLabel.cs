using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	/// <summary>
	/// Label utilizado para mostrar mensagens de listas vazias
	/// </summary>
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
