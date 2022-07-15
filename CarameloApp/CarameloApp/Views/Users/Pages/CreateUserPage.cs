using CarameloApp.Models;
using CarameloApp.Resources;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Shared.Pages;
using System;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Página final do processo de criação de usuário
	/// </summary>
	public class CreateUserPage : UserFormPage
	{
		private readonly string _email;
		private readonly string _pass;

		public CreateUserPage(string email, string pass)
		{
			_email = email;
			_pass = pass;

			NavigationPage.SetHasNavigationBar(this, false);

			var title = new CustomLabel()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold,
				Text = "Criar sua conta",
				FontSize = 30
			};

			var createButton = new CustomButton("Criar sua conta Caramelo")
			{
				Margin = new Thickness(0, 25, 0, 0),
				BackgroundColor = CarameloColors.InitialBackgroundColor
			};
			createButton.Clicked += CreateButton_Clicked;

			var center = GenerateCenterFormStackLayout();
			center.Children.Insert(0, title);
			center.Children.Add(createButton);			

			var returnLabel = CreateReturnLabel();
			returnLabel.Text = "< voltar";

			var botton = new Bottom(new View[]
			{
				returnLabel
			});

			Content = new Content(center, botton);
		}

		private async void CreateButton_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				ValidateForm();

				User user = new User
				{
					Email = _email,
					Password = User.EncodePassword(_pass),
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

				_userService.Insert(user);
				_sessionService.UpdateUser(user);

				await DisplayAlert(null, $"Perfil criado com sucesso", "Ok");
				await Navigation.PushAsync(new TabsPage());
			}
			catch (Exception exception)
			{
				await DisplayAlert(null, exception.Message, "Ok");
			}
		}
	}
}
