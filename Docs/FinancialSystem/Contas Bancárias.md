# BankAccountEndpoints

## Authentication

This API uses Bearer Authentication. To authenticate, provide your bearer token in the `Authorization` header of your HTTP requests.

Example:

Authorization: Bearer your_token_here

## Endpoints

### POST /api/bankaccount/create

Creates a new bank account.

#### Request

```json
{
    "AccountName": "string",
    "AccountNumber": "string",
    "BankCode": "string",
    "Agency": "string",
    "AccountType": "string",
    "Balance": "decimal"
}
```

#### Response

```json
{
  "Id": "Guid",
  "AccountNumber": "string",
  "BankCode": "string",
  "Agency": "string",
  "AccountType": "string",
  "Balance": "decimal"
}
```
---
### GET /api/bankaccount/
Gets the user's bank accounts.

#### Response

```json
[
    {
        "Id": "Guid",
        "AccountName": "string",
        "AccountNumber": "string",
        "BankCode": "string",
        "Agency": "string",
        "AccountType": "string",
        "Balance": "decimal"
    }
]
```
---
### PUT /api/bankaccount/{bankAccountId}
Updates a bank account.

#### Request

```json
{
    "AccountName": "string",
    "AccountNumber": "string",
    "BankCode": "string",
    "Agency": "string",
    "AccountType": "string",
    "Balance": "decimal"
}
```

#### Response
Status code 200 if successful.

---

### DELETE /api/bankaccount/{bankAccountId}
Deletes a bank account.

#### Response
Status code 204 if successful.