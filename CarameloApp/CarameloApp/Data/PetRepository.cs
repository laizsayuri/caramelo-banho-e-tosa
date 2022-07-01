using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;

namespace CarameloApp.Data
{
	// repositório de pets
	public class PetRepository : BaseRepositoy
	{
		private readonly ScheduleRepository _scheduleRepository = DependencyService.Get<ScheduleRepository>();

		public PetRepository()
		{
			_db.CreateTable<Pet>();
		}

		public void Insert(Pet pet) => _db.Insert(pet);
	
		public void Delete(Pet pet)
		{
			_db.Delete(pet);

			// validação para remover agendamentos relacionados ao pet excluído
			var toDeleteSchedules = _scheduleRepository.GetNotConcluded().Where(c => !c.Pets.Any()).ToList();
			toDeleteSchedules.ForEach(schedule => _scheduleRepository.Delete(schedule));
		}

		public void Update(Pet pet) => _db.Update(pet);

		public List<Pet> GetAll() => _db.Table<Pet>().OrderBy(x => x.Name).ToList();
	}
}
