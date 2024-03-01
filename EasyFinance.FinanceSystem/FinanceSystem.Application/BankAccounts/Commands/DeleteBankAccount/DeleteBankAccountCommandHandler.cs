using ErrorOr;
using FinanceSystem.Application.BankAccounts.Errors;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, ErrorOr<Success>>
    {
        private IRepository<BankAccount, BankAccountId> _repository;

        public DeleteBankAccountCommandHandler(IRepository<BankAccount, BankAccountId> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _repository.GetByIdAsync(BankAccountId.Create(request.BankAccountId));

            if (bankAccount is null)
            {
                return BankAccountServiceErrors.BankAccountNotFount;
            }

            if (bankAccount.UserId != request.UserId)
            {
                return BankAccountServiceErrors.BankAccountNotBelongsToUser;
            }

            await _repository.DeleteAsync(bankAccount);

            return Result.Success;
        }
    }
}
