# Domain Models

## Payment

```csharp
class Payment{
     ErrorOr<Payment> Create(DateTime date, decimal amount, string description);
}
```
```json
{
    "payment":{
      "id":  { "value":"00000000-0000-0000-0000-000000000000" },
      "paymentDate": "0001-01-01T00:00:00",
      "description": "",
      "amount": 0.0
    }
}
```