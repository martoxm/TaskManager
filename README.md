# TaskManager API

TaskManager API é uma API REST desenvolvida em ASP.NET Core com .NET 10, Entity Framework Core e MySQL.  
O projeto segue uma arquitetura em camadas com separação entre API, Application, Domain e Infrastructure.

## Funcionalidades

- Criar uma nova tarefa.
- Listar todas as tarefas.
- Buscar uma tarefa por ID.
- Atualizar uma tarefa existente.
- Excluir uma tarefa.
- Validação de entrada com respostas de erro claras.
- Documentação com Swagger para testar os endpoints.

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- C#
- Entity Framework Core
- Pomelo.EntityFrameworkCore.MySql
- MySQL
- Swagger / OpenAPI

## Estrutura do projeto

- `TaskManager.Api` - camada da API, controllers, middleware e Swagger.
- `TaskManager.Application` - DTOs, services e regras de aplicação.
- `TaskManager.Domain` - entidades, enums, exceções e contratos de repositório.
- `TaskManager.Infrastructure` - EF Core, configuração do MySQL, repositórios e migrations.

## Requisitos

- .NET 10 SDK
- MySQL Server
- Visual Studio 2026

## Configuração

Atualize a connection string no arquivo `TaskManager.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "MySQL": "Server=localhost;Database=taskmanager;Uid=root;Pwd=sua-senha;"
}
```

Certifique-se de que a versão do MySQL configurada no `Program.cs` seja compatível com a versão instalada no seu computador.

## Como executar

1. Abra a solution no Visual Studio.
2. Restaure os pacotes NuGet.
3. Defina `TaskManager.Api` como projeto de inicialização.
4. Execute a aplicação.
5. Abra o Swagger para testar os endpoints.

## Swagger

Com a aplicação em execução, acesse:

- `/swagger`

## Licença

Projeto desenvolvido para fins de estudo e portfólio.