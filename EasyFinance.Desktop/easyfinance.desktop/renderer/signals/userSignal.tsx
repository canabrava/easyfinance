import { signal } from "@preact/signals-react"
import { UserModel } from "../models/user"

const userSignal = signal(undefined as UserModel | undefined);

export function setUser(user : UserModel){
    userSignal.value = user;
}

export function getUser(): UserModel | undefined {
    return userSignal.value;
}