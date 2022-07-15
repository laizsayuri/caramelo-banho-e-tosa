using System;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;

namespace CarameloApp.Views.Pets.Pages
{
	/// <summary>
	/// Página para adicionar Pets
	/// </summary>
	public class AddPetPage : PetFormPage
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
				var userId = _sessionService.GetUserId();

				Pet pet = new Pet
				{
					Name = _nameEntry.Text,
					Species = GetSelectedSpecies(),
					Sex = GetSelectedSex(),
					Size = GetSelectedSize(),
					UserId = userId
				};

				_petService.Insert(pet);

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