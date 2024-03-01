using ErrorOr;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.AddBankAccount
{
    public record AddBankAccountCommand(
         Guid UserId,
         string AccountName,
         string AccountNumber,
         string BankCode,
         string Agency,
         string AccountType,
         decimal? Balance) : IRequest<ErrorOr<Guid>>;
}
