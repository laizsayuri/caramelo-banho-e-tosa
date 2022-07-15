using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace CarameloApp.Models
{
	/// <summary>
	/// Classe Pet e métodos auxiliares
	/// </summary>
	[Table("Pet")]
	public class Pet : BaseEntity
	{
		public string Name { get; set; }
		public Species Species { get; set; }
		public Sex Sex { get; set; }
		public Size Size { get; set; }

		[ForeignKey(typeof(User))]
		public int UserId { get; set; }

		[ManyToMany(typeof(ScheduleAnimal))]
		public List<Schedule> Schedules { get; set; }

		[Ignore]
		public string SpeciesDescription
		{
			get
			{
				switch (Species)
				{
					case Species.Dog:
						return "Cachorro";
					case Species.Cat:
						return "Gato";
					default:
						return "";
				}
			}
		}

		[Ignore]
		public string SexDescription
		{
			get
			{
				switch (Sex)
				{
					case Sex.Male:
						return "Macho";
					case Sex.Female:
						return "Femea";
					default:
						return "";
				}
			}
		}

		[Ignore]
		public string SizeDescription
		{
			get
			{
				switch (Size)
				{
					case Size.Small:
						return "Pequeno";
					case Size.Medium:
						return "Médio";
					case Size.Big:
						return "Grande";
					default:
						return "";
				}
			}
		}

		[Ignore]
		public string PicFile => $"{Species}_{Sex}_{Size}.png".ToLower();

		[Ignore]
		public bool IsSelected { get; set; }

		public override string ToString() => $"{Name}";

		public bool IsADog() => Species.Dog == Species;
		public bool IsACat() => Species.Cat == Species;

		public bool IsAMale() => Sex.Male == Sex;
		public bool IsAFemale() => Sex.Female == Sex;

		public bool IsSmall() => Size.Small == Size;
		public bool IsMedium() => Size.Medium == Size;
		public bool IsBig() => Size.Big == Size;

		public string GetPronounLetter() => IsAMale() ? "o" : "a";
	}

	public enum Species
	{
		Dog = 1,
		Cat = 2
	}

	public enum Sex
	{
		Male = 1,
		Female = 2
	}

	public enum Size
	{
		Small = 1,
		Medium = 2,
		Big = 3
	}
}
