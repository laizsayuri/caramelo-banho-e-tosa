using CarameloApp.Resources;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using System;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Página para recuperação de senha
	/// </summary>
	public class RecoverUserPassPage : UserFormPage
	{
		private readonly CustomButton _searchButton;

		public RecoverUserPassPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			var title = new CustomLabel()
			{
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold,
				Text = "Recuperar senha",
				FontSize = 30
			};

			_emailEntry = CreateEmailEntry();

			_searchButton = new CustomButton("Enviar")
			{
				Margin = new Thickness(0, 25, 0, 0),
				BackgroundColor = CarameloColors.InitialBackgroundColor
			};
			_searchButton.Clicked += SearchEmailButton_Clicked;
			
			var center = new Center(new View[] {
				title,
				new CustomLabel
				{
					Text = "E-mail"
				},
				_emailEntry,
				new CustomLabel
				{
					HorizontalOptions = LayoutOptions.Center,
					Text = "Insira seu email e você receberá as instruções para nova senha e acesso"
				},
				_searchButton,
				CreateActivityIndicator()
			});
			center.Spacing = 5;

			var returnLabel = CreateReturnLabel();

			var botton = new Bottom(new View[]
			{
				new CustomLabel
				{
					HorizontalOptions = LayoutOptions.Center,
					Text = "* Não serão enviados e-mails",
					FontSize = 15,
					Margin = new Thickness(0, 15)
				},
				returnLabel
			});

			Content = new Content(center, botton);
		}

		private async void SearchEmailButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				LockEmailAndPassForm(_searchButton, true);

				ValidateEmailAndPassForm();

				var email = _emailEntry.Text;

				var user = _userService.GetByEmail(email);

				if (user == null)
					throw new Exception("Usuário não encontrado");

				await DisplayAlert(null, "E-mail com as intruções de redefinição de senha enviados", "Ok");
			}
			catch (Exception ex)
			{
				await DisplayAlert(null, ex.Message, "Ok");
			}
			finally
			{
				LockEmailAndPassForm(_searchButton, false);
			}
		}
	}
}
