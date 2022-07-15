using CarameloApp.Resources;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using System;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Página inicial do processo de criar novo usuário (email e senha)
	/// </summary>
	public class CreateUserEmailAndPassPage : UserFormPage
	{
		private readonly CustomButton _createUserButton;

		public CreateUserEmailAndPassPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			var title = new CustomLabel()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold,
				Text = "Criar conta",
				FontSize = 30
			};

			_emailEntry = CreateEmailEntry();
			_passEntry = CreatePassEntry();			
			_passConfirmEntry = CreatePassConfirmEntry();

			_createUserButton = new CustomButton("Continuar")
			{
				Margin = new Thickness(0, 25, 0, 0),
				BackgroundColor = CarameloColors.InitialBackgroundColor
			};
			_createUserButton.Clicked += CreateUserButton_Clicked;

			var center = new Center(new View[] {
				title,
				new CustomLabel
				{
					Text = "E-mail"
				},
				_emailEntry,
				new CustomLabel
				{
					Text = "Senha"
				},
				_passEntry,
				new CustomLabel
				{					
					Margin = new Thickness(0, 0, 0, 25),
					Text = "As senhas devem ter pelo menos 6 caracteres",
					FontSize = 12.5
				},
				new CustomLabel
				{
					Text = "Insira a senha nova mais uma vez"
				},
				_passConfirmEntry,
				_createUserButton,
				CreateActivityIndicator()
			});
			center.Spacing = 5;

			var returnLabel = CreateReturnLabel();

			var botton = new Bottom(new View[]
			{
				new CustomLabel
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,					
					Text = "Ao criar uma conta, você concorda com as Condições de Uso de acordo com a LGPD. " +
					"Por favor verifique a Notificação de Privacidade, Notificação de Cookies e a Notificação de " +
					"Anúncios Baseados em Interesse.",
					FontSize = 15,
					Margin = new Thickness(0, 15)
				},
				returnLabel
			});

			Content = new Content(center, botton);
		}

		private async void CreateUserButton_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				LockEmailAndPassForm(_createUserButton, true);

				ValidateNewEmailAndPassForm();

				var email = _emailEntry.Text;
				var pass = _passEntry.Text;

				var checkUser = _userService.GetByEmail(email);

				if (checkUser != null)
					throw new Exception("E-mail já cadastrado. Insira outro");

				await Navigation.PushAsync(new CreateUserPage(email, pass));
			}
			catch (Exception ex)
			{
				await DisplayAlert(null, ex.Message, "Ok");
			}
			finally
			{
				LockEmailAndPassForm(_createUserButton, false);
			}
		}
	}
}
