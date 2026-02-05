# BankSys API

## Descrição
A **BankSys API** é uma API REST desenvolvida em **.NET**, com foco em boas práticas de arquitetura, organização de código e clareza de responsabilidades.  
O projeto simula operações bancárias básicas, como criação de contas e transferências.

## Documentação para consumo da API
A API disponibiliza sua documentação interativa através do **Swagger (OpenAPI)**.

Após iniciar a aplicação, o Swagger pode ser acessado em:
http://localhost:5266/swagger

No Swagger é possível:
- Visualizar todos os endpoints disponíveis
- Entender os contratos de entrada e saída (DTOs)
- Executar requisições diretamente pela interface
- Validar regras de negócio e respostas da API

## Documentação para execução do projeto
### Pré-requisitos

- **.NET SDK 10**
- **Docker** e **Docker Compose** para subir e utilizar o banco de dados
- **SQLServer**

---

### Passo a passo para rodar localmente

#### 1. Clonar o repositório
```bash
git clone https://github.com/RafaelVinicius-97/BankSys.git
```
#### 2. Subir o banco de dados com Docker Compose
```bash
cd BankSys

docker compose up -d

dotnet ef database update \
  --project BankSys.Persistence/BankSys.Persistence.csproj \
  --startup-project BankSys.Api/BankSys.Api.csproj
```

SQLServer ficará disponível em `localhost,1433` com usuário `sa` e senha `BankSys123!`.

#### 3. Executar a API
```bash
dotnet run --project BankSys.Api
```

## Arquitetura
API REST desenvolvida em .NET utilizando Arquitetura Limpa.

### BankSys.Domain

1. Entidades

2. Interfaces de repositório

3. Regras de negócio centrais

### BankSys.Application

1. Serviços de aplicação

2. Casos de uso

3. DTOs e validações

4. Orquestração das regras de negócio

### BankSys.Persistence

1. DbContext

2. Repositórios

3. Migrations

4. Unit of Work

### BankSys.Api

1. Controllers

2. Configuração da aplicação

3. Injeção de dependência