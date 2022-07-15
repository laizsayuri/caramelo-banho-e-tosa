using CarameloApp.Data;
using CarameloApp.Models;
using Xamarin.Forms;

namespace CarameloApp.Services
{
	/// <summary>
	/// Service para operações envolvendo Usuários
	/// </summary>
	public class UserService
	{
		private readonly UserRepository _userRepository;		
		private readonly SessionService _sessionService;

		public UserService()
		{
			_userRepository = DependencyService.Get<UserRepository>();
			_sessionService = DependencyService.Get<SessionService>();
		}

		public void Update(User user)
		{
			var currentUser = _sessionService.GetUser();
			user.SyncCredentials(currentUser);

			_userRepository.Update(user);
			_sessionService.UpdateUser();
		}

		public User GetByEmailAndPass(string email, string pass)
		{
			return _userRepository.GetByEmailAndPass(email, User.EncodePassword(pass));
		}

		public User GetByEmail(string email) => _userRepository.GetByEmail(email);

		public void Insert(User user) => _userRepository.Insert(user);
	}
}
