using CarameloApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Components
{
	/// <summary>
	/// ListView personalizada
	/// </summary>	
	public class CustomListView<T> : ListView where T : BaseEntity
	{
		public CustomListView(List<T> itemsSource)
		{
			Margin = new Thickness(0, 15);
			RowHeight = 100;
			ItemsSource = itemsSource;
			VerticalScrollBarVisibility = ScrollBarVisibility.Always;
		}
	}
}
