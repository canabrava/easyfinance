# Domain Models

## BankAccount

```csharp
class BankAccount{
    ErrorOr<BankAccount> Create(
            string accountName,
            string accountNumber,
            string bankCode,
            string agency,
            string accountType,
            decimal? balance = null));
     ErrorOr<Success> UpdateBankAccountDetails(
            string accountName,
            string agency,
            string accountType)
    ErrorOr<Success> AddTransaction(FinancialTransaction transaction);
    ErrorOr<Success> RemoveTransaction(FinancialTransaction transaction);
}
```


```json
{
  "bankAccount": {
    "id":  { "value":"00000000-0000-0000-0000-000000000000" },
    "userId": "00000000-0000-0000-0000-000000000000",
    "accountNumber": "0000-0",
    "bankCode": "000",
    "agency": "0000",
    "accountType": "0",
    "balance": 0.0,
    "creationDate": "0001-01-01T00:00:00",
    "financialTransactions": [{
        "id": { "value":"00000000-0000-0000-0000-000000000000" },
        "expenseId":  { "value":"00000000-0000-0000-0000-000000000000" },
        "incomeId": { "value":"00000000-0000-0000-0000-000000000000" },
        "creationDate": "0001-01-01T00:00:00",
        "amount": 0.0,
    }]
  }
}
```