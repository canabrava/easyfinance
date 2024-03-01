using FinanceSystem.Tests.TestConstants;

namespace FinanceSystem.Tests.TestUtils.BankAccount
{
    public static class BankAccountFactory
    {
        public static Domain.BankAccountAggregate.BankAccount CreateBankAccount(
           Guid? userId = null,
           string? accountName = null,
           string? accountNumber = null,
           string? bankCode = null,
           string? agency = null,
           string? accountType = null,
           decimal? balance = null)
        {
            return Domain.BankAccountAggregate.BankAccount.Create(
                userId ?? Guid.NewGuid(),
                accountName ?? Constants.BankAccount.AccountName,
                accountNumber ?? Constants.BankAccount.AccountNumber,
                bankCode ?? Constants.BankAccount.BankCode,
                agency ?? Constants.BankAccount.Agency,
                accountType ?? Constants.BankAccount.AccountType,
                balance ?? Constants.BankAccount.Balance).Value;
        }
    }
}
