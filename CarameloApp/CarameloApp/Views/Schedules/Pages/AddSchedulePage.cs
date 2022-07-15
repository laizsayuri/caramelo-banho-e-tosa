using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;

namespace CarameloApp.Views.Schedules.Pages
{
	/// <summary>
	/// Página para marcar Agendamentos
	/// </summary>
	public class AddSchedulePage : ScheduleFormPage
	{
		public AddSchedulePage()
		{
			Title = "Novo agendamento";

			_pets.ForEach(x => x.IsSelected = false);

			var center = GenerateCenterFormStackLayout();

			var saveButton = new CustomButton("Agendar serviço");
			saveButton.Clicked += AddScheduleButton_Clicked;

			var botton = new Bottom(new View[]
			{
				saveButton
			});

			Content = new Content(center, botton);
		}

		private async void AddScheduleButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				ValidateForm();
				var userId = _sessionService.GetUserId();

				Schedule schedule = new Schedule
				{
					DateTime = _datePicker.Date.AddTicks(_timePicker.Time.Ticks),
					ServiceType = GetSelectedServiceType(),
					Pets = _pets.Where(X => X.IsSelected).ToList(),
					UserId = userId
				};

				_scheduleService.Insert(schedule);

				await DisplayAlert(null, $"Serviço agendado", "Ok");
				await Navigation.PopAsync();
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}
