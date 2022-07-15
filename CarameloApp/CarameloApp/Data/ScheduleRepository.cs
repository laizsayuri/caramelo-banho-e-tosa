using SQLiteNetExtensions.Extensions;
using System.Linq;
using CarameloApp.Models;

namespace CarameloApp.Data
{
	/// <summary>
	/// Repositório para operações incluindo Agendamentos
	/// </summary>
	public class ScheduleRepository : BaseRepository
	{
		public ScheduleRepository()
		{
			_db.CreateTable<Schedule>();
			_db.CreateTable<ScheduleAnimal>();
		}

		public void Insert(Schedule schedule) => _db.InsertWithChildren(schedule);

		public void Delete(Schedule schedule) => _db.Delete(schedule);

		public void Update(Schedule schedule) => _db.UpdateWithChildren(schedule);

		public Schedule GetWithChildreen(int id) => _db.GetWithChildren<Schedule>(id);

		public void RemoveEmptySchedules()
		{
			var emptySchedules = _db.GetAllWithChildren<Schedule>().Where(c => !c.Concluded && (c.Pets == null || !c.Pets.Any()));

			foreach (var schedule in emptySchedules)
				_db.Delete(schedule);
		}
	}
}
