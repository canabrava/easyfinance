using ErrorOr;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.AddBankAccount
{

    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, ErrorOr<Guid>>
    {
        private IRepository<BankAccount, BankAccountId> _repository;

        public AddBankAccountCommandHandler(IRepository<BankAccount, BankAccountId> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Guid>> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = BankAccount.Create(
                request.UserId,
                request.AccountName,
                request.AccountNumber,
                request.BankCode,
                request.Agency,
                request.AccountType,
                request.Balance);

            if(bankAccount.IsError)
            {
                return bankAccount.Errors.Single();
            }

            await _repository.AddAsync(bankAccount.Value);

            return bankAccount.Value.Id.Value;
        }
    }
}
