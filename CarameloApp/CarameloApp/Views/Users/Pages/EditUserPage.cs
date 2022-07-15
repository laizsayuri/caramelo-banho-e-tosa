using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Shared.Pages;
using System;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Página para edição de usuário
	/// </summary>
	public class EditUserPage : UserFormPage
	{
		public EditUserPage(TabsPage root, ToolbarItem toolbarItem) : base(root, toolbarItem)
		{
			Title = "Meu perfil";
		}

		protected override void OnAppearing() => SetContent();

		private void SetContent()
		{
			var user = _sessionService.GetUser();
			var center = GenerateCenterFormStackLayout(user);

			var updateButton = new CustomButton("Atualizar cadastro");
			updateButton.Clicked += UpdatePetButton_Clicked;

			var botton = new Bottom(new View[]
			{
				updateButton
			});

			Content = new Content(center, botton);
		}

		private async void UpdatePetButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				ValidateForm();
				var userId = _sessionService.GetUserId();

				User user = new User
				{
					Id = userId,
					Name = _nameEntry.Text,
					Telephone = _telephoneEntry.Text,
					BirthDate = _birthDatePicker.Date,
					CEP = _cepEntry.Text,
					Number = int.Parse(_numberEntry.Text),
					StreetAddress = _streetAddressEntry.Text,
					District = _districtEntry.Text,
					City = (string)_cityPicker.SelectedItem,
					UF = (string)_ufPicker.SelectedItem
				};

				_userService.Update(user);

				await DisplayAlert(null, $"Perfil atualizado", "Ok");
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}
