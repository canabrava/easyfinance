import { getUser } from '../signals/userSignal';

export function getAuthorizationHeader() {
    const user = getUser();

    if (user) {
        return { Authorization: `${user.token}` };
    }

    return {};
};