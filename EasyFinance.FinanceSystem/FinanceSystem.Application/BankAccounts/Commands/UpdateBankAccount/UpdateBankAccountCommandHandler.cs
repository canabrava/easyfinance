using ErrorOr;
using FinanceSystem.Application.BankAccounts.Errors;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using MediatR;

namespace FinanceSystem.Application.BankAccounts.Commands.UpdateBankAccount
{
    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, ErrorOr<Success>>
    {
        private IRepository<BankAccount, BankAccountId> _repository;

        public UpdateBankAccountCommandHandler(IRepository<BankAccount, BankAccountId> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _repository.GetByIdAsync(BankAccountId.Create(request.BankAccountId));

            if (bankAccount is null)
            {
                return BankAccountServiceErrors.BankAccountNotFount;
            }

            if(bankAccount.UserId != request.UserId)
            {
                return BankAccountServiceErrors.BankAccountNotBelongsToUser;
            }

            bankAccount.UpdateBankAccountDetails(
                request.AccountName,
                request.Agency,
                request.AccountType);

            await _repository.UpdateAsync(bankAccount);

            return Result.Success;
        }
    }
}
