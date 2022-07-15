using System;
using System.Linq;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Services;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Pets.Components;
using CarameloApp.Views.Shared.Pages;

namespace CarameloApp.Views.Pets.Pages
{
	/// <summary>
	/// Listagem inicial de Pets
	/// </summary>
	public class PetHomePage : TabsRootPage
	{
		protected readonly PetService _petService;
		protected readonly SessionService _sessionService;

		public PetHomePage(TabsPage root, ToolbarItem toolbarItem) : base(root, toolbarItem)
		{
			_petService = DependencyService.Get<PetService>();
			_sessionService = DependencyService.Get<SessionService>();

			Title = "Meus pets cadastrados";
		}

		protected override void OnAppearing() => SetContent();

		private void SetContent()
		{
			var center = new Center(new View[] {
				GetPetsList()
			})
			{
				Margin = new Thickness(0)
			};

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
			var pets = _sessionService.GetPetList();

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