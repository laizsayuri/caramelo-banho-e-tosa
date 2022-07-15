using CarameloApp.Models;
using SQLiteNetExtensions.Extensions;
using System.Linq;

namespace CarameloApp.Data
{
	/// <summary>
	/// Repositório para operações incluindo Usuários
	/// </summary>
	public class UserRepository : BaseRepository
	{
		public UserRepository()
		{
			_db.CreateTable<User>();
		}

		public void Insert(User user) => _db.InsertWithChildren(user);

		public User GetById(int id) => _db.GetWithChildren<User>(id);

		public void DeleteAll() => _db.DeleteAll<User>();

		public User GetLast()
		{
			if(_db.GetAllWithChildren<User>().Any())
				return _db.GetAllWithChildren<User>().Last();

			return null;
		}

		public void Update(User user) => _db.Update(user);

		public User GetByEmailAndPass(string email, string pass)
		{
			return _db.GetAllWithChildren<User>().FirstOrDefault(x => x.Email == email && x.Password == pass);
		}

		public User GetByEmail(string email)
		{
			return _db.GetAllWithChildren<User>().FirstOrDefault(x => x.Email == email);
		}
	}
}
