namespace FinanceSystem.Contracts.BankAccounts
{
    public record BankAccountRequest(
        string AccountName,
        string AccountNumber,
        string BankCode,
        string Agency,
        string AccountType,
        decimal? Balance
    );
}
