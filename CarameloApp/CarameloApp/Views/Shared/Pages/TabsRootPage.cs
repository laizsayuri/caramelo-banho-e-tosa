using CarameloApp.Services;
using Xamarin.Forms;

namespace CarameloApp.Views.Shared.Pages
{
	public class TabsRootPage : ContentPage
	{
		private readonly TabsPage _root;
		private readonly bool _exitToRoot;

		public TabsRootPage(TabsPage root, ToolbarItem exitButtonToolbarItem = null)
		{
			_root = root;

			if (exitButtonToolbarItem != null)
			{
				_exitToRoot = true;
				ToolbarItems.Add(exitButtonToolbarItem);
			}
		}

		protected override bool OnBackButtonPressed()
		{

			if (_exitToRoot)
			{
				_root.LogOff();
				return true;
			}

			return false;
		}
	}
}
