using SQLite;

namespace CarameloApp.Models
{
	/// <summary>
	/// Classe base para entidades da aplicação
	/// </summary>
	public class BaseEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}
