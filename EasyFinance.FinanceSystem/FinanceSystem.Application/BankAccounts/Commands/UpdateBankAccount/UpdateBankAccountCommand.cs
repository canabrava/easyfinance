using ErrorOr;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.UpdateBankAccount
{
    public record UpdateBankAccountCommand(
        Guid UserId,
        Guid BankAccountId,
        string AccountName,
        string Agency,
        string AccountType) : IRequest<ErrorOr<Success>>;
}
