using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.ContentAreas
{
	// stack layout personalizado que já recebe os itens filho no construtor
	public class CustomStackLayout : StackLayout
	{
		public CustomStackLayout(params View[] children)
		{			
			if (children != null && children.Any())
			{
				foreach (var child in children)
					Children.Add(child);
			}
		}
	}
}
