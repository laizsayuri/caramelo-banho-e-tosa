using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using CarameloApp.Views.Pets.Pages;
using CarameloApp.Views.Schedules.Pages;

namespace CarameloApp.Views.Shared.Pages
{
	// página de tabs utilizada no projeto
	public class CustomTabbedPage : Xamarin.Forms.TabbedPage
	{
		public CustomTabbedPage()
		{
			BarBackgroundColor = CarameloColors.TabsColor;
			SelectedTabColor = Color.White;
			UnselectedTabColor = CarameloColors.TextColor;			

			On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

			NavigationPage petNavigationPage = new CustomNavigationPage(new PetHomePage(), "pet_icon.png", "Pets");
			NavigationPage scheduleNavigationPage = new CustomNavigationPage(new ScheduleHomePage(), "schedule_icon.png", "Agendamentos");

			Children.Add(petNavigationPage);
			Children.Add(scheduleNavigationPage);
		}
	}
}