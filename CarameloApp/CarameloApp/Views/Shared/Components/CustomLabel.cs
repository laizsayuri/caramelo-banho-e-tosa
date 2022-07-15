using CarameloApp.Resources;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	/// <summary>
	/// Label personalizado
	/// </summary>
	public class CustomLabel : Label
	{
		public CustomLabel()
		{
			TextColor = CarameloColors.TextColor;
		}

		public CustomLabel(int fontSize) : this()
		{
			FontSize = fontSize;			
		}
	}
}