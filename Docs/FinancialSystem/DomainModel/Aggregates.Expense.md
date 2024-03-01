# Domain Models

## Expense

```csharp
class Expense{
    ErrorOr<Expense> Create(
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? duePaymentDate,
            DateTime? paymentDate,
            bool paid,
            bool recurrent);
    ErrorOr<Success> AddPayment(Payment payment);
    ErrorOr<Success> RemovePayment(Payment payment);
    ErrorOr<Success> UpdateExpenseDetails(ExpenseDetails details);
}
```
```json
{
  "expense": {
    "id": { "value":"00000000-0000-0000-0000-000000000000" },
    "userId": "00000000-0000-0000-0000-000000000000",
    "description": "",
    "amount": 0.0,
    "creationDate": "0001-01-01T00:00:00",
    "dueDate": "0001-01-01T00:00:00",
    "paymentDate": "0001-01-01T00:00:00",
    "paid": "false",
    "recurrent": "false",
    "payments":[{
        "id":  { "value":"00000000-0000-0000-0000-000000000000" },
        "paymentDate": "0001-01-01T00:00:00",
        "description": "",
        "amount": 0.0
    }]
  }
}
```