using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Services;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Schedules.Components;
using CarameloApp.Views.Shared.Pages;

namespace CarameloApp.Views.Schedules.Pages
{
	/// <summary>
	/// Listagem inicial de Agendamentos
	/// </summary>
	public class ScheduleHomePage : TabsRootPage
	{
		private readonly ScheduleService _scheduleService;
		private readonly SessionService _sessionService;

		public ScheduleHomePage(TabsPage root, ToolbarItem toolbarItem) : base(root, toolbarItem)
		{
			_scheduleService = DependencyService.Get<ScheduleService>();
			_sessionService = DependencyService.Get<SessionService>();

			Title = "Meus agendamentos";
		}

		protected override void OnAppearing() => SetContent();

		private void SetContent(bool onlyConcluded = false)
		{
			var onlyConcludedCheckbox = new CheckBox
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.End,
				IsChecked = onlyConcluded,
			};
			onlyConcludedCheckbox.CheckedChanged += OnlyConcludedCheckbox_CheckedChanged;

			var onlyConcludedLabel = new CustomLabel
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.End,
				Text = "Apenas Concluídos",
				FontSize = 10
			};

			var top = new CarameloTop(TopType.Filter, new View[]
			{
				onlyConcludedLabel,
				onlyConcludedCheckbox
			});

			var center = new Center(new View[] {
				GetSchedulesList(onlyConcluded)
			})
			{
				Margin = new Thickness(0)
			};

			var addScheduleButton = new CustomButton("Agendar serviço");
			addScheduleButton.Clicked += AddScheduleButton_Clicked;

			var botton = new Bottom(new View[]
			{
				addScheduleButton
			})
			{
				IsVisible = !onlyConcluded
			};

			Content = new Content(top, center, botton);
		}

		private void OnlyConcludedCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			SetContent(e.Value);
		}

		private View GetSchedulesList(bool onlyConcluded = false)
		{
			var schedules = _sessionService.GetScheduleList();

			if (schedules == null || !schedules.Any())
				return new EmptyLabel(onlyConcluded ? "Não há registros de serviços concluídos" : "Nenhum serviço agendado");

			schedules = schedules.Where(x => x.Concluded == onlyConcluded).ToList();
			ScheduleListView listView;

			if (onlyConcluded)
				listView = new ScheduleListView(schedules);
			else
				listView = new ScheduleListView(schedules, true, (sender, e) => ScheduleListView_CheckboxChecked(sender, e));

			listView.ItemSelected += ScheduleListView_ItemSelected;
			return listView;
		}

		private async void ScheduleListView_CheckboxChecked(object sender, CheckedChangedEventArgs e)
		{
			var checkBox = (CheckBox)sender;
			var parent = (StackLayout)checkBox.Parent;

			var answer = await DisplayAlert(null, $"Deseja concluir o agendamento?", "Concluir", "Cancelar");

			if (answer)
			{
				var scheduleId = Convert.ToInt32(((Label)parent.Children[0]).Text);
				var schedule = _sessionService.GetScheduleList().FirstOrDefault(x => x.Id == scheduleId);
				schedule.Concluded = true;

				_scheduleService.Update(schedule);

				await DisplayAlert(null, $"Agendamento concluido", "Ok");
			}

			SetContent();
		}

		private async void ScheduleListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var schedule = (Schedule)e.SelectedItem;
			await Navigation.PushAsync(new EditSchedulePage(schedule));
		}

		private async void AddScheduleButton_Clicked(object sender, EventArgs e)
		{
			var pets = _sessionService.GetPetList();

			if (!pets.Any())
				await DisplayAlert(null, $"Primeiro registre pets para poder realizar agendamentos!", "Ok");
			else
				await Navigation.PushAsync(new AddSchedulePage());
		}
	}
}
