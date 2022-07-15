using CarameloApp.Data;
using CarameloApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Size = CarameloApp.Models.Size;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para criação de usuário inicial da aplicação
	/// </summary>
	public class DefaultUserService
	{
		private readonly UserRepository _userRepository;
		private readonly ScheduleRepository _scheduleRepository;
		private readonly PetRepository _petRepository;

		public DefaultUserService()
		{
			_userRepository = DependencyService.Get<UserRepository>();
			_scheduleRepository = DependencyService.Get<ScheduleRepository>();
			_petRepository = DependencyService.Get<PetRepository>();
		}

		public void CheckAndSetDefaultUser()
		{
			//_userRepository.DeleteAll();
			var defaultUser = _userRepository.GetLast();

			if (defaultUser == null)
				CreateDefaultUser();
		}

		private User CreateDefaultUser()
		{
			var user = new User
			{
				Name = "Fulano da Silva",
				Email = "fulano@gmail.com",
				Password = User.EncodePassword("senhas"),
				Telephone = "18996752701",
				BirthDate = DateTime.Now,
				CEP = "19806080",
				Number = 456,
				StreetAddress = "Rua Pindamanhangaba",
				District = "Centro",
				City = "São Paulo",
				UF = "SP"
			};

			_userRepository.Insert(user);

			user.Pets = CreateDefaultPets(user.Id);
			user.Schedules = CreateDefaultSchedules(user.Id, user.Pets);

			return user;
		}

		private List<Pet> CreateDefaultPets(int userId)
		{
			var pets = new List<Pet> {
				new Pet
				{
					UserId = userId,
					Name = "Fifita",
					Species = Species.Dog,
					Sex = Sex.Female,
					Size = Size.Medium
				},
				new Pet
				{
					UserId = userId,
					Name = "Márcio Garcia",
					Species = Species.Cat,
					Sex = Sex.Male,
					Size = Size.Medium
				},
				new Pet
				{
					UserId = userId,
					Name = "Frederico",
					Species = Species.Dog,
					Sex = Sex.Male,
					Size = Size.Small
				}
			};

			foreach (var pet in pets)
				_petRepository.Insert(pet);

			return pets;
		}

		private List<Schedule> CreateDefaultSchedules(int userId, List<Pet> pets)
		{
			var schedules = new List<Schedule>
			{
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 3, 25).AddHours(8),
					ServiceType = ServiceType.Groom,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[1]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 4, 11).AddHours(10),
					ServiceType = ServiceType.Bath,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[0],
						pets[2]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 4, 21).AddHours(14),
					ServiceType = ServiceType.Bath,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[1],
						pets[2]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 5, 3).AddHours(9),
					ServiceType = ServiceType.Groom,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[0],
						pets[1]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 5, 28).AddHours(17),
					ServiceType = ServiceType.Groom,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[2]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 6, 18).AddHours(16),
					ServiceType = ServiceType.Groom,
					Concluded = true,
					Pets = new List<Pet>
					{
						pets[0],
						pets[2]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 8, 13).AddHours(12),
					ServiceType = ServiceType.Bath,
					Concluded = false,
					Pets = new List<Pet>
					{
						pets[0],
						pets[1],
						pets[2]
					}
				},
				new Schedule
				{
					UserId = userId,
					DateTime = new DateTime(2022, 8, 29).AddHours(14),
					ServiceType = ServiceType.Groom,
					Concluded = false,
					Pets = new List<Pet>
					{
						pets[0]
					}
				}
			};

			foreach (var schedule in schedules)
				_scheduleRepository.Insert(schedule);

			return schedules;
		}
	}
}
