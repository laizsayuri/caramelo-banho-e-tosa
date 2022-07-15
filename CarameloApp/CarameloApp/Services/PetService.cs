using CarameloApp.Data;
using CarameloApp.Models;
using Xamarin.Forms;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para operações envolvendo Pets
	/// </summary>
	public class PetService
	{
		private readonly PetRepository _petRepository;
		private readonly ScheduleRepository _scheduleRepository;
		private readonly SessionService _sessionService;

		public PetService()
		{
			_petRepository = DependencyService.Get<PetRepository>();
			_scheduleRepository = DependencyService.Get<ScheduleRepository>();
			_sessionService = DependencyService.Get<SessionService>();
		}

		public void Insert(Pet pet)
		{
			_petRepository.Insert(pet);
			_sessionService.UpdateUser();
		}

		public void Delete(Pet pet)
		{
			_petRepository.Delete(pet);
			_scheduleRepository.RemoveEmptySchedules();
			_sessionService.UpdateUser();
		}

		public void Update(Pet pet)
		{
			_petRepository.Update(pet);
			_sessionService.UpdateUser();
		}
	}
}
