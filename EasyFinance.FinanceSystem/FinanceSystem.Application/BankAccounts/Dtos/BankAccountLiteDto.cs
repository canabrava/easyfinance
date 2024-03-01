namespace FinanceSystem.Application.BankAccounts.Dtos
{
    public record BankAccountLiteDto(
        Guid Id,
        string AccountName,
        string AccountNumber,
        string BankCode,
        string Agency,
        string AccountType,
        decimal Balance);
}
