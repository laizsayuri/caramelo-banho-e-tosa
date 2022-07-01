using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace CarameloApp.Models
{
	// classe agendamento
	[Table("Schedule")]
	public class Schedule : BaseEntity
	{
		public DateTime DateTime { get; set; }
		public ServiceType ServiceType { get; set; }

		[ManyToMany(typeof(ScheduleAnimal))]
		public List<Pet> Pets { get; set; }

		public bool Concluded { get; set; }

		[Ignore]
		public string DateTimeListAnotation => DateTime.ToString("dd/MM/yy - HH:mm");

		// pega descrição do tipo de serviço
		[Ignore]
		public string ServiceTypeDescription
		{
			get
			{
				switch (ServiceType)
				{
					case ServiceType.Bath:
						return "Banho";
					case ServiceType.Groom:
						return "Tosa";
					default:
						return "";
				}
			}
		}

		[Ignore]
		public string PicFile
		{
			get
			{
				switch (ServiceType)
				{
					case ServiceType.Bath:
						return "bath_pic.png";
					case ServiceType.Groom:
						return "groom_pic.png";
					default:
						return "";
				}
			}
		}

		public override string ToString() => $"{DateTime:dd/MM/yyyy HH:mm} - {ServiceTypeDescription}";

		public bool IsABath() => ServiceType.Bath == ServiceType;
		public bool IsAGroom() => ServiceType.Groom == ServiceType;
	}

	// classe utilizada para gerar relacionamento muitos para muitos entre agendamento e pet
	public class ScheduleAnimal
	{
		[ForeignKey(typeof(Schedule))]
		public int ScheduleId { get; set; }

		[ForeignKey(typeof(Pet))]
		public int PetId { get; set; }
	}

	public enum ServiceType
	{
		Bath = 1,
		Groom = 2
	}
}
