using Microsoft.Win32;
using ToDoList.Data.Models;
using ToDoList.Repositories;
using ToDoList.Utils;
using ToDoList.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Services
{
	public class LoginService
	{
		public readonly LoginRepository _repository;
		public readonly MailService _mailService;
		public readonly UserRepository _userRepository;
		private readonly TokenService _tokenServices;

		public LoginService(LoginRepository repository, MailService mailService, UserRepository userRepository, TokenService tokenServices)
		{
			_repository = repository;
			_mailService = mailService;
			_userRepository = userRepository;
			_tokenServices = tokenServices;
		}

		public ApiResponse<UserResponse> Login(string user, string password)
		{
			UserResponse? response = _repository.Login(user, password)?.ToModelResponse() ;

			var message = "Usuário ou senha inválidos";
			var codResponse = 404;

			if (response != null)
			{
				message = "Sucesso";
				codResponse = 200;
			}

			return new ApiResponse<UserResponse>(codResponse, response, message);
		}

		public ApiResponse<UserResponse> LoginWithMailAndPasswordRecovery(string mail, string password)
		{
			try
			{
				var message = "E-mail ou senha inválidos";
				var codResponse = 404;

				UserResponse? response = null;

				if (string.IsNullOrEmpty(mail) && string.IsNullOrEmpty(password) && !_userRepository.IsValidMail(mail))
				{
					message = "Para fazer login com após a recuperação de Senha, é necessário enviar o e-mail e a senha que você recebeu";

					return new ApiResponse<UserResponse>(codResponse, response, message);
				}

				var loginRecovery = _repository.LoginWithMailAndPasswordRecovery(mail, password);

				if (loginRecovery != null)
				{
					if (loginRecovery.ExpirationDate < DateTime.Now)
					{
						return new ApiResponse<UserResponse>(404, null, "Infelizmente essa nova senha está expirada! Solicite uma nova em esqueci minha senha");
					}

					response = _userRepository.ResetPasswordRevoveryToUser(mail, password)?.ToModelResponse();

					//Excluir a recuperaçãod e senha da tabela, porque o usuário já conseguiu logar com ela
					_repository.DeletePasswordRecovery(loginRecovery);
				}

				if (response != null)
				{
					message = "Login com a senha recuperada, realizado com sucesso";
					codResponse = 200;
				}

				return new ApiResponse<UserResponse>(codResponse, response, message);
			}
			catch
			{
				return new ApiResponse<UserResponse>(500, null, "Erro ao registrar sua nova senha, tente novamente ou entre em contato com o Administrador");
			}
		}

		public ApiResponse<object> GeneratePasswordRecovery(string mail)
		{
			if (!_userRepository.IsValidMail(mail))
			{
				return new ApiResponse<object>(404, null, "Esse e-mail não existe, por favor verifique!");
			}

			try
			{
				var code = _tokenServices.GeneatePasswordRecovery();

				int expirationTimeMinutes = 10;

				_repository.PasswordRevovery(mail, code, expirationTimeMinutes);

				var sendEmail = _mailService.SendPasswordResetEmailAsync(mail, code, expirationTimeMinutes);


				return sendEmail.Result;

			}
			catch
			{
				return new ApiResponse<object>(500, null, "Erro ao Enviar código para a recuperação de senha. Tente novamente ou entre em contato com o Administrador");
			}	
		}
	}
}
