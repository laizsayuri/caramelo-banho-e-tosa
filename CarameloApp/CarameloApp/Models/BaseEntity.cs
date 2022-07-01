using SQLite;

namespace CarameloApp.Models
{
	// classe base para as entidades do projeto
	public class BaseEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}
