using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// Classe criada para facilitar a estrutura do conteúdo das páginas
	/// </summary>
	public class Content : StackLayout
	{
		public Content(CarameloTop top, Center center)
		{
			Children.Add(top);
			Children.Add(center);
		}

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
