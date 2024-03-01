using FinanceSystem.Application.BankAccounts.Dtos;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Queries.GetUserBankAccounts
{
    public class GetUserBankAccountsQueryHandler : IRequestHandler<GetUserBankAccountsQuery, IEnumerable<BankAccountLiteDto>>
    {
        private readonly IRepository<BankAccount, BankAccountId> _repository;

        public GetUserBankAccountsQueryHandler(IRepository<BankAccount, BankAccountId> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BankAccountLiteDto>> Handle(GetUserBankAccountsQuery request, CancellationToken cancellationToken)
        {
            var userBankAccounts = await _repository.FindAsync(x => x.UserId == request.UserId);

            var bankAccountLiteDtos = userBankAccounts.Select(x => new BankAccountLiteDto(
                x.Id.Value,
                x.AccountName,
                x.AccountNumber,
                x.BankCode,
                x.Agency,
                x.AccountType,
                x.Balance ?? 0
            ));

            var result = bankAccountLiteDtos ?? Enumerable.Empty<BankAccountLiteDto>();

            return result;
        }
    }
}
