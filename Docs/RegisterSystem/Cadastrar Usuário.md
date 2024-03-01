# Cadastro de Usuário - EasyFinance

## Descrição

Esta documentação detalha a função de cadastro de usuário no sistema EasyFinance. A função permite o registro de novos usuários no sistema, exigindo informações essenciais para a criação da conta.

## Endpoint

POST /api/user/register

## Request

### Headers

- Content-Type: application/json
- Accept: application/json

### Body

```json
{
  "name": "string",
  "email": "string",
  "senha": "string"
}
```

**Parâmetros:**

- **name**: Nome completo do usuário.
- **email**: Endereço de e-mail do usuário.
- **senha**: Senha para a conta do usuário.

## Response

### Sucesso (200 OK)

```json
{
  "id": "Guid",
  "name": "string",
  "email": "string",
  "dataCadastro": "datetime"
}
```

**Campos:**

- **id**: Identificador único do usuário, tipo Guid.
- **name**: Nome do usuário.
- **email**: E-mail do usuário.
- **dataCadastro**: Data e hora do cadastro do usuário.

### Erro (400 Bad Request)

```json
{
    "type": {
      "type": "string",
      "format": "uri"
    },
    "title": {
      "type": "string"
    },
    "status": {
      "type": "integer"
    },
    "traceId": {
      "type": "string",
      "pattern": "^[0-9a-fA-F-]+$"
    },
    "errors": {
      "type": "object",
      "additionalProperties": {
        "type": "string"
      }
    }
}
```

#### Exemplo

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 400,
  "traceId": "00-da826b902d200fc35a371084cc74e8f6-324a3db9e4fdbfc0-00",
  "errors": {
    "email": [
      "The email field is not a valid e-mail address."
    ]
  }
}
```


**Possíveis Erros:**

- E-mail já cadastrado.
- Dados inválidos.