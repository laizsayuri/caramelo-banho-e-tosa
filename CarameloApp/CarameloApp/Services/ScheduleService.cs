using CarameloApp.Data;
using CarameloApp.Models;
using Xamarin.Forms;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para operações envolvendo Agendamentos
	/// </summary>
	public class ScheduleService
	{
		private static ScheduleRepository _scheduleRepository;
		private readonly SessionService _sessionService;

		public ScheduleService()
		{
			_scheduleRepository = DependencyService.Get<ScheduleRepository>();
			_sessionService = DependencyService.Get<SessionService>();
		}

		public void Insert(Schedule schedule)
		{
			_scheduleRepository.Insert(schedule);
			_sessionService.UpdateUser();
		}

		public void Delete(Schedule schedule)
		{
			_scheduleRepository.Delete(schedule);
			_sessionService.UpdateUser();
		}

		public void Update(Schedule schedule)
		{
			_scheduleRepository.Update(schedule);
			_sessionService.UpdateUser();
		}
	}
}
