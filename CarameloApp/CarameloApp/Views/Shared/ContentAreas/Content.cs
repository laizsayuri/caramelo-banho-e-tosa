using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout ("div") para Content da página
	public class Content : StackLayout
	{
		public Content(Center center, Bottom botton)
		{
			Children.Add(center);
			Children.Add(botton);
		}

		public Content(CarameloTop top, Center center, Bottom botton)
		{
			Children.Add(top);
			Children.Add(center);
			Children.Add(botton);
		}
	}
}
