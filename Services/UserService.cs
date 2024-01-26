using ToDoList.Data.Models;
using ToDoList.Interface;
using ToDoList.Repositories;
using ToDoList.Utils;
using ToDoList.ViewModel;

namespace ToDoList.Services
{
	public class UserService : BaseService, IValidation
	{
		public readonly UserRepository _repository;

		public UserService(IHttpContextAccessor httpContextAccessor, UserRepository repository) : base(httpContextAccessor)
		{
			_repository = repository;
		}

		public ApiResponse<List<User>> Get()
		{
			if (Role != "ADMIN")
				return new ApiResponse<List<User>>(401, null, "Você não tem permissão para pegar informações de todos os  usuário");

			List<User> lista = _repository.Get(u => 1 == 1).ToList();

			if (lista != null && lista.Count > 0)
				return new ApiResponse<List<User>>(200, lista, "Sucesso");
			else
				return new ApiResponse<List<User>>(404, null, "Não há usuários cadastrados");
		}

		public ApiResponse<UserResponse> GetUser(int? idUser)
		{
			var id = 0;
			if (idUser == null || idUser == 0) id = IdUserLogado;

			if (!IsValidId(id)) return new ApiResponse<UserResponse>(404, null, "o Id informado não é válido");
			if (!IsResourceOwner(id)) return new ApiResponse<UserResponse>(401, null, "Você não tem permissão para pegar informações desse usuário");

			User user = _repository.GetByField(u => u.Id == id);

			return new ApiResponse<UserResponse>(200, user.ToModelResponse(), "Sucesso");
		}

		public ApiResponse<UserResponse> Post(UserRequest user)
		{
			if (string.IsNullOrEmpty(user.Mail))
				return new ApiResponse<UserResponse>(200, null, "O campo email é um campo obrigatório");

			if (string.IsNullOrEmpty(user.Username))
				return new ApiResponse<UserResponse>(200, null, "O campo username é um campo obrigatório");

			if (string.IsNullOrEmpty(user.Username))
				return new ApiResponse<UserResponse>(200, null, "O campo de Senha é um campo obrigatório");

			if (_repository.GetByField(u => u.Username == user.Username || u.Mail == user.Mail) != null)
				return new ApiResponse<UserResponse>(200, null, "Username ou Email indisponível para novo cadastro. Qualquer dúvida, contate o administrador");

			try
			{
				var newUser = user.ToModel(null);
				var result = _repository.Post(newUser).ToModelResponse();

				return new ApiResponse<UserResponse>(200, result, "Usuário adicionada com sucesso");
			}
			catch
			{
				return new ApiResponse<UserResponse>(500, null, "Erro ao se cadastrar");
			}
		}

		public ApiResponse<UserResponse> Update(UserUpdateRequest request)
		{
			var id = IdUserLogado;

			if (!IsValidId(id)) return new ApiResponse<UserResponse>(404, null, "o Id informado não é válido");
			if (!IsResourceOwner(id))return new ApiResponse<UserResponse>(401, null, "Você não tem permissão para excluir informações desse usuário");

			try
			{
				var user = _repository.GetByField(u => u.Id == id);

				if(!string.IsNullOrWhiteSpace(request.Username))
				user.Username = request.Username;

				user.ImagePath = request.ImagePath;

				if (!string.IsNullOrWhiteSpace(request.Mail))
					user.Mail = request.Mail;

				var model = _repository.Update(user);

				return new ApiResponse<UserResponse>(200, model.ToModelResponse(), "Usuário atualizado com sucesso");
			}
			catch (Exception)
			{
				return new ApiResponse<UserResponse>(500, null, "Erro ao atualizar usuário");
			}
		}

		public ApiResponse<UserResponse> ResetPassword(string passwordOld, string passwordNew)
		{
			try
			{
				var message = "";
				var codResponse = 404;

				UserResponse? response = null;

				if (string.IsNullOrEmpty(passwordOld) && string.IsNullOrEmpty(passwordNew))
				{
					message = "Para o redefinir sua senha, precisamos que informe a sua senha antiga e a nova senha";

					return new ApiResponse<UserResponse>(codResponse, response, message);
				}

				var user = _repository.GetByField(u => u.Username == UserLogado && u.Password == passwordOld);

				if (user != null)
				{
					user.Password = passwordNew;
					response = _repository.Update(user).ToModelResponse();
				}
				else
				{
					return new ApiResponse<UserResponse>(404, null, "Infelizmente não foi possível redefinir sua senha porque a senha informada não coincide com a senha atual");
				}

				if (response != null)
				{
					message = "Senha alterada com sucesso! Entra novamente com a nova senha";
					codResponse = 200;
				}

				return new ApiResponse<UserResponse>(codResponse, response, message);
			}
			catch
			{
				return new ApiResponse<UserResponse>(500, null, "Erro ao redefinir senha, tente novamente ou entre em contato com o Administrador");
			}
		}

		public ApiResponse<UserResponse> Delete()
		{
			var id = IdUserLogado;
			if (!IsValidId(id)) return new ApiResponse<UserResponse>(404, null, "o Id informado não é válido");
			if (!IsResourceOwner(id)) return new ApiResponse<UserResponse>(401, null, "Você não tem permissão para excluir informações desse usuário");

			try
			{
				var taskEntity = _repository.GetByField(e => e.Id == id);

				_repository.Delete(taskEntity);

				return new ApiResponse<UserResponse>(200, null, "Usuário excluído com sucesso");
			}
			catch (Exception ex)
			{
				return new ApiResponse<UserResponse>(500, null, "Erro ao excluir Usuário...");
			}
		}

		public bool IsResourceOwner(int resourceId)
		{
			var retorno = _repository.GetByField(i => i.Id == resourceId && i.Id == IdUserLogado);

			return retorno != null;
		}

		public bool IsValidId(int id)
		{
			var retorno = _repository.GetByField(i => i.Id == id);

			return retorno != null;
		}

		
	}
}
