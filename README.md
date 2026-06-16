# TaskManager API

API REST para gerenciamento de tarefas desenvolvida em ASP.NET Core, Entity Framework Core, MySQL e JWT.

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- JWT Bearer Authentication
- Swagger / OpenAPI

## Estrutura do projeto

- `TaskManager.Api` — camada de apresentação, controllers e configuração da aplicação.
- `TaskManager.Application` — serviços e interfaces da aplicação.
- `TaskManager.Domain` — entidades e contratos de domínio.
- `TaskManager.Infrastructure` — acesso a dados, DbContext, repositories e configurações do EF Core.

## Funcionalidades implementadas

- Cadastro de usuários.
- Autenticação com JWT.
- Criação, listagem, atualização e remoção de tarefas.
- Separação em camadas com Repository Pattern.
- Configuração de Swagger para testar endpoints protegidos.
- Integração com MySQL via Entity Framework Core.

## Etapas implementadas

### Etapa 1 — Criação da solução

Foi criada a solução com os projetos `TaskManager.Api`, `TaskManager.Application`, `TaskManager.Domain` e `TaskManager.Infrastructure`.

### Etapa 2 — Configuração das entidades

Foram criadas as entidades principais do sistema, como `User` e `Task`.

### Etapa 3 — Configuração do banco

Foi configurado o `DbContext` e a conexão com o banco MySQL.

### Etapa 4 — Configuração das migrations

Foram adicionadas migrations para criação das tabelas no banco.

### Etapa 5 — Autenticação JWT

Foi implementada a autenticação via JWT para proteger os endpoints da API.

### Etapa 6 — Repositories

Foram criados os repositórios para acesso aos dados:

- `UserRepository`
- `TaskRepository`

### Etapa 7 — Services

Foram criados os services responsáveis pela lógica de negócio:

- `AuthService`
- `UserService`
- `TaskService`

### Etapa 8 — Controllers

Foram criados os controllers para expor os endpoints da API.

### Etapa 9 — Swagger com autenticação

O Swagger foi configurado para aceitar token JWT no botão `Authorize`.

### Etapa 10 — Configuração do pipeline

O `Program.cs` foi configurado com:

- `AddControllers`
- `AddSwaggerGen`
- `AddAuthentication`
- `AddAuthorization`
- `UseAuthentication`
- `UseAuthorization`

### Etapa 11 — Validação dos endpoints

Os endpoints protegidos passaram a exigir token JWT válido para acesso.

### Etapa 12 — Ajustes finais

Foram feitos ajustes na configuração do projeto, nas classes de infraestrutura e na documentação da API.

## Como executar o projeto

### 1. Restaurar os pacotes

```bash
dotnet restore
```

### 2. Atualizar o banco de dados

```powershell
Update-Database -Project TaskManager.Infrastructure -StartupProject TaskManager.Api
```

### 3. Executar a API

```bash
dotnet run --project TaskManager.Api
```

### 4. Abrir o Swagger

Acesse:

```text
https://localhost:7175/swagger
```

## Autenticação no Swagger

1. Faça login no endpoint de autenticação.
2. Copie o valor do token retornado.
3. No Swagger, clique em `Authorize`.
4. Cole o token no formato:

```text
Bearer SEU_TOKEN_AQUI
```

5. Clique em `Authorize` e depois em `Close`.

## Endpoints principais

### Auth

- `POST /api/auth/login`
- `POST /api/auth/register`

### Users

- `GET /api/users/{id}`
- `POST /api/users`

### Tasks

- `GET /api/tasks`
- `GET /api/tasks/{id}`
- `POST /api/tasks`
- `PUT /api/tasks`
- `DELETE /api/tasks/{id}`

## Observações

- Os endpoints protegidos exigem token JWT válido.
- O projeto segue separação por camadas.
- O Swagger foi configurado para facilitar testes locais durante o desenvolvimento.

## Próximos passos

- Etapa 13: criação do projeto de testes.
- Etapa 14: testes unitários dos services.
- Etapa 15: testes de controllers e validações.
