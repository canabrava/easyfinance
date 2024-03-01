using ErrorOr;

namespace FinanceSystem.Domain.BankAccountAggregate.Errors
{
    public static class BankAccountErrors
    {
        public static readonly Error MustHaveAnAccountName = Error.Validation(
            "BankAccount.MustHaveAnAccountName",
            "A conta bancária tem que ter um nome.");

        public static readonly Error MustHaveAValidAccountNumber = Error.Validation(
            "BankAccount.MustHaveAValidAccountNumber",
            "A conta bancária tem que ter um número de conta válido.");

        public static readonly Error MustHaveAValidBankCode = Error.Validation(
            "BankAccount.MustHaveAValidBankCode",
            "A conta bancária tem que ter um código de banco válido.");

        public static readonly Error MustHaveAValidAgency = Error.Validation(
            "BankAccount.MustHaveAValidAgency",
            "A conta bancária tem que ter uma agência válida.");

        public static readonly Error MustHaveAnAccountType = Error.Validation(
            "BankAccount.MustHaveAnAccountType",
            "A conta bancária tem que ter um tipo de conta.");

        public static readonly Error BalanceMustBeZeroOrPositive = Error.Validation(
            "BankAccount.BalanceMustBeZeroOrPositive",
            "A conta bancária tem que ter um saldo.");

        public static readonly Error NotEnoughBalance = Error.Validation(
                       "BankAccount.NotEnougthBalance",
                       "A conta bancária não tem saldo suficiente.");

        public static readonly Error TransactionNotFound = Error.Validation(
            "BankAccount.TransactionNotFound",
            "A transação não foi encontrada.");
    }
}
