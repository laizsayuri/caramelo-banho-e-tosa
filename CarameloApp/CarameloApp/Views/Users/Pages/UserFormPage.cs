using CarameloApp.Models;
using CarameloApp.Services;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared.Components.ContentAreas;
using CarameloApp.Views.Shared.Components.Forms;
using CarameloApp.Views.Shared.Pages;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;

namespace CarameloApp.Views.Users.Pages
{
	/// <summary>
	/// Formulário de usuários
	/// </summary>
	public class UserFormPage : TabsRootPage
	{
		protected CustomEntry _nameEntry;
		protected CustomEntry _telephoneEntry;
		protected CustomDatePicker _birthDatePicker;
		protected CustomEntry _cepEntry;
		protected CustomEntry _numberEntry;
		protected CustomEntry _streetAddressEntry;
		protected CustomEntry _districtEntry;
		protected CustomPicker _cityPicker;
		protected CustomPicker _ufPicker;

		protected ActivityIndicator _activityIndicator;
		protected CustomEntry _emailEntry;
		protected CustomEntry _passEntry;
		protected CustomEntry _passConfirmEntry;

		protected readonly UserService _userService;
		protected readonly SessionService _sessionService;

		public UserFormPage(TabsPage root = null, ToolbarItem toolbarItem = null) : base(root, toolbarItem)
		{
			_userService = DependencyService.Get<UserService>();
			_sessionService = DependencyService.Get<SessionService>();
		}

		protected CustomEntry CreateEmailEntry()
		{
			_emailEntry = new CustomEntry
			{
				Keyboard = Keyboard.Email
			};

			return _emailEntry;
		}

		protected CustomEntry CreatePassEntry()
		{
			_passEntry = new CustomEntry
			{
				Keyboard = Keyboard.Text,
				IsPassword = true				
			};

			return _passEntry;
		}

		protected CustomEntry CreatePassConfirmEntry()
		{
			_passConfirmEntry = new CustomEntry
			{
				Keyboard = Keyboard.Text,
				IsPassword = true
			};

			return _passConfirmEntry;
		}

		protected CustomLabel CreateReturnLabel()
		{
			var returnLabel = new CustomLabel()
			{
				Text = "< voltar ao início"
			};
			returnLabel.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = ReturnTapCommand
			});

