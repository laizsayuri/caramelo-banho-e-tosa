using System;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Pets.Forms;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Pets.Pages
{
	// página de cadastro de pet
	public class AddPetPage : PetForm
	{
		public AddPetPage()
		{
			Title = "Novo pet";

			var center = GenerateCenterFormStackLayout();

			var saveButton = new CustomButton("Cadastrar pet");
			saveButton.Clicked += AddPetButton_Clicked;

			var botton = new Bottom(new View[]
			{
				saveButton
			});

			Content = new Content(center, botton);
		}

		private async void AddPetButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				ValidateForm();

				Pet pet = new Pet
				{
					Name = _nameEntry.Text,
					Species = GetSelectedSpecies(),
					Sex = GetSelectedSex(),
					Size = GetSelectedSize()
				};

				_petRepository.Insert(pet);

				await DisplayAlert(null, $"{pet.Name} salv{pet.GetPronounLetter()}", "Ok");
				await Navigation.PopAsync();
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}