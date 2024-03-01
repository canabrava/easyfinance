using ErrorOr;
using FinanceSystem.Application.BankAccounts.Dtos;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Queries.GetUserBankAccounts
{
    public record GetUserBankAccountsQuery(Guid UserId) : IRequest<IEnumerable<BankAccountLiteDto>>;
}
