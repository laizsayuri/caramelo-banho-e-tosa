using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components.ContentAreas
{
	/// <summary>
	/// StackLayout personalizado
	/// </summary>
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
