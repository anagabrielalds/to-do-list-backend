using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoList.ViewModel;

namespace ToDoList.Services
{
	public class TokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(UserResponse user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Username.ToString()),
					
				}),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			if(!string.IsNullOrEmpty(user?.Role?.ToString()))
			{
				tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
			}

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public string GeneatePasswordRecovery()
		{
			int comprimento = 8;

			Random random = new Random();
			string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			StringBuilder senha = new StringBuilder(comprimento);

			for (int i = 0; i < comprimento; i++)
			{
				int indice = random.Next(caracteresPermitidos.Length);
				senha.Append(caracteresPermitidos[indice]);
			}

			return senha.ToString();
		}
	}
}
