using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Schedules.Forms;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Schedules.Pages
{
	// página de edição de agendamentos
	public class EditSchedulePage : ScheduleForm
	{
		private readonly Schedule _schedule;

		public EditSchedulePage(Schedule schedule)
		{
			_schedule = schedule;

			if(_pets != null)
				_pets.ForEach(x => x.IsSelected = _schedule.Pets.Any(z => z.Id == x.Id));

			Title = _schedule.ToString();

			var center = GenerateCenterFormStackLayout(_schedule);

			var updateButton = new CustomButton("Atualizar agendamento");
			updateButton.Clicked += UpdateScheduleButton_Clicked;

			var deleteButton = new CustomButton("Cancelar agendamento");
			deleteButton.Clicked += DeleteButton_Clicked;

			var botton = new Bottom(new View[]
			{
				updateButton,
				deleteButton
			});

			Content = new Content(center, botton);
		}

		private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(null, $"Tem certeza que deseja cancelar o agendamento?", "Cancelar agendamento", "Cancelar");

			if (answer)
			{
				_scheduleRepository.Delete(_schedule);

				await DisplayAlert(null, $"Agendamento cancelado", "Ok");
				await Navigation.PopAsync();
			}
		}

		private async void UpdateScheduleButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				ValidateForm();

				Schedule schedule = new Schedule
				{
					Id = _schedule.Id,
					DateTime = _datePicker.Date.AddTicks(_timePicker.Time.Ticks),
					ServiceType = GetSelectedServiceType(),
					Pets = _pets.Where(X => X.IsSelected).ToList()
				};

				_scheduleRepository.Update(schedule);

				await DisplayAlert(null, $"Agendamento atualizado", "Ok");
				await Navigation.PopAsync();
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}
