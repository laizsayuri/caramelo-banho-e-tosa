using SQLite;
using System;
using System.IO;

namespace CarameloApp.Data
{
	// repositório base para as outras entidades do projeto (pet, agendamento e etc)
	public class BaseRepositoy
	{
		private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "myDB.db3");
		protected readonly SQLiteConnection _db;

		public BaseRepositoy()
		{
			_db = new SQLiteConnection(_dbPath);			
		}
	}
}
