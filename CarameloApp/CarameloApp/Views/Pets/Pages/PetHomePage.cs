using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Data;
using CarameloApp.Models;
using CarameloApp.Views.Pets.ListViews;
using CarameloApp.Views.Shared.ContentAreas;
using CarameloApp.Views.Shared.Components;

namespace CarameloApp.Views.Pets.Pages
{
	// página inicial da área de pets
	public class PetHomePage : ContentPage
	{
		private readonly PetRepository _petRepository = DependencyService.Get<PetRepository>();

		public PetHomePage()
		{
			Title = "Meus pets cadastrados";
		}

		protected override void OnAppearing() => SetContent();

		private void SetContent()
		{
			var center = new Center(new View[] {
				GetPetsList()
			});
			center.Margin = new Thickness(0);

			var addPetButton = new CustomButton("Adicionar pet");
			addPetButton.Clicked += AddPetButton_Clicked;

			var botton = new Bottom(new View[]
			{
				addPetButton
			});

			Content = new Content(center, botton);
		}

		private View GetPetsList()
		{
			var pets = _petRepository.GetAll();

			if (pets != null && pets.Any())
			{
				var listView = new PetListView(pets);

				listView.ItemSelected += PetListView_ItemSelected;
				return listView;
			}
			else
				return new EmptyLabel("Nenhum pet cadastrado");

		}

		private async void PetListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var pet = (Pet)e.SelectedItem;
			await Navigation.PushAsync(new EditPetPage(pet));
		}

		private async void AddPetButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddPetPage());
		}
	}
}