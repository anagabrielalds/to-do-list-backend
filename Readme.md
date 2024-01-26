
# Web API To-Do List

Esta é uma Web API para gerenciamento de tarefas (To-Do List) com suporte a autenticação JWT, banco de dados SQLite usando EF Core, e envio de e-mails para recuperação de senha com o Mailtrap.io. A API está documentada utilizando Swagger.


## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core (EF Core)
- SQLite
- JSON Web Token (JWT) para Autenticação
- Mailtrap.io para envio de e-mails
- Swagger para Documentação da API

## Configuração do Projeto

### Requisitos

- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Mailtrap.io](https://mailtrap.io/) conta para configuração de e-mail


### Passos de Configuração

1. Clone o repositório para a sua máquina local.
   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git
   ```

2. Se não houver o banco de dados criado dentro do projeto, você pode rodar a migrations para atualizar/criar o banco de dados.
   ```bash
   dotnet ef database update
   ```

3. Configure as informações de autenticação JWT no arquivo `appsettings.json`. Essa configuração será usada para como uma chave para criar Tokens únicos
   ```json
   {
      "Secret" : ""
     // outras configurações...
   }
   ```

4. Configure as informações do Mailtrap.io para envio de e-mails de recuperação de senha no arquivo `appsettings.json`.
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
   - Remetente: será o email de quem irá enviar o email
   - As demais informações devem ser obtidas no https://mailtrap.io/  após fazer o seu cadastro. Eles disponibilizam um SMTP para teste.

5. Execute as migrações para criar o banco de dados SQLite.
   ```bash
   dotnet ef database update
   ```

6. Comando para criar um migation caso faça alguma modificação nas models
   ```bash
   dotnet ef migrations add NomeMigration
   ```

7. Comando para rodar a aplicação localmente
   ```bash
   dotnet run
   ```

## Documentação da API

A documentação da API está disponível utilizando o Swagger. Para acessar, rode o projeto localmente e vá para `https://localhost:7195/swagger` no seu navegador.

## Regras de negócio

- Não é permitido criar usuário com um email ou username que já existe cadastrado
- Ao solicitar a recuperação de senha é enviado um email para o usuário e ele deve fazer o login no sistema com essa nova senha. Após o login o sistema exclui essa senha da tabela de recuperação de senha
- A senha enviada por email na recuperação de senha tem válida de 10 minutos, após isso não é mais possível logar com essa senha


