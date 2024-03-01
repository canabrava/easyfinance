using FluentValidation.TestHelper;
using FinanceSystem.Contracts.BankAccounts.Validators;
using FinanceSystem.Contracts.BankAccounts.Messages.Error.BankAccount;
using FinanceSystem.Tests.TestUtils.BankAccount;


namespace FinanceSystem.Tests.UnitTests.Contracts.Validators
{
    public class BankAccountRequestValidatorTests
    {
        private readonly BankAccountRequestValidator _validator;

        public BankAccountRequestValidatorTests()
        {
            _validator = new BankAccountRequestValidator();
        }

        [Fact]
        public void AccountName_ShouldHaveError_WhenExceedsMaxLength()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(AccountName: "123456789012345678901234567890123456789012345678901");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.AccountName)
                .WithErrorMessage(BankAccountErrorMessagesPt.AccountNameMaxLengthExceeded);
        }

        [Fact]
        public void AccountNumber_ShouldHaveError_WhenEmpty()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(AccountNumber: "");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.AccountNumber)
                .WithErrorMessage(BankAccountErrorMessagesPt.AccountRequired);
        }

        [Fact]
        public void AccountNumber_ShouldHaveError_WhenInvalidFormat()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(AccountNumber: "12534-56");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.AccountNumber)
                .WithErrorMessage(BankAccountErrorMessagesPt.AccountInvalidFormat);
        }

        [Fact]
        public void BankCode_ShouldHaveError_WhenEmpty()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(BankCode: "");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.BankCode)
                .WithErrorMessage(BankAccountErrorMessagesPt.BankCodeRequired);
        }

        [Fact]
        public void BankCode_ShouldHaveError_WhenInvalidFormat()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(BankCode: "1234");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.BankCode)
                .WithErrorMessage(BankAccountErrorMessagesPt.BankCodeInvalidFormat);
        }

        [Fact]
        public void Agency_ShouldHaveError_WhenEmpty()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(Agency: "");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Agency)
                .WithErrorMessage(BankAccountErrorMessagesPt.AgencyRequired);
        }

        [Fact]
        public void Agency_ShouldHaveError_WhenInvalidFormat()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(Agency: "12345");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Agency)
                .WithErrorMessage(BankAccountErrorMessagesPt.AgencyInvalidFormat);
        }

        [Fact]
        public void AccountType_ShouldHaveError_WhenEmpty()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(AccountType: "");

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.AccountType)
                .WithErrorMessage(BankAccountErrorMessagesPt.AccountRequired);
        }

        [Fact]
        public void Balance_ShouldHaveError_WhenNegativeValue()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(Balance: -100);

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Balance)
                .WithErrorMessage(BankAccountErrorMessagesPt.BalanceInvalidValue);
        }

        [Fact]
        public void Balance_ShouldHaveError_WhenInvalidFormat()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(Balance: 123.456m);

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Balance)
                .WithErrorMessage(BankAccountErrorMessagesPt.BalanceInvalidFormat);
        }

        [Fact]
        public void Balance_ShouldNotHaveError_WhenValidValue()
        {
            var request = BankAccountRequestFactory.CreateBankAccountRequest(Balance: 1000);

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(x => x.Balance);
        }
    }
}