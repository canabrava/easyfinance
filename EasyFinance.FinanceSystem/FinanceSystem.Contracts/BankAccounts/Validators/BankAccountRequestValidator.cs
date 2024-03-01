using FluentValidation;
using FinanceSystem.Contracts.BankAccounts.Messages.Error.BankAccount;
using FinanceSystem.Domain.BankAccountAggregate.Utils;

namespace FinanceSystem.Contracts.BankAccounts.Validators
{
    public class BankAccountRequestValidator : AbstractValidator<BankAccountRequest>
    {
        public BankAccountRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .MaximumLength(maximumLength: 50).WithMessage(BankAccountErrorMessagesPt.AccountNameMaxLengthExceeded);

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage(BankAccountErrorMessagesPt.AccountRequired)
                .Matches(BankAccountValidationUtils.AccountNumberPattern).WithMessage(BankAccountErrorMessagesPt.AccountInvalidFormat);

            RuleFor(x => x.BankCode)
                .NotEmpty().WithMessage(BankAccountErrorMessagesPt.BankCodeRequired)
                .Matches(BankAccountValidationUtils.BankCodePattern).WithMessage(BankAccountErrorMessagesPt.BankCodeInvalidFormat);

            RuleFor(x => x.Agency)
                .NotEmpty().WithMessage(BankAccountErrorMessagesPt.AgencyRequired)
                .Matches(BankAccountValidationUtils.AgencyPattern).WithMessage(BankAccountErrorMessagesPt.AgencyInvalidFormat);

            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage(BankAccountErrorMessagesPt.AccountRequired);

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage(BankAccountErrorMessagesPt.BalanceInvalidValue)
                .PrecisionScale(int.MaxValue, 2, false).WithMessage(BankAccountErrorMessagesPt.BalanceInvalidFormat);
        }
    }
}
