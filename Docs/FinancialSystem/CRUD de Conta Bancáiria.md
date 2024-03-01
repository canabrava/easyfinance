# CRUD de Contas Bancárias - EasyFinance

## Descrição

Esta documentação descreve o processo de CRUD (Create, Read, Update, Delete) para contas bancárias no sistema EasyFinance. Este documento cobre a funcionalidade de criação (Create) de contas bancárias, permitindo aos usuários adicionar novas contas ao sistema.

## Endpoint

POST /api/bankaccount/create

## Request

### Headers

- Content-Type: application/json
- Accept: application/json
- Authorization: Bearer JWT_Token

### Body

```json
{
  "accountNumber": "string",
  "bankCode": "string",
  "agency": "string",
  "accountType": "string",
  "balance": "decimal"
}
```

###  Parâmetros:

- accountNumber: Número da conta bancária.
- bankCode: Código do banco.
- agency: Número da agência.
- accountType: Tipo de conta (ex: corrente, poupança).
- balance: Saldo inicial da conta.

## Response

### Sucesso (201 Created)

```json
{
  "bankAccount": {
    "id": "Guid",
    "accountNumber": "string",
    "bankCode": "string",
    "agency": "string",
    "accountType": "string",
    "balance": "decimal",
    "createdAt": "datetime"
  }
}
```

### Campos:

- bankAccount: Objeto contendo informações da - conta bancária criada.
- id: Identificador único da conta bancária, - tipo Guid.
- accountNumber: Número da conta bancária.
- bankCode: Código do banco.
- agency: Número da agência.
- accountType: Tipo de conta.
- balance: Saldo da conta.
- createdAt: Data e hora da criação da conta.

### Erro (400 Bad Request)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 400,
  "traceId": "00-da826b902d200fc35a371084cc74e8f6-324a3db9e4fdbfc0-00",
  "errors": {
    "bankCode": [
      "Invalid bank code."
    ]
  }
}
```

## Possíveis Erros:

- Dados de conta bancária incompletos ou inválidos.
- Formato de saldo incorreto.
- Erro de validação em qualquer um dos campos.

Claro, aqui está a continuação do seu arquivo Markdown (.md) para documentar as outras ações do CRUD (Read, Update, Delete) no sistema EasyFinance.

---

## Read (Ler)

### Descrição

Este endpoint é usado para consultar as informações de contas bancárias existentes no sistema.

### Endpoint

GET /api/bankaccount/read/{id}

### Parâmetros

- `id`: Identificador único da conta bancária (Guid).

### Response

#### Sucesso (200 OK)

```json
{
  "bankAccount": {
    "id": "Guid",
    "accountNumber": "string",
    "bankCode": "string",
    "agency": "string",
    "accountType": "string",
    "balance": "decimal",
    "createdAt": "datetime"
  }
}
```

#### Erro (404 Not Found)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Account not found.",
  "status": 404,
  "traceId": "Trace ID"
}
```
---
## Update (Atualizar)

### Descrição

Este endpoint permite atualizar informações de uma conta bancária existente.

### Endpoint

PUT /api/bankaccount/update/{id}

### Parâmetros

- `id`: Identificador único da conta bancária (Guid).

### Body

```json
{
  "accountNumber": "string",
  "bankCode": "string",
  "agency": "string",
  "accountType": "string",
  "balance": "decimal"
}
```

### Response

#### Sucesso (200 OK)

```json
{
  "message": "Bank account updated successfully."
}
```

#### Erro (400 Bad Request)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 400,
  "traceId": "Trace ID",
  "errors": {
    "field_name": [
      "Error message."
    ]
  }
}
```
---
## Delete (Deletar)

### Descrição

Este endpoint permite deletar uma conta bancária existente do sistema.

### Endpoint

DELETE /api/bankaccount/delete/{id}

### Parâmetros

- `id`: Identificador único da conta bancária (Guid).

### Response

#### Sucesso (204 No Content)

Não retorna corpo na resposta.

#### Erro (404 Not Found)

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Account not found.",
  "status": 404,
  "traceId": "Trace ID"
}
```