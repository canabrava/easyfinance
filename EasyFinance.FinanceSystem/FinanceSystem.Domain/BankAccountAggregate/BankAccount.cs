using ErrorOr;
using FinanceSystem.Domain.BankAccountAggregate.Entities;
using FinanceSystem.Domain.BankAccountAggregate.Errors;
using FinanceSystem.Domain.BankAccountAggregate.Utils;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.BankAccountAggregate
{
    public sealed class BankAccount : AggregateRoot<BankAccountId>
    {
        public Guid UserId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; }
        public string BankCode { get; }
        public string Agency { get; private set; }
        public string AccountType { get; private set; }
        public decimal? Balance { get; private set; }
        public List<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();

        private BankAccount(
            BankAccountId id,
            Guid userId,
            string accountName,
            string accountNumber,
            string bankCode,
            string agency,
            string accountType,
            decimal? balance) : base(id)
        {
            UserId = userId;
            AccountName = accountName;
            AccountNumber = accountNumber;
            BankCode = bankCode;
            Agency = agency;
            AccountType = accountType;
            Balance = balance;
        }

        public static ErrorOr<BankAccount> Create(
            Guid userId,
            string accountName,
            string accountNumber,
            string bankCode,
            string agency,
            string accountType,
            decimal? balance = null)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return BankAccountErrors.MustHaveAnAccountName;
            }

            if (!BankAccountValidationUtils.IsValidAccountNumber(accountNumber))
            {
                return BankAccountErrors.MustHaveAValidAccountNumber;
            }

            if (!BankAccountValidationUtils.IsValidBankCode(bankCode))
            {
                return BankAccountErrors.MustHaveAValidBankCode;
            }

            if (!BankAccountValidationUtils.IsValidAgency(agency))
            {
                return BankAccountErrors.MustHaveAValidAgency;
            }

            if (string.IsNullOrWhiteSpace(accountType))
            {
                return BankAccountErrors.MustHaveAnAccountType;
            }

            if(balance < 0)
            {
                return BankAccountErrors.BalanceMustBeZeroOrPositive;
            }

            return new BankAccount(
                BankAccountId.CreateUnique(),
                userId,
                accountName,
                accountNumber,
                bankCode,
                agency,
                accountType,
                balance);
        }

        public ErrorOr<Success> UpdateBankAccountDetails(
            string accountName,
            string agency,
            string accountType)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return BankAccountErrors.MustHaveAnAccountName;
            }

            if (!BankAccountValidationUtils.IsValidAgency(agency))
            {
                return BankAccountErrors.MustHaveAValidAgency;
            }

            if (string.IsNullOrWhiteSpace(accountType))
            {
                return BankAccountErrors.MustHaveAnAccountType;
            }

            AccountName = accountName;
            Agency = agency;
            AccountType = accountType;

            return Result.Success;
        }

        public ErrorOr<Success> AddTransaction(FinancialTransaction transaction)
        {

            if(transaction.IsExpense())
            {
                if (Balance < transaction.Amount)
                {
                    return BankAccountErrors.NotEnoughBalance;
                }

                Balance -= transaction.Amount;
            }
            else
            {
                Balance += transaction.Amount;
            }

            FinancialTransactions.Add(transaction);

            return Result.Success;
        }

        public ErrorOr<Success> RemoveTransaction(FinancialTransaction transaction)
        {
            if(!FinancialTransactions.Contains(transaction))
            {
                return BankAccountErrors.TransactionNotFound;
            }

            if (transaction.IsIncome())
            {
                if(Balance < transaction.Amount)
                {
                    return BankAccountErrors.NotEnoughBalance;
                }

                Balance -= transaction.Amount;
            }
            else
            {
                Balance += transaction.Amount;
            }

            FinancialTransactions.Remove(transaction);

            return Result.Success;
        }
    }
}
