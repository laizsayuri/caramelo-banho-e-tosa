using SQLite;
using System;
using System.IO;

namespace CarameloApp.Data
{
	/// <summary>
	/// Repositório base para operações no banco
	/// </summary>
	public class BaseRepository
	{
		private readonly string _dbPath;
		protected readonly SQLiteConnection _db;

		public BaseRepository()
		{
			_dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "carameloDB.db3");
			_db = new SQLiteConnection(_dbPath);
		}
	}
}
