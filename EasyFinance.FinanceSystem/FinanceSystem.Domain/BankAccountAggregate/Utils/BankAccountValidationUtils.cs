using System.Text.RegularExpressions;

namespace FinanceSystem.Domain.BankAccountAggregate.Utils
{
    public static class BankAccountValidationUtils
    {
        public static readonly string AccountNumberPattern = @"^\d{4}-\d{1}$";
        public static readonly string BankCodePattern = @"^\d{3}$";
        public static readonly string AgencyPattern = @"^\d{4}$";

        public static bool IsValidAccountNumber(string accountNumber)
        {
            return !string.IsNullOrWhiteSpace(accountNumber) && Regex.IsMatch(accountNumber, AccountNumberPattern);
        }

        public static bool IsValidBankCode(string bankCode)
        {
            return !string.IsNullOrWhiteSpace(bankCode) && Regex.IsMatch(bankCode, BankCodePattern);
        }

        public static bool IsValidAgency(string agency)
        {
            return !string.IsNullOrWhiteSpace(agency) && Regex.IsMatch(agency, AgencyPattern);
        }
    }
}
