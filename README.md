# 🏟️ Gerencial Clube API

A **Gerencial Clube API** é uma aplicação ASP.NET Core voltada para a gestão de sócios, planos e acessos em clubes, utilizando práticas modernas de desenvolvimento como **CQRS**, **Clean Architecture**, autenticação via **JWT**, e testes automatizados.

---

## 📐 Arquitetura

O projeto segue os princípios da **Clean Architecture** com separação clara entre camadas:

- **GerencialClube.Dominio**: contém as entidades de domínio e contratos de repositórios.
- **GerencialClube.Aplicacao**: responsável pelos casos de uso (Application Services), DTOs, validadores e mapeamentos.
- **GerencialClube.Infra**: implementações concretas dos repositórios e contexto do Entity Framework Core.
- **GerencialClube.WebApi**: ponto de entrada da aplicação (controllers, middlewares, autenticação).
- **GerencialClube.Tests**: testes unitários e de integração com apoio de builders e mocks.

Além disso, é adotado o padrão **CQRS** (Command Query Responsibility Segregation), separando operações de leitura e escrita para maior clareza e escalabilidade.

---

## 🧰 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core (EF Core)
- AutoMapper
- FluentValidation
- JWT (JSON Web Tokens)
- Swagger / OpenAPI
- Bogus (para gerar dados fake nos testes)
- Moq (para mocks em testes)
- xUnit (testes automatizados)

---

## 🔐 Autenticação

A API utiliza autenticação **JWT** para proteção das rotas sensíveis.  
Para consumir endpoints autenticados, inclua o token no cabeçalho da requisição:

```
Authorization: Bearer {seu_token}
```

> No Swagger, após autenticar, clique em **Authorize** e insira `Bearer {seu_token}`.

---

## ⚙️ Configuração `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server={SeuServidor};Database=GerencialClube;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "ApiSettings": {
    "ViaCepBaseUrl": "https://viacep.com.br/ws/"
  },
  "JwtSettings": {
    "SecretKey": "ChaveSuperSecretaMultiClubes1234567890!@#",
    "Issuer": "GerencialClubeAPI",
    "Audience": "GerencialClubeAPIUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 📡 Endpoints da API

### 🔐 Autenticação

- `POST /api/Autenticacao/login`  
  → Realiza login e retorna token JWT

### 👥 Sócios

- `POST /api/socios`  
  → Cria um novo sócio
- `GET /api/socios`  
  → Retorna todos os sócios
- `GET /api/socios/{id}`  
  → Retorna um sócio pelo ID
- `PUT /api/socios`  
  → Atualiza os dados de um sócio
- `DELETE /api/socios/{id}`  
  → Remove um sócio

### 📅 Acessos

- `POST /api/acessos`  
  → Registra tentativa de acesso de sócio
- `GET /api/acessos/socio/{socioId}`  
  → Lista todos os acessos de um sócio

### 🧾 Planos

- `POST /api/planos`  
  → Cria um novo plano
- `GET /api/planos`  
  → Retorna todos os planos
- `GET /api/planos/{id}`  
  → Retorna um plano específico
- `PUT /api/planos`  
  → Atualiza um plano
- `DELETE /api/planos/{id}`  
  → Remove um plano

---

## 🗃️ Atualizando o Banco de Dados

A aplicação utiliza **Entity Framework Core** com Migrations.

### ⚠️ As migrations já estão criadas no projeto. Para aplicar:

> No **Package Manager Console** do Visual Studio, execute:

```powershell
Update-Database -Context ContextoGerencialClube -Project GerencialClube.Infra -StartupProject GerencialClube.WebApi
```

---

## 🧪 Testes Automatizados

A solução possui cobertura de:

- ✅ **Testes Unitários**: focados em métodos de domínio e serviços com mocks (`Moq`)
- 🔁 **Testes de Integração**: testam fluxos completos com EF Core InMemory, Builders com `Bogus`, e simulações reais de banco

### Builders disponíveis:

- `SocioBuilder`
- `PlanoBuilder`
- `EnderecoBuilder`
- `ContatoBuilder`

> Isso permite simular objetos complexos com facilidade e reutilização.

---

## 🗂️ Organização da Solução

```
GerencialClube.sln
├── GerencialClube.Dominio           # Entidades e contratos
├── GerencialClube.Aplicacao         # Serviços de aplicação, DTOs, mapeamentos
├── GerencialClube.Infra             # EF Core, repositórios, configurações
├── GerencialClube.WebApi            # Controllers, autenticação, middlewares
├── GerencialClube.Tests             # Testes unitários e de integração
```

---

## 📌 Observações Finais

- As requisições de CEP são feitas via API externa [ViaCEP](https://viacep.com.br).
- Enumerações como `AreaClube` e `TipoContato` são tratadas com `JsonStringEnumConverter`.
- Toda a comunicação segue o padrão REST com tratamento global de exceções via middleware.

---

## 👨‍💻 Contato e Suporte

Este projeto foi desenvolvido por **Bruno Tomm**.  
Para dúvidas técnicas, solicite suporte diretamente à equipe de desenvolvimento.
