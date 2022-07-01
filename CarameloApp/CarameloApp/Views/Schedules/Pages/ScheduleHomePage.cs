using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Models;
using CarameloApp.Views.Schedules.ListViews;
using CarameloApp.Views.Shared.ContentAreas;
using CarameloApp.Views.Shared.Components;

namespace CarameloApp.Views.Schedules.Pages
{
	// página inicial da área de agendamentos
	public class ScheduleHomePage : ContentPage
	{
		private readonly ScheduleRepository _scheduleRepository = DependencyService.Get<ScheduleRepository>();
		private readonly PetRepository _petRepository = DependencyService.Get<PetRepository>();

		public ScheduleHomePage()
		{
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
			});
			center.Margin = new Thickness(0);

			var addScheduleButton = new CustomButton("Agendar serviço");			
			addScheduleButton.Clicked += AddScheduleButton_Clicked;

			var botton = new Bottom(new View[]
			{
				addScheduleButton
			});
			botton.IsVisible = !onlyConcluded;

			Content = new Content(top, center, botton);
		}

		private void OnlyConcludedCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			SetContent(e.Value);
		}

		private View GetSchedulesList(bool onlyConcluded = false)
		{
			var schedules = new List<Schedule>();

			if (onlyConcluded)
				schedules = _scheduleRepository.GetConcluded();
			else
				schedules = _scheduleRepository.GetNotConcluded();

			if (schedules != null && schedules.Any())
			{
				ScheduleListView listView;
				
				if (onlyConcluded)
					listView = new ScheduleListView(schedules);
				else
					listView = new ScheduleListView(schedules, true, (sender, e) => ScheduleListView_CheckboxChecked(sender, e));

				listView.ItemSelected += ScheduleListView_ItemSelected;
				return listView;
			}
			else
				return new EmptyLabel("Nenhum serviço agendado");

		}

		private async void ScheduleListView_CheckboxChecked(object sender, CheckedChangedEventArgs e)
		{
			var checkBox = (CheckBox)sender;
			var parent = (StackLayout)checkBox.Parent;
			var scheduleId = Convert.ToInt32(((Label)parent.Children[0]).Text);

			var answer = await DisplayAlert(null, $"Deseja concluir o agendamento?", "Concluir", "Cancelar");

			if (answer)
			{
				_scheduleRepository.Conclude(scheduleId);
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
			if (!_petRepository.GetAll().Any())
				await DisplayAlert(null, $"Primeiro registre pets para poder realizar agendamentos!", "Ok");
			else
				await Navigation.PushAsync(new AddSchedulePage());
		}
	}
}