			return returnLabel;
		}

		protected ActivityIndicator CreateActivityIndicator()
		{
			_activityIndicator = new ActivityIndicator();
			return _activityIndicator;
		}

		protected void LockEmailAndPassForm(CustomButton button, bool shouldLock)
		{
			if(_activityIndicator != null)
			{
				_activityIndicator.IsEnabled = shouldLock;
				_activityIndicator.IsRunning = shouldLock;
				_activityIndicator.IsVisible = shouldLock;
			}

			if (_emailEntry != null)
				_emailEntry.IsEnabled = !shouldLock;

			if (_passEntry != null)
				_passEntry.IsEnabled = !shouldLock;

			if (_passConfirmEntry != null)
				_passConfirmEntry.IsEnabled = !shouldLock;

			button.IsEnabled = !shouldLock;
		}

		protected void ValidateEmailAndPassForm()
		{
			if (string.IsNullOrEmpty(_emailEntry.Text))
				throw new Exception("Campo e-mail obrigatório");

			if(!ValidateEmail(_emailEntry.Text))
				throw new Exception("Campo e-mail inválido");

			if (_passEntry != null && string.IsNullOrEmpty(_passEntry.Text))
				throw new Exception("Campo senha obrigatório");						
		}

		protected void ValidateNewEmailAndPassForm()
		{
			ValidateEmailAndPassForm();

			if (_passEntry.Text.Length < 6)
				throw new Exception("As senhas devem ter pelo menos 6 caracteres");

			if (string.IsNullOrEmpty(_passConfirmEntry.Text))
				throw new Exception("Confirmação de senha obrigatório");

			if (!_passEntry.Text.Equals(_passConfirmEntry.Text))
				throw new Exception("Senhas não conferem");
		}

		protected void ValidateForm()
		{
			if (string.IsNullOrEmpty(_nameEntry.Text))
				throw new Exception("Campo nome obrigatório");

			if (string.IsNullOrEmpty(_telephoneEntry.Text))
				throw new Exception("Campo telefone obrigatório");

			if (!ValidadeTelephone(_telephoneEntry.Text))
				throw new Exception("Campo telefone inválido");

			if (string.IsNullOrEmpty(_cepEntry.Text))
				throw new Exception("Campo CEP obrigatório");

			if (!ValidadeCEP(_cepEntry.Text))
				throw new Exception("Campo CEP inválido");

			if (string.IsNullOrEmpty(_numberEntry.Text) || !int.TryParse(_numberEntry.Text, out int number) || number == 0)
				throw new Exception("Campo número obrigatório");

			if (string.IsNullOrEmpty(_streetAddressEntry.Text))
				throw new Exception("Campo rua obrigatório");

			if (string.IsNullOrEmpty(_districtEntry.Text))
				throw new Exception("Campo bairro obrigatório");

			if (string.IsNullOrEmpty((string)_cityPicker.SelectedItem))
				throw new Exception("Campo cidade obrigatório");

			if (string.IsNullOrEmpty((string)_ufPicker.SelectedItem))
				throw new Exception("Campo UF obrigatório");
		}

		private ICommand ReturnTapCommand => new Command(async () =>
		{
			await Navigation.PopAsync();
		});

		private bool ValidateEmail(string email)
		{
			var match = Regex.Match(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return match.Success;
		}

		private bool ValidadeCEP(string cep)
		{
			var match = Regex.Match(cep, @"^[0-9]{5}-[0-9]{3}$");
			return match.Success;
		}

		private bool ValidadeTelephone(string telephone)
		{
			var match = Regex.Match(telephone, @"^\([0-9]{2}\) [0-9]{5}-[0-9]{4}$");
			return match.Success;
		}

		protected Center GenerateCenterFormStackLayout(User user = null)
		{
			var newUser = user == null;

			_nameEntry = new CustomEntry
			{
				Text = user?.Name,
				Keyboard = Keyboard.Text,
			};

			_telephoneEntry = new CustomEntry
			{
				Keyboard = Keyboard.Telephone,
				MaxLength = 15
			};
			_telephoneEntry.Behaviors.Add(new MaskedBehavior
			{
				Mask = "(XX) XXXXX-XXXX",
				UnMaskedCharacter = 'X'
			});
			_telephoneEntry.Text = user?.Telephone;

			_birthDatePicker = new CustomDatePicker
			{
				Date = newUser ? new DateTime() : user.BirthDate,
				MinimumDate = new DateTime(1950, 1, 1),
				MaximumDate = DateTime.Today.AddYears(-18)
			};

			_cepEntry = new CustomEntry
			{
				Keyboard = Keyboard.Numeric,
				MaxLength = 9
			};
			_cepEntry.Behaviors.Add(new MaskedBehavior
			{
				Mask = "XXXXX-XXX",
				UnMaskedCharacter = 'X'
			});
			_cepEntry.Text = user?.CEP;

			_numberEntry = new CustomEntry
			{
				Text = newUser ? null : user.Number.ToString(),
				Keyboard = Keyboard.Numeric,
			};

			_streetAddressEntry = new CustomEntry
			{
				Text = user?.StreetAddress,
				Keyboard = Keyboard.Text,
			};

			_districtEntry = new CustomEntry
			{
				Text = user?.District,
				Keyboard = Keyboard.Text,
			};

			var cities = GetCities(user?.UF);
			_cityPicker = new CustomPicker
			{
				ItemsSource = newUser ? null : cities,
				IsEnabled = !newUser,
			};
			_cityPicker.SelectedIndex = newUser ? -1 : cities.IndexOf(user.City);

			_ufPicker = new CustomPicker
			{
				ItemsSource = User.UFs
			};
			_ufPicker.SelectedIndex = newUser ? -1 : User.UFs.IndexOf(user.UF);
			_ufPicker.SelectedIndexChanged += ufPicker_SelectedIndexChanged;

			return new Center(new View[]
			{
				new CustomLabel
				{
					Text = "Nome do tutor"
				},
				_nameEntry,
				new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children = {
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "Telefone"
								},
								_telephoneEntry,
							}
						},
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "Data de Nascimento"
								},
								_birthDatePicker
							}
						},
					}
				},
				new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children =
					{
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "CEP"
								},
								_cepEntry
							}
						},
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "Número"
								},
								_numberEntry
							}
						}
					}
				},
				new CustomLabel
				{
					Text = "Rua"
				},
				_streetAddressEntry,
				new CustomLabel
				{
					Text = "Bairro"
				},
				_districtEntry,
				new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children =
					{
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "Município"
								},
								_cityPicker,
							}
						},
						new StackLayout
						{
							Orientation = StackOrientation.Vertical,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
							{
								new CustomLabel
								{
									Text = "UF"
								},
								_ufPicker
							}
						}
					}
				}
			});
		}

		private void ufPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			var uf = (string)_ufPicker.SelectedItem;
			_cityPicker.ItemsSource = GetCities(uf);
			_cityPicker.IsEnabled = true;
		}

		private List<string> GetCities(string uf)
		{
			switch (uf)
			{
				case "SC":
					return User.SCCities;
				case "SP":
					return User.SPCities;
				default:
					return null;
			}
		}
	}
}
