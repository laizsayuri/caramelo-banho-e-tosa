using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Models;
using CarameloApp.Views.Pets.ListViews;
using CarameloApp.Views.Shared.ContentAreas;
using CarameloApp.Views.Shared.Components;

namespace CarameloApp.Views.Schedules.Forms
{
	// formulário de edição/cadastro de agendamentos
	public class ScheduleForm : ContentPage
	{
		protected CustomDatePicker _datePicker;
		protected CustomTimePicker _timePicker;

		protected CustomRadioButton _bathServiceTypeRadioButton;
		protected CustomRadioButton _groomServiceTypeRadioButton;

		protected ScheduleRepository _scheduleRepository;
		protected PetRepository _petRepository;

		protected List<Pet> _pets;

		public ScheduleForm()
		{
			_scheduleRepository = DependencyService.Get<ScheduleRepository>();
			_petRepository = DependencyService.Get<PetRepository>();
			_pets = _petRepository.GetAll();
		}

		protected ServiceType GetSelectedServiceType() =>
			_bathServiceTypeRadioButton.IsChecked ? (ServiceType)_bathServiceTypeRadioButton.Value : (ServiceType)_groomServiceTypeRadioButton.Value;

		protected void ValidateForm()
		{
			if (!_pets.Any(x => x.IsSelected))
				throw new Exception("Selecione pelo menos um pet para realizar o serviço");

			if (_datePicker.Date.DayOfWeek == DayOfWeek.Sunday || _timePicker.Time.TotalHours < 8 || _timePicker.Time.TotalHours > 18)
				throw new Exception("Atendemos somente das 08:00 às 18:00 horas de segunda à sábado");

			if ((_datePicker.Date == DateTime.Today) && ((_datePicker.Date.Add(_timePicker.Time) - DateTime.Now).Hours < 1))
				throw new Exception("Agende com pelo menos 1 hora de antecedência");
		}

		protected DateTime GetRecommendedDateTime()
		{
			var date = DateTime.Today.AddDays(1);

			if (date.DayOfWeek == DayOfWeek.Sunday)
				date = date.AddDays(1);

			return date;
		}

		protected Center GenerateCenterFormStackLayout(Schedule schedule = null)
		{
			var newSchedule = schedule == null;

			_timePicker = new CustomTimePicker
			{
				Time = newSchedule ? new TimeSpan(8, 0, 0) :
				new TimeSpan(schedule.DateTime.Hour, schedule.DateTime.Minute, schedule.DateTime.Second),
				Format = "HH:mm"
			};

			_datePicker = new CustomDatePicker
			{
				Date = newSchedule ? GetRecommendedDateTime() : schedule.DateTime.Date,
				MinimumDate = DateTime.Now,
			};

			_bathServiceTypeRadioButton = new CustomRadioButton
			{
				Content = "Banho",
				GroupName = "service",
				Value = ServiceType.Bath,
				IsChecked = newSchedule || schedule.IsABath(),
			};

			_groomServiceTypeRadioButton = new CustomRadioButton
			{
				Content = "Tosa",
				GroupName = "service",
				Value = ServiceType.Groom,
				IsChecked = !newSchedule && schedule.IsAGroom(),
			};

			return new Center(new View[]
			{
				new CustomLabel
				{
					Text = "Selecione o dia",
				},
				_datePicker,
				_timePicker,
				new CustomLabel
				{
					Text = "Selecione os serviços",
				},
				new RadioGroup(StackOrientation.Vertical, new View[]
				{
					_bathServiceTypeRadioButton,
					_groomServiceTypeRadioButton
				}),
				new PetListView(_pets, true)
			});
		}
	}
}
