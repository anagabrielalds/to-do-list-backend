
# Web API To-Do List

Esta � uma Web API para gerenciamento de tarefas (To-Do List) com suporte a autentica��o JWT, banco de dados SQLite usando EF Core, e envio de e-mails para recupera��o de senha com o Mailtrap.io. A API est� documentada utilizando Swagger.


## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core (EF Core)
- SQLite
- JSON Web Token (JWT) para Autentica��o
- Mailtrap.io para envio de e-mails
- Swagger para Documenta��o da API

## Configura��o do Projeto

### Requisitos

- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Mailtrap.io](https://mailtrap.io/) conta para configura��o de e-mail


### Passos de Configura��o

1. Clone o reposit�rio para a sua m�quina local.
   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git
   ```

2. Se n�o houver o banco de dados criado dentro do projeto, voc� pode rodar a migrations para atualizar/criar o banco de dados.
   ```bash
   dotnet ef database update
   ```

3. Configure as informa��es de autentica��o JWT no arquivo `appsettings.json`. Essa configura��o ser� usada para como uma chave para criar Tokens �nicos
   ```json
   {
      "Secret" : ""
     // outras configura��es...
   }
   ```

4. Configure as informa��es do Mailtrap.io para envio de e-mails de recupera��o de senha no arquivo `appsettings.json`.
   ```json
   {
    "STMP_mailtrap": 
        {
            "remetente": "",
            "hostSMTP": "",
            "portSMTP": 2525,
            "userSMTP": "",
            "passwordSMTP": ""
        }
    }
   ```
   - Remetente: ser� o email de quem ir� enviar o email
   - As demais informa��es devem ser obtidas no https://mailtrap.io/  ap�s fazer o seu cadastro. Eles disponibilizam um SMTP para teste.

5. Execute as migra��es para criar o banco de dados SQLite.
   ```bash
   dotnet ef database update
   ```

6. Comando para criar um migation caso fa�a alguma modifica��o nas models
   ```bash
   dotnet ef migrations add NomeMigration
   ```

7. Comando para rodar a aplica��o localmente
   ```bash
   dotnet run
   ```

## Documenta��o da API

A documenta��o da API est� dispon�vel utilizando o Swagger. Para acessar, rode o projeto localmente e v� para `https://localhost:7195/swagger` no seu navegador.

## Regras de neg�cio

- N�o � permitido criar usu�rio com um email ou username que j� existe cadastrado
- Ao solicitar a recupera��o de senha � enviado um email para o usu�rio e ele deve fazer o login no sistema com essa nova senha. Ap�s o login o sistema exclui essa senha da tabela de recupera��o de senha
- A senha enviada por email na recupera��o de senha tem v�lida de 10 minutos, ap�s isso n�o � mais poss�vel logar com essa senha


