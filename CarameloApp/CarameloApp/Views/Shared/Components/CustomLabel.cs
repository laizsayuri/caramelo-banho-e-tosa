using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	//label personalizado do projeto
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