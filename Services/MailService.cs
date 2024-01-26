using System.Net;
using System.Net.Mail;
using ToDoList.Utils;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace ToDoList.Services
{
	public class MailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<ApiResponse<object>> SendPasswordResetEmailAsync(string destinatario, string code, int expirationTimeMinutes)
		{
			try
			{
				var remetente = _configuration["STMP_mailtrap:remetente"];
				string assunto = "Recuperação de Senha";

				var hostSMTP = _configuration["STMP_mailtrap:hostSMTP"];

				int.TryParse(_configuration["STMP_mailtrap:portSMTP"], out int portSMTP);

				var userSMTP = _configuration["STMP_mailtrap:userSMTP"];

				var passwordSMTP = _configuration["STMP_mailtrap:passwordSMTP"];


				string caminhoArquivoHTML = "Utils\\templateMailRecoveryPassword.html";
				string corpoHTML = File.ReadAllText(caminhoArquivoHTML);

				corpoHTML = corpoHTML.Replace("[codigo]", code );
				corpoHTML = corpoHTML.Replace("[validade]", expirationTimeMinutes.ToString());
				corpoHTML = corpoHTML.Replace("[username]", destinatario);


				using (var client = new SmtpClient(hostSMTP, portSMTP))
				{
					client.Credentials = new NetworkCredential(userSMTP, passwordSMTP);
					client.EnableSsl = true;

					using (MailMessage mailMessage = new MailMessage(remetente, destinatario, assunto, corpoHTML))
					{
						mailMessage.IsBodyHtml = true;

						await client.SendMailAsync(mailMessage);
					}
				};

				return new ApiResponse<object>(200, null, $"Você receberá um email com o seu token de acesso, ele tem validadade de {expirationTimeMinutes} minutos");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
	}
}
