using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Views.Shared.Pages;

namespace CarameloApp
{
	// classe inicial do projeto
	public partial class App : Application
	{
		public App()
		{
			RegisterDependencies();
			InitializeComponent();			
			MainPage = new CustomTabbedPage();
		}

		private void RegisterDependencies()
		{
			DependencyService.RegisterSingleton(new PetRepository());
			DependencyService.RegisterSingleton(new ScheduleRepository());
		}

		protected override void OnStart()
		{			
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
