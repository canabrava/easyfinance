using ErrorOr;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.DeleteBankAccount
{
    public record DeleteBankAccountCommand(Guid BankAccountId, Guid UserId) : IRequest<ErrorOr<Success>>;
}
