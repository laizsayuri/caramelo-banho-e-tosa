using CarameloApp.Resources;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Shared.Pages;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Página de login
	/// </summary>
	public class LoginPage : UserFormPage
	{
		private readonly CustomButton _loginButton;

		public LoginPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			var title = new CustomLabel()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold,
				Text = "Login",
				FontSize = 30
			};

			_emailEntry = CreateEmailEntry();
			_passEntry = CreatePassEntry();

			var recoverAccountLabel = new CustomLabel()
			{
				Text = "Recuperar",
				Margin = new Thickness(0),
				HorizontalTextAlignment = TextAlignment.Center,
				TextDecorations = TextDecorations.Underline,
				FontAttributes = FontAttributes.Bold,
			};
			recoverAccountLabel.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = RecoverAccountTapCommand
			});

			_loginButton = new CustomButton("Login")
			{
				Margin = new Thickness(0, 250, 0, 0),
				BackgroundColor = CarameloColors.InitialBackgroundColor
			};
			_loginButton.Clicked += LoginButton_Clicked;

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
					HorizontalTextAlignment = TextAlignment.Center,
					Margin = new Thickness(0),
					Text = "Esqueceu a senha?"
				},
				recoverAccountLabel,
				_loginButton,
				CreateActivityIndicator()
			});
			center.Spacing = 5;

			var returnLabel = CreateReturnLabel();

			var botton = new Bottom(new View[]
			{
				returnLabel
			});

			Content = new Content(center, botton);
		}

		private async void LoginButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				LockEmailAndPassForm(_loginButton, true);

				ValidateEmailAndPassForm();

				var email = _emailEntry.Text;
				var pass = _passEntry.Text;

				var user = _userService.GetByEmailAndPass(email, pass);

				if (user == null)
					throw new Exception("E-mail ou senha invalido");

				_sessionService.UpdateUser(user);
				await Navigation.PushAsync(new TabsPage());
			}
			catch (Exception ex)
			{
				await DisplayAlert(null, ex.Message, "Ok");
			}
			finally
			{
				LockEmailAndPassForm(_loginButton, false);
			}
		}

		private ICommand RecoverAccountTapCommand => new Command(async () =>
		{
			await Navigation.PushAsync(new RecoverUserPassPage());
		});
	}
}
