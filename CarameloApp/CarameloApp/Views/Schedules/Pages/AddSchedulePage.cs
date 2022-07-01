using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Schedules.Forms;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Schedules.Pages
{
	// página de cadastro de agendamentos
	public class AddSchedulePage : ScheduleForm
	{
		public AddSchedulePage()
		{
			Title = "Novo agendamento";

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

				Schedule schedule = new Schedule
				{
					DateTime = _datePicker.Date.AddTicks(_timePicker.Time.Ticks),
					ServiceType = GetSelectedServiceType(),
					Pets = _pets.Where(X => X.IsSelected).ToList()
				};

				_scheduleRepository.Insert(schedule);

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
