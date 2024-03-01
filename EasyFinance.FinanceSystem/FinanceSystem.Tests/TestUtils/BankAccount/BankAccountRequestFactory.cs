using FinanceSystem.Contracts.BankAccounts;
using FinanceSystem.Tests.TestConstants;

namespace FinanceSystem.Tests.TestUtils.BankAccount
{
    public static class BankAccountRequestFactory
    {
        public static BankAccountRequest CreateBankAccountRequest(
            string? AccountName = null,
            string? AccountNumber = null,
            string? BankCode = null,
            string? Agency = null,
            string? AccountType = null,
            decimal? Balance = null)
        {
            return new BankAccountRequest(
                AccountName ?? Constants.BankAccount.AccountName,
                AccountNumber ?? Constants.BankAccount.AccountNumber,
                BankCode ?? Constants.BankAccount.BankCode,
                Agency ?? Constants.BankAccount.Agency,
                AccountType ?? Constants.BankAccount.AccountType,
                Balance ?? Constants.BankAccount.Balance);
        }
    }
}