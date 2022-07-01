using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace CarameloApp.Models
{
	// classe Pet

	[Table("Pet")]
	public class Pet : BaseEntity
	{
		public string Name { get; set; }
		public Species Species { get; set; }
		public Sex Sex { get; set; }
		public Size Size { get; set; }

		[ManyToMany(typeof(ScheduleAnimal))]
		public List<Schedule> Schedules { get; set; }

		// pega descrição da raça
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

		// pega descrição do sexo
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

		// pega descrição do tamanho
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

		// pega arquivo utilizado na List View
		[Ignore]
		public string PicFile
		{
			get
			{
				switch (Species)
				{
					case Species.Dog:
						return "dog_pic.png";
					case Species.Cat:
						return "cat_pic.png";
					default:
						return "";
				}
			}
		}

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
