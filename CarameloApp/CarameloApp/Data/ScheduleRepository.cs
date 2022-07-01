using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using CarameloApp.Models;

namespace CarameloApp.Data
{
	// repositório de agendamentos
	public class ScheduleRepository : BaseRepositoy
	{
		public ScheduleRepository()
		{
			_db.CreateTable<Schedule>();
			_db.CreateTable<ScheduleAnimal>();
		}

		public void Insert(Schedule schedule)
		{
			_db.InsertWithChildren(schedule);
		}

		public void Delete(Schedule schedule) => _db.Delete(schedule);

		public void Update(Schedule schedule) => _db.UpdateWithChildren(schedule);

		public List<Schedule> GetNotConcluded() => _db.GetAllWithChildren<Schedule>().Where(x => !x.Concluded).OrderBy(x => x.DateTime).ToList();
		public List<Schedule> GetConcluded() => _db.GetAllWithChildren<Schedule>().Where(x => x.Concluded).OrderBy(x => x.DateTime).ToList();
		public Schedule GetById(int id) => _db.GetAllWithChildren<Schedule>().Where(x => x.Id == id).FirstOrDefault();

		internal void Conclude(int id)
		{
			var schedule = GetById(id);
			schedule.Concluded = true;
			Update(schedule);
		}
	}
}
