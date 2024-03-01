namespace FinanceSystem.Contracts.BankAccounts.Messages.Error.BankAccount
{
    public static class BankAccountErrorMessagesPt
    {
        public const String AccountRequired = "Número da conta é obrigatório.";
        public const String AccountInvalidFormat = "Formato inválido para o número da conta. O formato deve ser XXXX-X.";
        public const String BankCodeRequired = "Código do banco é obrigatório.";
        public const String BankCodeInvalidFormat = "Formato inválido para o código do banco. O formato deve ser XXX.";
        public const String AgencyRequired = "Agência é obrigatória.";
        public const String AgencyInvalidFormat = "Formato inválido para a agência. O formato deve ser XXXX.";
        public const String AccountTypeRequired = "Tipo de conta é obrigatório.";
        public const String BalanceInvalidFormat = "O valor não pode ter mais de duas casas decimais.";
        public const String BalanceInvalidValue = "O valor não pode ser menor que zero.";
        public const String AccountNameMaxLengthExceeded = "O nome da conta não pode ter mais de 50 caracteres.";
    }
}
