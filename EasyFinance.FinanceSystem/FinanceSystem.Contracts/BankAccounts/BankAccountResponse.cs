namespace FinanceSystem.Contracts.BankAccounts
{
    public record BankAccountResponse(
        Guid Id,
        string AccountNumber,
        string BankCode,
        string Agency,
        string AccountType,
        decimal Balance
 );
}