using CarameloApp.Data;
using CarameloApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para operações envolvendo sessão do Usuário
	/// </summary>
	public class SessionService
	{
		private readonly UserRepository _userRepository;
		private readonly ScheduleRepository _scheduleRepository;	

		public SessionService()
		{
			_userRepository = DependencyService.Get<UserRepository>();
			_scheduleRepository = DependencyService.Get<ScheduleRepository>();			
		}

		public void UpdateUser(User user = null)
		{
			if(user == null)
				user = GetUser();
			
			user = _userRepository.GetById(user.Id);
			SetSchedules(user);

			DependencyService.RegisterSingleton(user);
		}

		public User SetSchedules(User user)
		{
			foreach (var schedule in user.Schedules)
			{
				var pets = _scheduleRepository.GetWithChildreen(schedule.Id).Pets;
				schedule.Pets = pets;
			}

			return user;
		}

		public User GetUser() => DependencyService.Get<User>();

		public int GetUserId() => DependencyService.Get<User>().Id;

		public List<Pet> GetPetList() => DependencyService.Get<User>().Pets.OrderBy(x => x.Name).ToList();

		public List<Schedule> GetScheduleList() => DependencyService.Get<User>().Schedules.OrderBy(x => x.DateTime).ToList();

		public void CleanUser() => DependencyService.RegisterSingleton((User)null);
	}
}
