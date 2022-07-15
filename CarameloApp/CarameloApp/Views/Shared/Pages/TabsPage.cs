using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using CarameloApp.Resources;
using CarameloApp.Views.Home.Pages;
using CarameloApp.Views.Pets.Pages;
using CarameloApp.Views.Schedules.Pages;
using CarameloApp.Views.Users.Pages;
using CarameloApp.Services;

namespace CarameloApp.Views.Shared.Pages
{
	public class TabsPage : Xamarin.Forms.TabbedPage
	{
		private readonly SessionService _sessionService;

		public TabsPage()
		{
			_sessionService = DependencyService.Get<SessionService>();

			NavigationPage.SetHasNavigationBar(this, false);

			BarBackgroundColor = CarameloColors.TabsColor;
			SelectedTabColor = Color.White;
			UnselectedTabColor = CarameloColors.TextColor;

			On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

			var exitButtonToolbar = CreateExitButtonToolbarItem();

			CustomNavigationPage homeNavigationPage = new CustomNavigationPage(new HomePage(this, exitButtonToolbar), "home_icon.png", "Home");
			CustomNavigationPage petNavigationPage = new CustomNavigationPage(new PetHomePage(this, exitButtonToolbar), "pet_icon.png", "Pets");
			CustomNavigationPage scheduleNavigationPage = new CustomNavigationPage(new ScheduleHomePage(this, exitButtonToolbar), "schedule_icon.png", "Agendamentos");
			CustomNavigationPage userNavigationPage = new CustomNavigationPage(new EditUserPage(this, exitButtonToolbar), "profile_icon.png", "Perfil");

			Children.Add(homeNavigationPage);
			Children.Add(petNavigationPage);
			Children.Add(scheduleNavigationPage);
			Children.Add(userNavigationPage);
		}

		public async void LogOff()
		{
			var answer = await DisplayAlert(null, $"Tem certeza que deseja sair?", "Sair", "Cancelar");

			if (answer)
			{
				_sessionService.CleanUser();
				await Navigation.PopToRootAsync();
			}
		}

		private ToolbarItem CreateExitButtonToolbarItem()
		{
			ToolbarItem exitButton = new ToolbarItem
			{
				IconImageSource = "exit_icon.png"
			};
			exitButton.Clicked += ExitButton_Clicked;

			return exitButton;
		}

		private void ExitButton_Clicked(object sender, System.EventArgs e) => LogOff();
	}
}