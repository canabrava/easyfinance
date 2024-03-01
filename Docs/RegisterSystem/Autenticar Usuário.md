
# Autenticação - EasyFinance

## Descrição

Esta documentação descreve a função de autenticação do sistema EasyFinance. A função é responsável por autenticar os usuários, utilizando JWT (JSON Web Token) para garantir a segurança e a integridade da sessão do usuário.

## Endpoint

POST /api/user/login

## Request

### Headers

- Content-Type: application/json
- Accept: application/json

### Body

```json
{
  "email": "string",
  "senha": "string"
}
```

**Parâmetros:**

- **email**: Endereço de e-mail do usuário.
- **senha**: Senha da conta do usuário.

## Response

### Sucesso (200 OK)

```json
{
  "user": {
    "id": "Guid",
    "name": "string",
    "email": "string"
  },
  "token": "JWT Token"
}
```

**Campos:**

- **user**: Objeto contendo informações do usuário.
  - **id**: Identificador único do usuário, tipo Guid.
  - **name**: Nome do usuário.
  - **email**: E-mail do usuário.
- **token**: Token JWT utilizado para autenticação e autorização nas demais requisições.

### Erro (401 Unauthorized)

```json
{
  "erro": "Credenciais inválidas"
}
```

**Possíveis Erros:**

- E-mail ou senha incorretos.
- Conta de usuário não encontrada.