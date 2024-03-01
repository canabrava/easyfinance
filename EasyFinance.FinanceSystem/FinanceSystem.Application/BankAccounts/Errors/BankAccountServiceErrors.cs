using ErrorOr;

namespace FinanceSystem.Application.BankAccounts.Errors
{
    public static class BankAccountServiceErrors
    {
        public static readonly Error BankAccountNotFount = Error.NotFound("Bank account not found");
        public static readonly Error BankAccountNotBelongsToUser = Error.Unauthorized("Bank account not belongs to user");

    }
}
