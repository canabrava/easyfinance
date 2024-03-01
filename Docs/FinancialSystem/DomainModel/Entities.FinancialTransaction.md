# Domain Models

## FinnancialTransaction

```csharp
class FinancialTransaction{
    ErrorOr<FinancialTransaction> Create(
            DateTime creationDate,
            decimal amount,
            ExpenseId? expenseId = null,
            IncomeId? incomeId = null);
    bool IsExpense();
    bool IsIncome();
}
```


```json
{
    "financialTransaction": {
        "id": { "value":"00000000-0000-0000-0000-000000000000" },
        "expenseId":  { "value":"00000000-0000-0000-0000-000000000000" },
        "incomeId": { "value":"00000000-0000-0000-0000-000000000000" },
        "creationDate": "0001-01-01T00:00:00",
        "amount": 0.0,
    }
}
```