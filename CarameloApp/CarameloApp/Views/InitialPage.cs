using CarameloApp.Resources;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Users.Pages;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarameloApp.Views
{
	/// <summary>
	/// Páginal inicial
	/// </summary>
	public class InitialPage : ContentPage
	{
		protected override void OnAppearing() => SetContent();

		private void SetContent()
		{
			BackgroundColor = CarameloColors.InitialBackgroundColor;

			var logoImage = new Image
			{
				Source = "logo.png",
				Margin = new Thickness(0, 0, 0, 50),
			};

			var loginButton = new CustomButton("LOGIN")
			{
				FontAttributes = FontAttributes.Bold,
				BackgroundColor = CarameloColors.InitialBackgroundColor
			};
			loginButton.Clicked += LoginButton_ClickedAsync;

			var registerLabel = new CustomLabel
			{
				HorizontalTextAlignment = TextAlignment.Center,
				TextDecorations = TextDecorations.Underline,
				FontAttributes = FontAttributes.Bold,
				Text = "Cadastre-se"
			};
			registerLabel.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = CreateUserTapCommand
			});

			var center = new StackLayout()
			{
				Margin = new Thickness(0, 50),
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.Center,
				Children =
				{
					logoImage,
					loginButton,
					new CustomLabel
					{
						HorizontalTextAlignment = TextAlignment.Center,
						Text = "Não possui conta?",
						FontAttributes = FontAttributes.Bold,
					},
					registerLabel
				}
			};

			Content = center;
		}

		private ICommand CreateUserTapCommand => new Command(async () =>
		{
			await Navigation.PushAsync(new CreateUserEmailAndPassPage());
		});

		private async void LoginButton_ClickedAsync(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}
	}
}
