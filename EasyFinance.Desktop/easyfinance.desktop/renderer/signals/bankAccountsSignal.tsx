import { signal } from "@preact/signals-react"
import { BankAccount } from "../models/bankAccount"

const bankAccount: BankAccount[] = [];

const bankAccountsSignal = signal(bankAccount);

export function setBankAccountsSignal(bankAccount : BankAccount[]){
    bankAccountsSignal.value = bankAccount;
}

export function getBankAccountsSignal() : BankAccount[] {
    return bankAccountsSignal.value;
}