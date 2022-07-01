using System;
using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Models;
using Size = CarameloApp.Models.Size;
using CarameloApp.Views.Shared.ContentAreas;
using CarameloApp.Views.Shared.Components;

namespace CarameloApp.Views.Pets.Forms
{
	// formulário de cadastro/edição de pet
	public class PetForm : ContentPage
	{
		protected CustomEntry _nameEntry;

		protected CustomRadioButton _catSpeciesRadioButton;
		protected CustomRadioButton _dogSpeciesRadioButton;

		protected CustomRadioButton _maleSexRadioButton;
		protected CustomRadioButton _femaleSexRadioButton;

		protected CustomRadioButton _smallSizeRadioButton;
		protected CustomRadioButton _mediumSizeRadioButton;
		protected CustomRadioButton _bigSizeRadioButton;

		protected readonly PetRepository _petRepository = DependencyService.Get<PetRepository>();

		public PetForm()
		{
			_petRepository = new PetRepository();
		}

		protected Species GetSelectedSpecies() =>
			_catSpeciesRadioButton.IsChecked ? (Species)_catSpeciesRadioButton.Value : (Species)_dogSpeciesRadioButton.Value;

		protected Sex GetSelectedSex() =>
			_maleSexRadioButton.IsChecked ? (Sex)_maleSexRadioButton.Value : (Sex)_femaleSexRadioButton.Value;

		protected Size GetSelectedSize()
		{
			if (_smallSizeRadioButton.IsChecked)
				return (Size)_smallSizeRadioButton.Value;

			if (_mediumSizeRadioButton.IsChecked)
				return (Size)_mediumSizeRadioButton.Value;

			return (Size)_bigSizeRadioButton.Value;
		}

		protected void ValidateForm()
		{
			if (string.IsNullOrEmpty(_nameEntry.Text))
				throw new Exception("Campo nome obrigatório");
		}

		protected Center GenerateCenterFormStackLayout(Pet pet = null)
		{
			var newPet = pet == null;

			_nameEntry = new CustomEntry
			{
				Text = pet?.Name,
				Keyboard = Keyboard.Text,
				Placeholder = "Nome do pet"
			};

			_catSpeciesRadioButton = new CustomRadioButton
			{
				Content = "Gato",
				GroupName = "species",
				Value = Species.Cat,
				IsChecked = newPet || pet.IsACat(),
			};

			_dogSpeciesRadioButton = new CustomRadioButton
			{
				Content = "Cachorro",
				GroupName = "species",
				Value = Species.Dog,
				IsChecked = !newPet && pet.IsADog(),
			};

			_maleSexRadioButton = new CustomRadioButton
			{
				Content = "Macho",
				GroupName = "sex",
				Value = Sex.Male,
				IsChecked = newPet || pet.IsAMale(),
			};

			_femaleSexRadioButton = new CustomRadioButton
			{
				Content = "Femea",
				GroupName = "sex",
				Value = Sex.Female,
				IsChecked = !newPet && pet.IsAFemale(),
			};

			_smallSizeRadioButton = new CustomRadioButton
			{
				Content = "Pequeno",
				GroupName = "size",
				Value = Size.Small,
				IsChecked = newPet || pet.IsSmall(),
			};

			_mediumSizeRadioButton = new CustomRadioButton
			{
				Content = "Médio",
				GroupName = "size",
				Value = Size.Medium,
				IsChecked = !newPet && pet.IsMedium(),
			};

			_bigSizeRadioButton = new CustomRadioButton
			{
				Content = "Grande",
				GroupName = "size",
				Value = Size.Big,
				IsChecked = !newPet && pet.IsBig(),
			};			

			return new Center(new View[]
			{
				_nameEntry,
				new RadioGroup(StackOrientation.Horizontal, new View[]
				{
					_catSpeciesRadioButton,
					_dogSpeciesRadioButton
				}),
				new RadioGroup(StackOrientation.Horizontal, new View[]
				{
					_maleSexRadioButton,
					_femaleSexRadioButton
				}),								
				new CustomLabel
				{
					Text = "Porte",
				},
				new RadioGroup(StackOrientation.Horizontal, new View[]
				{
					_smallSizeRadioButton,
					_mediumSizeRadioButton,
					_bigSizeRadioButton
				})
			});
		}
	}
}
