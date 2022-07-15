using CarameloApp.Resources;
using CarameloApp.Services;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.Charts;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Shared.Pages;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Views.Home.Pages
{
	/// <summary>
	/// Página inicial com gráficos
	/// </summary>
	public class HomePage : TabsRootPage
	{
		private readonly ChartService _chartService;
		private readonly SessionService _sessionService;

		public HomePage(TabsPage root, ToolbarItem toolbarItem) : base(root, toolbarItem)
		{
			_chartService = DependencyService.Get<ChartService>();
			_sessionService = DependencyService.Get<SessionService>();

			Title = "Olá!";
		}

		protected override void OnAppearing() => SetContent();

		private void SetContent()
		{
			var user = _sessionService.GetUser();

			Title = $"Olá, {user.FirstName}!";

			var top = new CarameloTop(TopType.Default,
				new Image { Source = "user_pic.png", WidthRequest = 75 },
				new StackLayout
				{
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.Center,
					Margin = new Thickness(5, 0),
					Children =
					{
						new CustomLabel(20) { Text = user.Name }
					}
				}
			);

			var center = new Center(new View[] {
				GenerateTotalSchedulesChart(),
				GenerateTotalPetsChart()
			});

			Content = new Content(top, center);
		}

		private View GenerateTotalSchedulesChart()
		{
			var chartView = new CustomChartView();
			var results = _chartService.GetTotalFinishedSchedules();

			var resultStack = new CustomStackLayout(new CustomLabel { Text = "Serviços realizados", FontSize = 15 });

			if (!results.Any())
				resultStack.Children.Add(new EmptyLabel("Não há registros de serviços concluídos"));
			else
			{
				var entries = new List<CustomChartEntry>
				{
					new CustomChartEntry(results[0].Count, CarameloColors.TextColor, results[0].Name),
					new CustomChartEntry(results[1].Count, CarameloColors.TabsColor, results[1].Name),
				};

				chartView.Chart = new CustomDonutChart(entries);
				resultStack.Children.Add(chartView);
			}

			return resultStack;
		}

		private View GenerateTotalPetsChart()
		{
			var chartView = new CustomChartView();
			var results = _chartService.GetTotalPetsFromFinishedSchedules();

			var resultStack = new CustomStackLayout(new CustomLabel { Text = "Pets mais recorrentes", FontSize = 15 });

			if (!results.Any())
				resultStack.Children.Add(new EmptyLabel("Não há registros de serviços concluídos"));
			else
			{
				var entries = new List<CustomChartEntry>();
				foreach (var result in results)
					entries.Add(new CustomChartEntry(result.Count, CarameloColors.GetRandom(), result.Name, blackText: true));

				chartView.Chart = new CustomDonutChart(entries);
				resultStack.Children.Add(chartView);
			}

			return resultStack;
		}
	}
}
