using CarameloApp.Models;

namespace CarameloApp.Data
{
	/// <summary>
	/// Repositório para operações incluindo Pets
	/// </summary>
	public class PetRepository : BaseRepository
	{
		public PetRepository()
		{
			_db.CreateTable<Pet>();
		}

		public void Insert(Pet pet) => _db.Insert(pet);

		public void Delete(Pet pet) => _db.Delete(pet);

		public void Update(Pet pet) => _db.Update(pet);
	}
}
