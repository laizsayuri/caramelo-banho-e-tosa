using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Services;
using CarameloApp.Views;
using CarameloApp.Views.Shared.Pages;

namespace CarameloApp
{
	/// <summary>
	/// Classe inicial onde é configurado as dependências e deifinição da página inicial
	/// </summary>
	public class App : Application
	{
		public App()
		{
			RegisterDependencies();					
			MainPage = new CustomNavigationPage(new InitialPage(), hasBar: false);			
		}

		private void RegisterDependencies()
		{
			DependencyService.RegisterSingleton(new PetRepository());
			DependencyService.RegisterSingleton(new ScheduleRepository());
			DependencyService.RegisterSingleton(new UserRepository());

			DependencyService.RegisterSingleton(new SessionService());
			DependencyService.RegisterSingleton(new ChartService());
			
			DependencyService.RegisterSingleton(new UserService());
			DependencyService.RegisterSingleton(new PetService());
			DependencyService.RegisterSingleton(new ScheduleService());

			var defaultService = new DefaultUserService();
			defaultService.CheckAndSetDefaultUser();
		}
	}
}
