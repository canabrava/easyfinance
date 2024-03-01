# Domain Models

## Income

```csharp
class Income{
    ErrorOr<Income> Create(
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? expectedReceiptDate,
            DateTime? receiptDate,
            bool received,
            bool recurrent);
    ErrorOr<Success> ReceiveIncome(DateTime receiptDate);
    ErrorOr<Success> Update(Income income)
}
```
```json
{
  "income": {
    "id":  { "value":"00000000-0000-0000-0000-000000000000" },
    "userId": "00000000-0000-0000-0000-000000000000",
    "description": "",
    "amount": 0.0,
    "creationDate": "0001-01-01T00:00:00",
    "expectedReceiptDate": "0001-01-01T00:00:00",
    "receiptDate": "0001-01-01T00:00:00",
    "received": "false",
    "recurrent": "false",
  }
}
```