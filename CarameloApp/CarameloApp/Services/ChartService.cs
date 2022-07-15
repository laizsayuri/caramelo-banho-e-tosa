using CarameloApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para operações envolvendo os gráficos da página inicial
	/// </summary>
	public class ChartService
	{		
		private readonly SessionService _sessionService;

		public ChartService()
		{
			_sessionService = DependencyService.Get<SessionService>();
		}

		public List<TotalQueryResult> GetTotalPetsFromFinishedSchedules()
		{
			var queryResult = new List<TotalQueryResult>();
			var totalPetsList = new List<Pet>();

			var schedules = _sessionService.GetScheduleList();
			var finishedSchedules = schedules.Where(x => x.Concluded).OrderBy(x => x.DateTime).ToList();

			if (!finishedSchedules.Any())
				return queryResult;

			foreach (var petList in finishedSchedules.Select(x => x.Pets))
				totalPetsList.AddRange(petList);

			var groupedPets = totalPetsList.GroupBy(x => x.Id).ToList();

			foreach (var group in groupedPets)
				queryResult.Add(new TotalQueryResult(group.First().Name, group.Count()));

			return queryResult;
		}

		public List<TotalQueryResult> GetTotalFinishedSchedules()
		{
			var queryResult = new List<TotalQueryResult>();
			var schedules = _sessionService.GetScheduleList();

			if(schedules == null)
				return queryResult;

			var finishedSchedules = schedules.Where(x => x.Concluded).OrderBy(x => x.DateTime).ToList();

			if (!finishedSchedules.Any())
				return queryResult;

			var totalBath = finishedSchedules.Where(x => x.ServiceType == ServiceType.Bath).Count();
			var totalGroom = finishedSchedules.Where(x => x.ServiceType == ServiceType.Groom).Count();

			queryResult.Add(new TotalQueryResult("Banho", totalBath));
			queryResult.Add(new TotalQueryResult("Tosa", totalGroom));

			return queryResult;
		}
	}

	public class TotalQueryResult
	{
		public TotalQueryResult(string name, int count)
		{
			Name = name;
			Count = count;
		}

		public string Name { get; set; }

		public int Count { get; set; }
	}
}
