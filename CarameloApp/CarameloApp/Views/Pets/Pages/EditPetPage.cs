using System;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Pets.Forms;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Pets.Pages
{
	// página de edição de pet
	public class EditPetPage : PetForm
	{
		private readonly Pet _pet;

		public EditPetPage(Pet pet)
		{
			_pet = pet;
			Title = _pet.Name;

			var center = GenerateCenterFormStackLayout(_pet);

			var updateButton = new CustomButton("Atualizar pet");
			updateButton.Clicked += UpdatePetButton_Clicked;

			var deleteButton = new CustomButton("Excluir pet");
			deleteButton.Clicked += DeleteButton_Clicked;

			var botton = new Bottom(new View[]
			{
				updateButton,
				deleteButton
			});

			Content = new Content(center, botton);
		}

		private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
			var answer = await DisplayAlert(null, $"Tem certeza que deseja excluir {_pet.Name} da sua lista?", "Excluir pet", "Cancelar");

			if (answer)
			{
				_petRepository.Delete(_pet);

				await DisplayAlert(null, $"{_pet.Name} excluíd{_pet.GetPronounLetter()}", "Ok");
				await Navigation.PopAsync();
			}
		}

		private async void UpdatePetButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				ValidateForm();

				Pet pet = new Pet
				{
					Id = _pet.Id,
					Name = _nameEntry.Text,
					Species = GetSelectedSpecies(),
					Sex = GetSelectedSex(),
					Size = GetSelectedSize()
				};

				_petRepository.Update(pet);

				await DisplayAlert(null, $"{pet.Name} atualizado", "Ok");
				await Navigation.PopAsync();
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}