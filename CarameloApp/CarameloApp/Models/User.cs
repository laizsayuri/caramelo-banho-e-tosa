using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CarameloApp.Models
{
	/// <summary>
	/// Classe Usuário e métodos auxiliares
	/// </summary>
	public class User : BaseEntity
	{			
		public string Name { get; set; }

		public string Email { get; set; }
		
		public string Telephone { get; set; }

		public DateTime BirthDate { get; set; }

		public string CEP { get; set; }

		public int Number { get; set; }

		public string StreetAddress { get; set; }

		public string District { get; set; }

		public string City { get; set; }

		public string UF { get; set; }

		[OneToMany]
		public List<Pet> Pets { get; set; }

		[OneToMany]
		public List<Schedule> Schedules { get; set; }

		public string Password { get; set; }

		[Ignore]
		public string FirstName { get { return Name.Trim().Split(' ')[0]; } }

		[Ignore]
		public static List<string> UFs
		{
			get
			{
				return new List<string>
				{
					"SC",
					"SP"
				};
			}
		}

		[Ignore]
		public static List<string> SCCities
		{
			get
			{
				return new List<string>
				{
					"Chapecó",
					"Florianópolis",
					"Itajaí"
				};
			}
		}

		[Ignore]
		public static List<string> SPCities
		{
			get
			{
				return new List<string>
				{
					"Assis",
					"São Paulo"
				};
			}
		}

		public void SyncCredentials(User user)
		{
			Email = user.Email;
			Password = user.Password;
		}

		public static string EncodePassword(string password)
		{
			byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
			byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

			return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
		}
	}
}
